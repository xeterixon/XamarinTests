using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GalleryTest
{
	class ImageData{
		public string Caption{get;set;}
		public string Image{get;set;}
	}
	public class App : Application
	{

		ObservableCollection<ImageData> ListOfStuff;
		List<string> ImageUrls{ get; set; }
		public App ()
		{
			// Some random images to use...
			ImageUrls = new List<string>
			{
				"http://cdn.filipekberg.se/fekberg-blog/wp-content/uploads/2014/04/VS2013Logo.png",
				"https://dynamicimagesen-v2b.netdna-ssl.com/product/en_97_professional-c-sharp-vol2.jpg",
				"http://taswar.zeytinsoft.com/wp-content/uploads/2012/11/dotnet.png",
				"http://charlespetzold.com/pwcs/pwcs.png"

			};
			// this is the list of images
			ListOfStuff = new ObservableCollection<ImageData> ();
			MainPage = new ContentPage {
				Content = new ScrollView
				{
					Content = new GalleryView {
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						ItemsSource = ListOfStuff
					}
				}
			};
			int index = 0;
			Device.StartTimer (TimeSpan.FromSeconds (2), () => {
				ListOfStuff.Add(
					new ImageData{
						Caption = DateTime.Now.ToString(),
						Image = ImageUrls[index] 
					}
				);
				index++;
				return ImageUrls.Count > index ;
			});
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

