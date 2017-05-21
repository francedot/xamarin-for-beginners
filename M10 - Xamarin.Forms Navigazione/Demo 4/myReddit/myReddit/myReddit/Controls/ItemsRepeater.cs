using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyReddit.Controls
{
    public class ItemsRepeater : StackLayout
    {

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(ItemsRepeater),
                default(IEnumerable<object>), BindingMode.TwoWay, null, ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ItemsRepeater), default(object),
                BindingMode.TwoWay, null, OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsRepeater),
                default(DataTemplate));

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object o, object newValue1)
        {
            var itemsLayout = (ItemsRepeater) bindable;
            itemsLayout.SetItems();
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (ItemsRepeater) bindable;
            if (newValue == oldValue)
                return;

            itemsView.SetSelectedItem(newValue);
        }

        #region Item Rendering

        protected readonly ICommand ItemSelectedCommand;

        protected virtual void SetItems()
        {
            Children.Clear();

            if (ItemsSource == null)
            {
                ObservableSource = null;
                return;
            }

            foreach (var item in ItemsSource)
            {
                var view = GetItemView(item);
                Children.Add(view);
            }

            // Now Supports ObservableCollection derived types
            var t = ItemsSource.GetType();
            var baseTypes = t.GetBaseTypes().ToList();
            foreach (var baseType in baseTypes)
            {
                var isObs = baseType.IsConstructedGenericType &&
                            baseType.GetGenericTypeDefinition() == typeof(ObservableCollection<>);
                if (isObs)
                {
                    object o =
                        Activator.CreateInstance(
                            typeof(ObservableReadOnlyCollection<>).MakeGenericType(baseType.GenericTypeArguments),
                            ItemsSource);
                    ObservableSource = (IObservableReadOnlyCollection<object>) o;
                    break;
                }
            }
        }

        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();

            var view = ((ViewCell) content).View;
            if (view == null)
                return null;

            view.BindingContext = item;

            var gesture = new TapGestureRecognizer
            {
                Command = ItemSelectedCommand,
                CommandParameter = item
            };

            AddGesture(view, gesture);

            return view;
        }

        protected void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            var layout = view as Layout<View>;

            if (layout == null)
                return;

            foreach (var child in layout.Children)
                AddGesture(child, gesture);
        }

        protected virtual void SetSelectedItem(object selectedItem)
        {
            var handler = SelectedItemChanged;
            handler?.Invoke(this, new SelectedItemChangedEventArgs(selectedItem));
        }

        IObservableReadOnlyCollection<object> _observableSource;

        protected IObservableReadOnlyCollection<object> ObservableSource
        {
            get { return _observableSource; }
            set
            {
                if (_observableSource != null)
                {
                    _observableSource.CollectionChanged -= CollectionChanged;
                }
                _observableSource = value;

                if (_observableSource != null)
                {
                    _observableSource.CollectionChanged += CollectionChanged;
                }
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    int index = e.NewStartingIndex;
                    foreach (var item in e.NewItems)
                        Children.Insert(index++, GetItemView(item));

                }
                    break;
                case NotifyCollectionChangedAction.Move:
                {
                    var item = ObservableSource[e.OldStartingIndex];
                    Children.RemoveAt(e.OldStartingIndex);
                    Children.Insert(e.NewStartingIndex, GetItemView(item));
                }
                    break;
                case NotifyCollectionChangedAction.Remove:
                {
                    Children.RemoveAt(e.OldStartingIndex);
                }
                    break;
                case NotifyCollectionChangedAction.Replace:
                {
                    Children.RemoveAt(e.OldStartingIndex);
                    Children.Insert(e.NewStartingIndex, GetItemView(ObservableSource[e.NewStartingIndex]));
                }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Children.Clear();
                    foreach (var item in ItemsSource)
                        Children.Add(GetItemView(item));
                    break;
            }
        }

        #endregion

        public ItemsRepeater()
        {
            this.Spacing = 0.0;
            ItemSelectedCommand = new Command<object>(item => { SelectedItem = item; });
        }
    }

    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.BaseType == null) return typeInfo.ImplementedInterfaces;

            return Enumerable.Repeat(typeInfo.BaseType, 1)
                .Concat(typeInfo.ImplementedInterfaces)
                .Concat(typeInfo.ImplementedInterfaces.SelectMany<Type, Type>(GetBaseTypes))
                .Concat(typeInfo.BaseType.GetBaseTypes());
        }
    }

    public interface IObservableReadOnlyCollection<out T> : IEnumerable<T>, INotifyCollectionChanged
    {
        T this[int i] { get; }
        int Count { get; }
    }

    public class ObservableReadOnlyCollection<T> : IObservableReadOnlyCollection<T>
    {
        public ObservableCollection<T> _inner;

        public ObservableReadOnlyCollection(ObservableCollection<T> inner)
        {
            _inner = inner;
        }

        public T this[int i] => _inner[i];
        public int Count => _inner.Count;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _inner.CollectionChanged += value; }
            remove { _inner.CollectionChanged -= value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }
}