using MongoDB.Driver;

namespace Database
{
    public class ConnectionManager
    {
        public void OpenConnection()
        {
            MongoClient client = new MongoClient();
        }
    }
}