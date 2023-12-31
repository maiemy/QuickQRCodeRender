## About

Libreria per la generazione di immagini QRCode personalizzabili nella forma, nei finder e nel logo centrale. Estendibile nella generazione di nuove forme grafiche
Si basa sul calcolo della matrice di punti della libreria [**QRCoder**](https://github.com/codebude/QRCoder) di "codebude" Raffael Herrmann che ringrazio per l'ottimo lavoro.

***

## Documentation

👉 *Your first place to go should be our wiki. Here you can find a detailed documentation 

### Release Notes


## Usage / Quick start

You only need four lines of code, to generate and view your first QR code.

```csharp

    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
    QRCodeData qrData = qRCodeGenerator.CreateQrCode(@"https://www.maiemyphoto.com", QRCodeGenerator.ECCLevel.L, eciMode: QRCodeGenerator.EciMode.Utf8, requestedVersion: 4);
    RenderMatrix renderMatrix = new RenderMatrix(qrData);
    RenderMatrixOptions opts = new RenderMatrixOptions();
    IDrawQRCode renderer = new DefaultRenderer();
    Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix,opts);

    bitmapRis.Save(@"C:\temp\QRBreak.png", ImageFormat.Png);
}
```

### Optional parameters and overloads

The GetGraphics-method has some more overloads. The first two enable you to set the color of the QR code graphic. One uses Color-class-types, the other HTML hex color notation.

```csharp
//Set color by using Color-class types
Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.DarkRed, Color.PaleGreen, true);

//Set color by using HTML hex color notation
Bitmap qrCodeImage = qrCode.GetGraphic(20, "#000ff0", "#0ff000");
```

The other overload enables you to render a logo/image in the center of the QR code.

```csharp
Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));
```

There are a plenty of other options. So feel free to read more on that in our wiki: [Wiki: How to use QRCoder](https://github.com/codebude/QRCoder/wiki/How-to-use-QRCoder)

## Help & Issues

If you think you have found a bug or have new ideas or feature requests, then feel free to open a new issue: https://github.com/codebude/QRCoder/issues 

In case you have a question about using the library (and couldn't find an answer in our wiki), feel free to open a new question/discussion: https://github.com/codebude/QRCoder/discussions


## Legal information and credits

QRCoder is a project by [Raffael Herrmann](https://raffaelherrmann.de) and was first released in 10/2013. It's licensed under the [MIT license](https://github.com/codebude/QRCoder/blob/master/LICENSE.txt).