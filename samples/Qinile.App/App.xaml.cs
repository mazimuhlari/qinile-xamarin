using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Akavache;
using Qinile.App.Views;
using Qinile.Core;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Qinile.App
{
    public partial class App : Application
    {
        public static Application Instance { get; set; }
        public NavigationPage Stack { get; set; }

        public App()
        {
            Instance = this;
            InitializeComponent();

            VersionTracking.Track();
            Registrations.Start(Configuration.APP_NAMESPACE);
            BlobCache.ApplicationName = Configuration.APP_NAMESPACE;
            Preferences.Set(PreferenceKeys.TOKEN_KEY, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI1ZmVmYmVlNWMxOWY1YTFkOWJlNmE5ZjciLCJpYXQiOjE2MDk3Njc5NjF9.voCiw-cB2FuObV99ueTiY0xCvaZYLYqhCko2EToVSqM");
            Stack = new NavigationPage(new XListPage());
            Instance.MainPage = Stack;
        }

        protected override void OnStart()
        {
            AppCenter.Start($"ios={Configuration.APPCENTER_SECRET_APPLE}android={Configuration.APPCENTER_SECRET_ANDROID}", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            var caches = new[]
            {
                BlobCache.LocalMachine,
                BlobCache.Secure,
                BlobCache.UserAccount,
                BlobCache.InMemory
            };

            caches
                .Select(x => x.Flush())
                .Merge()
                .Select(_ => Unit.Default)
                .Wait();
        }

        protected override void OnResume()
        {
        }
    }
}