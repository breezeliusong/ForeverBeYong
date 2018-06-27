using ForeverBeYoung.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
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
        private Geopoint geopoint, fromGeopoint, toGeopoint;
        private ObservableCollection<Geopoint> fromGeopoints, toGeopoints;
        private List<MapLocation> fromLocations, toLocations;
        public MapPage()
        {
            this.InitializeComponent();
            mapHelper = new MapHelper();
            fromGeopoints = new ObservableCollection<Geopoint>();
            toGeopoints = new ObservableCollection<Geopoint>();

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    geolocator = new Geolocator { DesiredAccuracyInMeters = 0, DesiredAccuracy = PositionAccuracy.High };
                    geolocator.StatusChanged += Geolocator_StatusChanged;
                    geolocator.PositionChanged += Geolocator_PositionChanged;

                    Geoposition geoposition = await geolocator.GetGeopositionAsync();
                    geopoint = geoposition.Coordinate.Point;
                    mapHelper.AddMapIcon(geopoint, mapControl, "I am here");
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
            //TODO
        }

        private void Geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            //TODO
        }

        private async void FromASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                fromLocations = await mapHelper.FindGecodeAsync(sender.Text, geopoint);
                if (fromLocations != null)
                {
                    var Towns = new ObservableCollection<string>();
                    foreach (var location in fromLocations)
                    {
                        Towns.Add(location.Address.Town);
                        fromGeopoints.Add(location.Point);
                    }
                    sender.ItemsSource = Towns;
                }
                else
                    sender.ItemsSource = new string[] { "No results found" };
            }
        }

        private async void ToASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                toLocations = await mapHelper.FindGecodeAsync(sender.Text, geopoint);
                if (toLocations != null)
                {
                    var Towns = new ObservableCollection<string>();
                    foreach (var location in toLocations)
                    {
                        Towns.Add(location.Address.Town);
                        toGeopoints.Add(location.Point);
                    }
                    sender.ItemsSource = Towns;
                }
                else
                    sender.ItemsSource = new string[] { "No results found" };
            }
        }


        private async void FindRoutesAsync(object sender, RoutedEventArgs e)
        {
            if (ToASB.Text == string.Empty)
            {
                await new MessageDialog("Please enter the destination").ShowAsync();
            }
            if (FromASB.Text == string.Empty)
            {
                fromGeopoint = geopoint;
            }
            MapRouteFinderResult routeFinderResults = await MapRouteFinder.GetDrivingRouteAsync(fromGeopoint, toGeopoint, MapRouteOptimization.TimeWithTraffic, MapRouteRestrictions.None);
            if (routeFinderResults.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeFinderResults.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapControl.TrySetViewBoundsAsync(
                      routeFinderResults.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
            else
            {
                await new MessageDialog("No route found").ShowAsync();
            }

        }

        private void FromASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                foreach (var location in fromLocations)
                {
                    if (location.Address.Town == args.ChosenSuggestion.ToString())
                    {
                        fromGeopoint = location.Point;
                        mapHelper.AddMapIcon(location.Point, mapControl, location.Address.Town+">>>>From here?");
                    }
                    else
                    {
                        //TODO
                    }
                }
            }
            else
            {
                if (FromASB.Text == null)
                {
                    mapHelper.AddMapIcon(geopoint, mapControl, "I am here");
                }
                else
                {
                    foreach (var location in fromLocations)
                    {
                        mapHelper.AddMapIcon(location.Point, mapControl, location.Address.Town +">>>>From here?");
                    }
                }
            }
        }

        private void ToASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                foreach (var location in toLocations)
                {
                    if (location.Address.Town == args.ChosenSuggestion.ToString())
                    {
                        toGeopoint = location.Point;
                        mapHelper.AddMapIcon(location.Point, mapControl, location.Address.Town);
                    }
                    else
                    {
                        //TODO
                    }
                }
            }
            else
            {
                if (ToASB.Text == null)
                {
                    mapHelper.AddMapIcon(geopoint, mapControl, "I am here");
                }
                else
                {
                    foreach (var location in toLocations)
                    {
                        mapHelper.AddMapIcon(location.Point, mapControl, location.Address.Town);
                    }
                }
            }
        }
    }
}
