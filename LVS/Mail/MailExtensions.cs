using Common.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LVS.Mail
{
    public static class MailExtensions
    {
        /// <summary>
        /// Entschlüsselt DPAPI-geschützte Bytes (CurrentUser) und übergibt die Klar-JSON-Bytes an Users.
        /// Nutzung: user.ApplyProtectedMailCredentials(protectedBytes);
        /// </summary>
        public static void ApplyProtectedMailCredentials(this Users user, byte[] protectedBytes)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (protectedBytes == null || protectedBytes.Length == 0) return;

            try
            {
                var clear = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
                // clear sind UTF8-JSON-Bytes wie erwartet
                user.SetMailCredentialsFromJsonBytes(clear);
            }
            catch (Exception ex)
            {
                // defensiv: Fehler behandeln/loggen, aber keine sensiblen Daten ausgeben
                System.Diagnostics.Debug.WriteLine($"ApplyProtectedMailCredentials: Unprotect failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Hilfsmethode für Base64-String (z.B. aus DB-Feld), erkennt DPAPI-protected Base64 und ruft Unprotect auf.
        /// </summary>
        public static void ApplyProtectedMailCredentialsFromBase64(this Users user, string base64Protected)
        {
            if (string.IsNullOrWhiteSpace(base64Protected)) return;
            try
            {
                var bytes = Convert.FromBase64String(base64Protected);
                user.ApplyProtectedMailCredentials(bytes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ApplyProtectedMailCredentialsFromBase64: invalid base64: {ex.Message}");
            }
        }
    }
}