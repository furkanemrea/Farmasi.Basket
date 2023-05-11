using Farmasi.Basket.Data.Abstraction;
using MongoDB.Driver;

namespace Farmasi.Basket.Data.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;

        public MongoContext()
        {
            _commands = new List<Func<Task>>();
        }


        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            // Configure mongo (You can inject the config, just to simplify)
            MongoClient = new MongoClient("mongodb+srv://furkan-emre-a:lUKVDTlJEcOFnE0K@basket.bz5unkj.mongodb.net/?retryWrites=true&w=majority");

            Database = MongoClient.GetDatabase("farmasi_basket");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

 
    }
}