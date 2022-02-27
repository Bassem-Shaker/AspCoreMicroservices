using Catalog.Api.Entities;
using Catalog.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {

        public CatalogContext(IOptions<CatalogDatabaseSettings> catalogDatabaseSettings)
        {
            //this.catalogDatabaseSettings = catalogDatabaseSettings;

            var client = new MongoClient(catalogDatabaseSettings.Value.ConnectionString);// configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(catalogDatabaseSettings.Value.DatabaseName);// configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(catalogDatabaseSettings.Value.ProductsCollectionName);// configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
