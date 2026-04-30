using System;
using System.IO;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Fonts;

public class TelerikReporting_FontRegistration
{
    private static bool _fontsRegistered;

    public static void RegisterLiberationSansFonts()
    {
        if (_fontsRegistered)
        {
            return;
        }

        string fontsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts");

        RegisterFontFile(
            fontsFolder,
            "LiberationSans-Regular.ttf",
            "Liberation Sans",
            FontStyles.Normal,
            FontWeights.Normal);

        RegisterFontFile(
            fontsFolder,
            "LiberationSans-Bold.ttf",
            "Liberation Sans",
            FontStyles.Normal,
            FontWeights.Bold);

        RegisterFontFile(
            fontsFolder,
            "LiberationSans-Italic.ttf",
            "Liberation Sans",
            FontStyles.Italic,
            FontWeights.Normal);

        RegisterFontFile(
            fontsFolder,
            "LiberationSans-BoldItalic.ttf",
            "Liberation Sans",
            FontStyles.Italic,
            FontWeights.Bold);

        _fontsRegistered = true;
    }

    private static void RegisterFontFile(
        string fontsFolder,
        string fileName,
        string fontFamilyName,
        FontStyle fontStyle,
        FontWeight fontWeight)
    {
        string fullPath = Path.Combine(fontsFolder, fileName);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"Schriftdatei nicht gefunden: {fullPath}");
        }

        byte[] fontData = File.ReadAllBytes(fullPath);
        FontsRepository.RegisterFont(
            new Telerik.Documents.Core.Fonts.FontFamily(fontFamilyName),
            fontStyle,
            fontWeight,
            fontData);
    }
}