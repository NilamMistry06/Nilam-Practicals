namespace Practical_Nilam
{
    public class Ride
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<RideDetails> listings { get; set; }

    }

    public class RideDetails
    {
        public string name { get;set; }
        public decimal pricePerPassenger { get; set; }
        public VehicleType vehicleType { get; set; }
        public decimal totalPrice { get; set; }
    }
    public class VehicleType
    {
        public string name { get; set; }
        public int maxPassengers { get; set; }

    }
}
