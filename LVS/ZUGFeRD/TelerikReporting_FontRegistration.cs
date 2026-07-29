using LVS;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Fixed.Model.Fonts;


namespace LVS.ZUGFeRD
{
    public class TelerikReporting_FontRegistration
    {
        private static bool _fontsRegistered;

        // Liste der benötigten Font-Dateien (konfigurierbar)
        private static readonly string[] LiberationFiles = new[]
        {
            "LiberationSans-Regular.ttf",
            "LiberationSans-Bold.ttf",
            "LiberationSans-Italic.ttf",
            "LiberationSans-BoldItalic.ttf"
        };

        public static void RegisterLiberationSansFonts()
        {
            if (_fontsRegistered)
            {
                return;
            }

            string fontsFolder = Path.Combine(AppContext.BaseDirectory ?? AppDomain.CurrentDomain.BaseDirectory, "Fonts");
            try
            {
                // 1) Sicherstellen, dass Zielordner existiert
                if (!Directory.Exists(fontsFolder))
                    Directory.CreateDirectory(fontsFolder);

                // 2) Prüfen / ggf. kopieren aus Projektordner (Variante A)
                foreach (var fileName in LiberationFiles)
                {
                    string fullPath = Path.Combine(fontsFolder, fileName);

                    // helper_IOFile.CheckPath legt Zielpfad an (vorhanden im Projekt)
                    helper_IOFile.CheckPath(fullPath);

                    if (!helper_IOFile.CheckFile(fullPath))
                    {
                        // Versuche, aus Projektordner zu kopieren (lokales Dev-Setup)
                        string projectFonts = FindProjectFontsFolder();
                        if (!string.IsNullOrEmpty(projectFonts))
                        {
                            string src = Path.Combine(projectFonts, fileName);
                            if (File.Exists(src))
                            {
                                try
                                {
                                    File.Copy(src, fullPath, overwrite: true);
                                }
                                catch (Exception ex)
                                {
                                    Debug.WriteLine($"Font copy failed from project folder: {src} -> {fullPath}. {ex.Message}");
                                }
                            }
                        }

                        // Falls immer noch nicht vorhanden: versuche weitere Kandidaten (embedded resources / common paths)
                        if (!helper_IOFile.CheckFile(fullPath))
                        {
                            bool copied = TryCopyFromCandidateLocations(fullPath, fileName);
                            if (!copied)
                            {
                                Debug.WriteLine($"Font '{fileName}' not found in output or project folders.");
                            }
                        }
                    }
                }

                // 3) Registrierung der Fonts (nur wenn vorhanden)
                bool allAvailable = true;
                foreach (var fileName in LiberationFiles)
                {
                    string fullPath = Path.Combine(fontsFolder, fileName);
                    if (!helper_IOFile.CheckFile(fullPath))
                    {
                        allAvailable = false;
                        Debug.WriteLine($"Required font missing after copy attempts: {fullPath}");
                        break;
                    }
                }

                if (!allAvailable)
                {
                    throw new FileNotFoundException("Nicht alle benötigten Font-Dateien konnten bereitgestellt werden. Prüfe bin/Debug/.../Fonts oder Projekt Fonts-Ordner.");
                }

                // 4) Fonts für Telerik registrieren
                //RegisterFontFile(fontsFolder, "LiberationSans-Regular.ttf", "Liberation Sans", FontStyles.Normal, FontWeights.Normal);
                //RegisterFontFile(fontsFolder, "LiberationSans-Bold.ttf", "Liberation Sans", FontStyles.Normal, FontWeights.Bold);
                //RegisterFontFile(fontsFolder, "LiberationSans-Italic.ttf", "Liberation Sans", FontStyles.Italic, FontWeights.Normal);
                //RegisterFontFile(fontsFolder, "LiberationSans-BoldItalic.ttf", "Liberation Sans", FontStyles.Italic, FontWeights.Bold);

                RegisterFontFile(fontsFolder, "LiberationSans-Regular.ttf", "Liberation Sans", System.Windows.FontStyles.Normal, System.Windows.FontWeights.Normal);
                RegisterFontFile(fontsFolder, "LiberationSans-Bold.ttf", "Liberation Sans", System.Windows.FontStyles.Normal, System.Windows.FontWeights.Bold);
                RegisterFontFile(fontsFolder, "LiberationSans-Italic.ttf", "Liberation Sans", System.Windows.FontStyles.Italic, System.Windows.FontWeights.Normal);
                RegisterFontFile(fontsFolder, "LiberationSans-BoldItalic.ttf", "Liberation Sans", System.Windows.FontStyles.Italic, System.Windows.FontWeights.Bold);

                _fontsRegistered = true;
            }
            catch
            {
                // nicht schlucken: rethrow für Aufrufer sichtbar
                throw;
            }
        }




