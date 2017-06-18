using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyReddit.Helpers
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }
        public IList<T> Values { get; private set; }
        public Grouping(K key, IList<T> values)
        {
            Key = key;
            Values = values;
            foreach (var value in values)
            {
                base.Items.Add(value);
            }
        }
    }
}