using ProgressSoft.Core.Entites; // Ensure this is necessary for your context
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.Json;
using ZXing;
using ZXing.Windows.Compatibility;

var barcodeWriter = new BarcodeWriter
{
    Format = BarcodeFormat.QR_CODE,
    Options = new ZXing.Common.EncodingOptions
    {
        Width = 200,
        Height = 200
    }
};

// Combine the directory with the JSON file name
string filePath = Path.Combine(@"C:\Users\ahmad\ProgressSoftTask\API\QRCodeGenrator", "CardReaders.json");

// Read the JSON file content
string jsonContent = File.ReadAllText(filePath);

string desktopPath;

// Check if running on Windows
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
}
else
{
    Console.WriteLine("This application is designed to run on Windows.");
    return;
}

string imagePath = Path.Combine(desktopPath, "barcode.png");

var barcodeBitmap = barcodeWriter.Write(jsonContent);

barcodeBitmap.Save(imagePath, ImageFormat.Png);

Console.WriteLine($"Barcode saved to {imagePath}");
