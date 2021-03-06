using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.Collections.Generic;
using BuildingBlocks.GeoLocation;
using NetTopologySuite;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.ValueObjects
{
    [Owned]
    public class Location : ValueObject
    {
        [JsonProperty]
        public double Longitude { get; private set; }
        [JsonProperty]
        public double Latitude { get; private set; }
        [JsonProperty]
        public string Street { get; private set; }
        [JsonProperty]
        public string City { get; private set; }
        [JsonProperty]
        public string Province { get; private set; }
        [JsonProperty]
        public string PostalCode { get; private set; }
        [JsonProperty]

        [JsonIgnore]
        public Point Point
        {
            get
            {
                var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

                return geometryFactory.CreatePoint(new Coordinate(Longitude, Latitude));
            }
        }

        protected Location()
        {

        }

        public double Distance(Location location)
            => Point.ProjectTo(2855).Distance(location.Point.ProjectTo(2855)) / 1000;

        private Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
        }

        public static Result<Location> Create(double longitude, double latitude)
        {
            return CSharpFunctionalExtensions.Result.Success(new Location(longitude, latitude));
        }
    }
}
