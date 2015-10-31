using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;

using MciobanuTestWebApi03.Models;

namespace MciobanuTestWebApi03.Tests
{


    public class DocumentDbLink
    {
        private static string EndpointUrl = "https://mciobanu-doc01.documents.azure.com:443/";
        private static string AuthorizationKey = "iKXBiZ7FTdQmFOYKJd7Rks5NpkJsoWyoXvDz5hHmEiVzMT8At1XdIax3jzfXnf+BpuNAuJs+AWs47yWtiI4wCw==";

        DocumentClient client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

        public static async Task<Database> GetDatabase(DocumentClient client, string databaseName)
        {
            if (client.CreateDatabaseQuery().Where(db => db.Id == databaseName).AsEnumerable().Any())
            {
                return client.CreateDatabaseQuery().Where(db => db.Id == databaseName).AsEnumerable().FirstOrDefault();
            }
            return await client.CreateDatabaseAsync(new Database { Id = databaseName });
        }

        public static async Task<DocumentCollection> GetCollection(DocumentClient client, Database database, string collName)
        {
            if (client.CreateDocumentCollectionQuery(database.SelfLink).Where(coll => coll.Id == collName).ToArray().Any())
            {
                return client.CreateDocumentCollectionQuery(database.SelfLink).Where(coll => coll.Id == collName).ToArray().FirstOrDefault();
            }
            return await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection { Id = collName });
        }

        public async Task saveResp(MathInfo1 resp)
        {
            /*
             * AccountEndpoint=https://mciobanu-doc01.documents.azure.com:443/;AccountKey=iKXBiZ7FTdQmFOYKJd7Rks5NpkJsoWyoXvDz5hHmEiVzMT8At1XdIax3jzfXnf+BpuNAuJs+AWs47yWtiI4wCw==;
             */
            Database database = await GetDatabase(client, "db1");
            DocumentCollection collection = await GetCollection(client, database, "mciobanu-doc01");
            // Create a document collection.
            /*DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                new DocumentCollection
                {
                    Id = "FamilyCollection"
                });*/
            //collection.SaveTo
            ResourceResponse<Document> r = await client.CreateDocumentAsync(collection.DocumentsLink, resp);
            int a = 9;
        }
    }
}