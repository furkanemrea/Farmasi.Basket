using Farmasi.Basket.Data.Abstraction;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace Farmasi.Basket.Data.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private string _connectionString = string.Empty;
        public MongoContext(IConfiguration configuration)
        {
            _commands = new List<Func<Task>>();
            _connectionString = configuration["Mongo:ConnectionString"].ToString();
        }


        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(_connectionString);

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