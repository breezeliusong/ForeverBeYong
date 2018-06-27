using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;

namespace ForeverBeYoung.ViewModels
{
    public class MapHelper
    {
        public async Task<List<MapLocation>> FindGecodeAsync(string PositionName, Geopoint Point)
        {
            BasicGeoposition queryhint = new BasicGeoposition()
            {
                Latitude = Point.Position.Latitude,
                Longitude = Point.Position.Longitude,
                Altitude = Point.Position.Altitude
            };

            Geopoint hintPoint = new Geopoint(queryhint);
            // Geocode the specified address, using the specified reference point
            // as a query hint. Return no more than 5 results.
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(PositionName, hintPoint, 5);
            if(result.Status== MapLocationFinderStatus.Success)
            {
                return result.Locations.ToList();
            }
            else
            {
                await new MessageDialog("There is an error, no result return").ShowAsync();
                return null;
            }
        }

        public void AddMapIcon(Geopoint geopoint, MapControl mapControl,string title)
        {
            MapIcon LocationIcon = new MapIcon()
            {
                Location = geopoint,
                CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,
                //Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icon.png")),
                Title = title,
                NormalizedAnchorPoint = new Point(0.5, 1.0)
            };
            var Icons = new List<MapElement>();
            Icons.Add(LocationIcon);
            MapElementsLayer mapLayer = new MapElementsLayer()
            {
                ZIndex = 1,
                MapElements = Icons,
            };
            mapLayer.MapElementPointerEntered += MapLayer_MapElementPointerEntered;
            mapLayer.MapElementPointerExited += MapLayer_MapElementPointerExited;
            mapControl.Layers.Add(mapLayer);
            mapControl.Center = geopoint;
            mapControl.ZoomLevel = 16;
        }

        private void MapLayer_MapElementPointerExited(MapElementsLayer sender, MapElementsLayerPointerExitedEventArgs args)
        {
            var mapIcon = args.MapElement as MapIcon;
            mapIcon.Title = "I am here";
        }

        private async void GetRoutesAsync(Geopoint startPoint, Geopoint endPoint)
        {
            
        }

        private void MapLayer_MapElementPointerEntered(MapElementsLayer sender, MapElementsLayerPointerEnteredEventArgs args)
        {
            var mapIcon= args.MapElement as MapIcon;
            mapIcon.Title = "You point here";
            //mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Icon.png"));
        }
    }
}
