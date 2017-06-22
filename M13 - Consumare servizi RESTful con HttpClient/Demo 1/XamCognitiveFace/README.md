# XamCognitiveFace

This repo includes a Xamarin.Android demo application using Microsoft Cognitive Services Face API to perform face detection and emotion recognition.<br/>

<img src="/CognitiveFace1.png" width="340"> <img src="/CognitiveFace2.png" width="340">
## Setup
In MainActivity.cs replace the first parameter of FaceServiceClient constructor with your Face API Key
```csharp
 private readonly FaceServiceClient _faceServiceClient = new FaceServiceClient("Your Face API Key", "https://westeurope.api.cognitive.microsoft.com/face/v1.0");
```

*Authors: Francesco Bonacci*
