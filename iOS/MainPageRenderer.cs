using System;
using authtest4;
using authtest4.iOS;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
//using Microsoft.Identity.Client;
//using Microsoft.Identity.Client;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(authtest4Page), typeof(MainPageRenderer))]
namespace authtest4.iOS
{
	public class MainPageRenderer : PageRenderer
	{
		authtest4Page page;

		public MainPageRenderer()
		{
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			page = e.NewElement as authtest4Page;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			page.platformParameters = new PlatformParameters(this);
		}
	}
}
