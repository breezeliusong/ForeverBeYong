using ForeverBeYoung.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForeverBeYoung.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private MapHelper mapHelper;
        private Geolocator geolocator;
        private Geopoint geopoint;
        public MapPage()
        {
            this.InitializeComponent();
            mapHelper = new MapHelper();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    geolocator = new Geolocator { DesiredAccuracyInMeters = 0, DesiredAccuracy = PositionAccuracy.Default };
                    geolocator.StatusChanged += Geolocator_StatusChanged;
                    geolocator.PositionChanged += Geolocator_PositionChanged;

                    Geoposition geoposition = await geolocator.GetGeopositionAsync();
                    geopoint = geoposition.Coordinate.Point;
                    MapIcon LocationIcon = new MapIcon()
                    {
                        Location = geopoint,
                        CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,
                        //Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icon.png")),
                        Title = "I am here",
                        NormalizedAnchorPoint = new Point(0.5, 1.0)
                    };
                    var Icons = new List<MapElement>();
                    Icons.Add(LocationIcon);
                    MapElementsLayer mapLayer = new MapElementsLayer()
                    {
                        ZIndex = 1,
                        MapElements = Icons
                    };

                    mapControl.Layers.Add(mapLayer);
                    mapControl.Center = geopoint;
                    mapControl.ZoomLevel = 16;



                    break;

                case GeolocationAccessStatus.Denied:
                    await new MessageDialog("You denied!").ShowAsync();
                    break;

                case GeolocationAccessStatus.Unspecified:
                    await new MessageDialog("Unexpected error happened!").ShowAsync();

                    break;

            }

        }

        private void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {

        }

        private void Geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {

        }

        private async void FromASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var Locations = await mapHelper.FindGecodeAsync(sender.Text, geopoint);
                if (Locations != null)
                {
                    sender.ItemsSource = Locations;
                }
                else
                sender.ItemsSource = new string[] { "No results found" };
            }
        }

        private void ToASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindRoutesAsync(object sender, RoutedEventArgs e)
        {

        }
    }
}
