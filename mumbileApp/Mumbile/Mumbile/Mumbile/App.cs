using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mumbile.Services;
using Mumbile.XAML_Implementation;
using Xamarin.Forms;

namespace Mumbile
{
    public class App : Application
    {
        public App()
        {
			var tabs = new TabbedPage ();
			var navPage = new NavigationPage () {Title="App Content"};
			tabs.Children.Add (navPage);

			bool useXaml = false; //change this to use the code implementation

			if (useXaml) {
				tabs.Children.Add (new LoadingLabelXaml ());
			} else {
				tabs.Children.Add (new LoadingLabelCode ());
			}

			MainPage = new LoadingLabelCode();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