        //private static void RegisterFontFile(
        //                                        string fontsFolder,
        //                                        string fileName,
        //                                        string fontFamilyName,
        //                                        FontStyle fontStyle,
        //                                        FontWeight fontWeight)
        //{
        //    string fullPath = Path.Combine(fontsFolder, fileName);
        //    // helper_IOFile.CheckPath(fullPath); // schon sichergestellt
        //    if (!File.Exists(fullPath))
        //    {
        //        throw new FileNotFoundException($"Schriftdatei nicht gefunden: {fullPath}");
        //    }

        //    byte[] fontData = File.ReadAllBytes(fullPath);
        //    FontsRepository.RegisterFont(
        //                                new Telerik.Documents.Core.Fonts.FontFamily(fontFamilyName),
        //                                fontStyle,
        //                                fontWeight,
        //                                fontData);
        //    //Debug.WriteLine($"Registered font: {fontFamilyName} ({fileName})");
        //}


        private static void RegisterFontFile(
                                        string fontsFolder,
                                        string fileName,
                                        string fontFamilyName,
                                        System.Windows.FontStyle fontStyle,
                                        System.Windows.FontWeight fontWeight
                                        )
        {
            string fullPath = Path.Combine(fontsFolder, fileName);
            // helper_IOFile.CheckPath(fullPath); // schon sichergestellt
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Schriftdatei nicht gefunden: {fullPath}");
            }

            byte[] fontData = File.ReadAllBytes(fullPath);
            FontsRepository.RegisterFont(
                                        new System.Windows.Media.FontFamily(fontFamilyName),
                                        fontStyle,
                                        fontWeight,
                                        fontData);
            //Debug.WriteLine($"Registered font: {fontFamilyName} ({fileName})");
        }

        // versucht mehrere Standard-Quellen (Program/Windows-Fonts/embedded ressources) als Fallback
        private static bool TryCopyFromCandidateLocations(string fullPath, string fileName)
        {
            var baseDir = AppContext.BaseDirectory ?? AppDomain.CurrentDomain.BaseDirectory;
            var entryDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? baseDir) ?? baseDir;

            var candidates = new[]
            {
                Path.Combine(baseDir, "Fonts", fileName),
                Path.Combine(baseDir, fileName),
                Path.Combine(entryDir, "Fonts", fileName),
                Path.Combine(entryDir, fileName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Fonts", fileName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Fonts", fileName), // System Fonts
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Fonts", fileName),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Fonts", fileName)
            }
            .Select(p => { try { return Path.GetFullPath(p); } catch { return p; } })
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

            foreach (var src in candidates)
            {
                try
                {
                    if (File.Exists(src))
                    {
                        EnsureTargetDir(fullPath);
                        File.Copy(src, fullPath, overwrite: true);
                        if (File.Exists(fullPath))
                            return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Copy candidate failed: {src} -> {fullPath}. {ex.Message}");
                }
            }

            // embedded resources prüfen
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                string[] resources;
                try
                {
                    resources = asm.GetManifestResourceNames();
                }
                catch
                {
                    continue;
                }

                if (resources == null || resources.Length == 0)
                    continue;

                string match = resources.FirstOrDefault(r =>
                    r.EndsWith("." + fileName, StringComparison.OrdinalIgnoreCase) &&
                    r.IndexOf("liberation", StringComparison.OrdinalIgnoreCase) >= 0)
                    ?? resources.FirstOrDefault(r => r.EndsWith("." + fileName, StringComparison.OrdinalIgnoreCase));

                if (match != null)
                {
                    try
                    {
                        using (var rs = asm.GetManifestResourceStream(match))
                        {
                            if (rs != null)
                            {
                                EnsureTargetDir(fullPath);
                                using (var fs = File.Create(fullPath))
                                {
                                    rs.CopyTo(fs);
                                }
                                if (File.Exists(fullPath))
                                    return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Resource extract failed: {match} -> {fullPath}. {ex.Message}");
                    }
                }
            }

            return false;
        }

        // Aufwärts-Suche nach Projekt- oder Solution-Ordner mit Fonts
        private static string FindProjectFontsFolder()
        {
            try
            {
                var dir = new DirectoryInfo(AppContext.BaseDirectory ?? AppDomain.CurrentDomain.BaseDirectory);
                int maxLevels = 10;
                for (int i = 0; i < maxLevels && dir != null; i++)
                {
                    var fontsHere = Path.Combine(dir.FullName, "Fonts");
                    if (Directory.Exists(fontsHere))
                        return fontsHere;

                    var lvsFonts = Path.Combine(dir.FullName, "LVS", "Fonts");
                    if (Directory.Exists(lvsFonts))
                        return lvsFonts;

                    // Indikator: csproj oder sln vorhanden
                    try
                    {
                        if (dir.GetFiles("LVS.csproj").Any() || dir.GetFiles("*.sln").Any())
                        {
                            var candidate = Path.Combine(dir.FullName, "Fonts");
                            if (Directory.Exists(candidate))
                                return candidate;
                        }
                    }
                    catch { }

                    dir = dir.Parent;
                }
            }
            catch { }

            return null;
        }

        private static void EnsureTargetDir(string fullPath)
        {
            var targetDir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);
        }
    }
}