﻿using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Cinepolis
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Portrait)]
    //[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            const int requestNotification = 0;
            string[] notiPermission =
              {
                Manifest.Permission.PostNotifications
              };

            if ((int)Build.VERSION.SdkInt < 33) return;

            if (CheckSelfPermission(Manifest.Permission.PostNotifications) == Permission.Granted)
                return;
            
                RequestPermissions(notiPermission, requestNotification);
            
        }
    }
}
