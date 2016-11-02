using System;
using Android.App;
using authtest4;
using authtest4.Droid;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(authtest4Page), typeof(MainPageRenderer))]
namespace authtest4.Droid
{
	public class MainPageRenderer : PageRenderer
	{
		authtest4Page page;

		public MainPageRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);
			page = e.NewElement as authtest4Page;
			var activity = this.Context as Activity;
			page.platformParameters = new PlatformParameters(activity);
		}
	}
}
