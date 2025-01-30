namespace d3e.core
{
    public class Geolocation
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public Geolocation(double Latitude, double Longitude)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        public double DistanceTo(Geolocation other)
        {
            if (other != null)
            {
                double earthRadius = 637100;
                double lat = (other.Latitude - Latitude) * (3.141592653589793 / 180);
                double lon = (other.Longitude - Longitude) * (3.141592653589793 / 180);
                double a = Math.Sin(lat / 2) * Math.Sin(lat / 2) + Math.Cos(Latitude * (3.141592653589793 / 180))
                    * Math.Cos(other.Latitude * (3.141592653589793 / 180)) * Math.Sin(lon / 2) * Math.Sin(lon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double distance = earthRadius * c;
                return distance;
            }
            return 0.0;
        }
    }
}