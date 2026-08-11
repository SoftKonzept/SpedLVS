using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace LVS.Mail
{
    /// <summary>
    /// Verwaltet verschlüsselte E-Mail-Credentials in einer Datei
    /// Nutzt Windows DPAPI für sichere Verschlüsselung
    /// </summary>
    public class MailCredentialsManager
    {
        private readonly string _credentialsFilePath;
        private const string XML_ROOT = "MailCredentials";
        private const string XML_CREDENTIALS = "Credential";

        public MailCredentialsManager(string credentialsFilePath = null)
        {
            if (string.IsNullOrWhiteSpace(credentialsFilePath))
            {
                // Standard-Pfad: Applikationsverzeichnis + "config" Ordner
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string configPath = Path.Combine(appPath, "config");
                
                if (!Directory.Exists(configPath))
                {
                    Directory.CreateDirectory(configPath);
                }
                
                _credentialsFilePath = Path.Combine(configPath, "mail_credentials.xml");
            }
            else
            {
                _credentialsFilePath = credentialsFilePath;
                
                // Sicherstellen, dass das Verzeichnis existiert
                string directory = Path.GetDirectoryName(_credentialsFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        /// <summary>
        /// Speichert E-Mail-Credentials verschlüsselt in der Datei
        /// </summary>
        public bool SaveCredentials(string profileName, MailCheckConfig config)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profileName) || config == null)
                    return false;

                XDocument doc = LoadOrCreateXml();
                
                // Existierendes Profil entfernen, falls vorhanden
                var existingElement = doc.Root?.Elements(XML_CREDENTIALS)
                    .FirstOrDefault(e => e.Attribute("name")?.Value == profileName);
                
                if (existingElement != null)
                {
                    existingElement.Remove();
                }

                // Neues Element erstellen und verschlüsselte Daten hinzufügen
                var credElement = new XElement(XML_CREDENTIALS,
                    new XAttribute("name", profileName),
                    new XAttribute("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("Server", EncryptString(config.Server ?? string.Empty)),
                    new XElement("Port", EncryptString(config.Port.ToString())),
                    new XElement("Username", EncryptString(config.Username ?? string.Empty)),
                    new XElement("Password", EncryptString(config.Password ?? string.Empty)),
                    new XElement("MailFrom", EncryptString(config.MailFrom ?? string.Empty)),
                    new XElement("EnableSsl", EncryptString(config.EnableSsl.ToString())),
                    new XElement("MailBCC", EncryptString(config.MailBCC ?? string.Empty))
                );

                doc.Root?.Add(credElement);
                doc.Save(_credentialsFilePath);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Speichern der Credentials: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lädt E-Mail-Credentials aus der verschlüsselten Datei
        /// </summary>
        public MailCheckConfig LoadCredentials(string profileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profileName) || !File.Exists(_credentialsFilePath))
                    return null;

                XDocument doc = XDocument.Load(_credentialsFilePath);
                
                var credElement = doc.Root?.Elements(XML_CREDENTIALS)
                    .FirstOrDefault(e => e.Attribute("name")?.Value == profileName);

                if (credElement == null)
                    return null;

                var config = new MailCheckConfig
                {
                    Server = DecryptString(credElement.Element("Server")?.Value ?? string.Empty),
                    Port = int.TryParse(DecryptString(credElement.Element("Port")?.Value ?? "0"), out int port) ? port : 0,
                    Username = DecryptString(credElement.Element("Username")?.Value ?? string.Empty),
                    Password = DecryptString(credElement.Element("Password")?.Value ?? string.Empty),
                    MailFrom = DecryptString(credElement.Element("MailFrom")?.Value ?? string.Empty),
                    EnableSsl = bool.TryParse(DecryptString(credElement.Element("EnableSsl")?.Value ?? "true"), out bool ssl) ? ssl : true,
                    MailBCC = DecryptString(credElement.Element("MailBCC")?.Value ?? string.Empty)
                };

                return config;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Laden der Credentials: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Listet alle gespeicherten Credential-Profile auf
        /// </summary>
        public string[] GetAllProfileNames()
        {
            try
            {
                if (!File.Exists(_credentialsFilePath))
                    return new string[0];

                XDocument doc = XDocument.Load(_credentialsFilePath);
                
                return doc.Root?.Elements(XML_CREDENTIALS)
                    .Select(e => e.Attribute("name")?.Value)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToArray() ?? new string[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Abrufen der Profile: {ex.Message}");
                return new string[0];
            }
        }

        /// <summary>
        /// Löscht ein gespeichertes Credential-Profil
        /// </summary>
        public bool DeleteProfile(string profileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(profileName) || !File.Exists(_credentialsFilePath))
                    return false;

                XDocument doc = XDocument.Load(_credentialsFilePath);
                
                var credElement = doc.Root?.Elements(XML_CREDENTIALS)
                    .FirstOrDefault(e => e.Attribute("name")?.Value == profileName);

                if (credElement == null)
                    return false;

                credElement.Remove();
                doc.Save(_credentialsFilePath);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Fehler beim Löschen des Profils: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Verschlüsselt einen String mit Windows DPAPI
        /// </summary>
        private string EncryptString(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                return string.Empty;

            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Verschlüsselungsfehler: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Entschlüsselt einen String mit Windows DPAPI
        /// </summary>
        private string DecryptString(string encryptedText)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
                return string.Empty;

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Entschlüsselungsfehler: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Lädt oder erstellt das XML-Dokument für die Credentials
        /// </summary>
        private XDocument LoadOrCreateXml()
        {
            try
            {
                if (File.Exists(_credentialsFilePath))
                {
                    return XDocument.Load(_credentialsFilePath);
                }
            }
            catch
            {
                // Datei existiert aber kann nicht gelesen werden - neu erstellen
            }

            // Neues XML-Dokument erstellen
            return new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(XML_ROOT)
            );
        }

        /// <summary>
        /// Gibt den Pfad der Credentials-Datei zurück
        /// </summary>
        public string GetCredentialsFilePath()
        {
            return _credentialsFilePath;
        }
    }
}