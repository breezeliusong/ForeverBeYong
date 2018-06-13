using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Popups;

namespace ForeverBeYoung.ViewModels
{
    public class MapHelper
    {
        public async Task<ObservableCollection<string>> FindGecodeAsync(string PositionName, Geopoint Point)
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
                ObservableCollection<string> list = new ObservableCollection<string>();
                foreach(var Location in result.Locations)
                {
                    list.Add(Location.Address.FormattedAddress);
                }
                return list;
            }
            else
            {
                await new MessageDialog("There is an error, no result return").ShowAsync();
                return null;
            }
        }
    }
}
