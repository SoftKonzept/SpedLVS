using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LvsScan.Portable.Helper
{
    public class helper_AndroidPermission
    {
        /// <summary>
        ///             Die Funktion ermittelt Freigabe für den Zugriff auf den Speicher.
        ///             
        ///             Android 13 und höher benötigt die Berechtigung "StorageRead" für den Lesezugriff auf den Speicher.
        ///             •	Verwenden Sie die neuen Berechtigungen READ_MEDIA_IMAGES, READ_MEDIA_VIDEO und READ_MEDIA_AUDIO für Android 13.
        ///             •	Passen Sie Ihre Berechtigungslogik an die Android-Version an.
        ///             •	Fügen Sie die Berechtigungen in der AndroidManifest.xml hinzu.
        ///             •	Nutzen Sie AppInfo.ShowSettingsUI(), um den Benutzer zu den App-Einstellungen zu leiten, falls die Berechtigung verweigert wurde.
        ///             <uses-permission android:name="android.permission.READ_MEDIA_IMAGES" />
        ///             <uses-permission android:name="android.permission.READ_MEDIA_VIDEO" />
        ///             <uses-permission android:name="android.permission.READ_MEDIA_AUDIO" />
        /// </summary>
        public static async Task<PermissionStatus> RequestStoragePermissionAsync()
        {
            if (DeviceInfo.Version.Major >= 13) // Android 13 oder höher
            {
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Berechtigung erforderlich",
                        "Bitte aktivieren Sie die Speicherberechtigung in den App-Einstellungen.",
                        "OK"
                    );
                    AppInfo.ShowSettingsUI(); // Öffnet die App-Einstellungen
                }
                return status;
            }
            else
            {
                // Für ältere Android-Versionen
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Berechtigung erforderlich",
                        "Bitte aktivieren Sie die Speicherberechtigung in den App-Einstellungen.",
                        "OK"
                    );
                    AppInfo.ShowSettingsUI();
                }
                return status;
            }
        }

        public static async Task<PermissionStatus> RequestCameraPermissionAsync()
        {
            if (DeviceInfo.Version.Major >= 13) // Android 13 oder höher
            {
                //var status = await Permissions.RequestAsync<Permissions.StorageRead>();
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Kamera-Berechtigung erforderlich",
                        "Bitte aktivieren Sie die Kamera-Berechtigung in den App-Einstellungen.",
                        "OK"
                    );
                    AppInfo.ShowSettingsUI(); // Öffnet die App-Einstellungen
                }
                return status;
            }
            else
            {
                // Für ältere Android-Versionen
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if (status != PermissionStatus.Granted)
                {
                    await App.Current.MainPage.DisplayAlert(
                        "Kamera-Berechtigung erforderlich",
                        "Bitte aktivieren Sie die Kamera-Berechtigung in den App-Einstellungen.",
                        "OK"
                    );
                    AppInfo.ShowSettingsUI();
                }
                return status;
            }
        }
    }
}
