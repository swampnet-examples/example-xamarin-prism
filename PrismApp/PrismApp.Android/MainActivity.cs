using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;

using PrismApp;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Android.Runtime;

namespace PrismApp.Droid
{
    [Activity(Label = "PrismApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;

			global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}

	public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
			// Register any platform specific implementations
			container.RegisterInstance<IBatteryService>(new BatteryService());
			container.RegisterInstance<ILabelPrintService>(new LabelPrintService());
		}
	}
}

