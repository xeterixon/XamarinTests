using System;
using Xamarin.Forms;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;

namespace GalleryTest
{
	public class GalleryViewCell : ContentView
	{
		private Label _caption;
		private Image _image;
		public GalleryViewCell()
		{
			HeightRequest = 100;
			WidthRequest = 100;
			BackgroundColor = Color.Gray;
			_caption = new Label{ HeightRequest = 18, WidthRequest = 100 };
			_image = new Image{ WidthRequest = 100, HeightRequest = 70, BackgroundColor = Color.Yellow };
			_image.SetBinding (Image.SourceProperty, "Image");
			_caption.SetBinding(Label.TextProperty,  "Caption");
			StackLayout layout = new StackLayout
			{ 
				Orientation = StackOrientation.Vertical,
				Children = {
					_image,
					_caption
				}
			};
			Content = layout;
		}

	}
	/// <summary>
	/// Gallery view.
	/// The ItemsSource property should be bound to a ObservableCollection.
	/// </summary>
	public class GalleryView : WrapLayout
	{
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<GalleryView,IEnumerable> ((prop) => prop.ItemsSource, default(IEnumerable), 
			propertyChanged:(bindable, oldValue, newValue) =>  ((GalleryView)bindable).ImageSourceChanged(),
			propertyChanging:(bindable, oldValue, newValue) =>  ((GalleryView)bindable).ImageSourceAboutToChange()
		);


		public IEnumerable ItemsSource {
			get{ return (IEnumerable)GetValue (ItemsSourceProperty); }
			set{ SetValue (ItemsSourceProperty, value); }
		}
		private void ImageSourceChanged()
		{
			var occ = this.ItemsSource as INotifyCollectionChanged;
			if (occ != null) {
				occ.CollectionChanged += HandleCollectionChanged;
				AddItems (ItemsSource);
			}

		}
		private void ImageSourceAboutToChange()
		{
			var occ = ItemsSource as INotifyCollectionChanged;
			if (occ != null) {
				occ.CollectionChanged -= HandleCollectionChanged;
				Children.Clear ();
			}
		}
		private void AddItems(IEnumerable list)
		{
			foreach (var item in list)
			{
				Children.Add ( new GalleryViewCell{ BindingContext = item});
			}

		}

		protected void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			// Only dealing with Reset and Add actions for now.
			// Implement the others yourself ... ;)
			if (e.Action == NotifyCollectionChangedAction.Reset) {
				Children.Clear ();
			}
			else if (e.Action == NotifyCollectionChangedAction.Add) 
			{
				if (e.NewItems != null) 
				{
					AddItems (e.NewItems);
				}
			}
			
		}
		public GalleryView ()
		{
		}
	}
}

