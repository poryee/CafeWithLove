using CafeWithLove.Models;

namespace CafeWithLove.DAL
{
    public class CarParkGateway : DataGateway<CarPark>
    {
        //initialise search and store into our data within this class
        public CarParkGateway()
        {
            this.data = db.Set<CarPark>();
        }
    }
}