using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Phonebook
{
    public static class DocumentDBManager<T>
    {
        //Use the Database if it exists, if not create a new Database
        private static Database ReadOrCreateDB()
        {
            var db = Client.CreateDatabaseQuery().Where(d => d.Id == DBId).AsEnumerable().FirstOrDefault();

            if (db.Equals(null))
            {
                db = Client.CreateDatabaseAsync(new Database { Id = DBId }).Result;
            }

            return db;
        }

        //Use the DocumentCollection if it exists, if not create a new Collection
        private static DocumentCollection ReadOrCreateCollection(string dbLink)
        {
            var col = Client.CreateDocumentCollectionQuery(dbLink).Where(c => c.Id == CollectionId).AsEnumerable()
                              .FirstOrDefault();

            if (col.Equals(null))
            {
                var collectionSpec = new DocumentCollection { Id = CollectionId };
                var requestOptions = new RequestOptions { OfferType = "S1" };

                col = Client.CreateDocumentCollectionAsync(dbLink, collectionSpec, requestOptions).Result;
            }

            return col;
        }

        //Expose the "db" value from configuration as a property for internal use
        private static string dbId;
        private static String DBId
        {
            get
            {
                if (string.IsNullOrEmpty(dbId))
                {
                    dbId = ConfigurationManager.AppSettings["database"];
                }

                return dbId;
            }
        }

        //Expose the "collection" value from configuration as a property for internal use
        private static string collectionId;
        private static String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(collectionId))
                {
                    collectionId = ConfigurationManager.AppSettings["collection"];
                }

                return collectionId;
            }
        }

        //Use the ReadOrCreateDatabase function to get a reference to the database.
        private static Database database;
        private static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = ReadOrCreateDB();
                }

                return database;
            }
        }

        //Use the ReadOrCreateCollection function to get a reference to the collection.
        private static DocumentCollection collection;
        private static DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return collection;
            }
        }

        //This property establishes a new connection to DocumentDB the first time it is used, 
        //and then reuses this instance for the duration of the application avoiding the
        //overhead of instantiating a new instance of DocumentClient with each request
        private static DocumentClient client;
        private static DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                }

                return client;
            }
        }
        // Get All students from DB
        public static IEnumerable<T> GetAllStudents()
        {
            return Client.CreateDocumentQuery<T>(Collection.DocumentsLink).AsEnumerable();
        }
        // CreateStudent
        public static async Task<Document> CreateStudentAsync(T student)
        {
            return await Client.CreateDocumentAsync(Collection.SelfLink, student);
        }
        //Get a student from a database on the condition
        public static T GetStudent(Expression<Func<T, bool>> predicate)
        {
            return Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                        .Where(predicate)
                        .AsEnumerable()
                        .FirstOrDefault();
        }

        private static Document GetDocumentFromCollectionDB(string id)
        {
            return Client.CreateDocumentQuery(Collection.DocumentsLink)
                .Where(s => s.Id == id)
                .AsEnumerable()
                .FirstOrDefault();
        }
        //UpdateStudent
        public static async Task<Document> UpdateStudentAsync(string id, T student)
        {
            Document doc = GetDocumentFromCollectionDB(id);
            return await Client.ReplaceDocumentAsync(doc.SelfLink, student);
        }
        //DeleteStudent
        public static async Task DeleteStudentAsync(string id)
        {
            Document doc = GetDocumentFromCollectionDB(id);
            await client.DeleteDocumentAsync(doc.SelfLink);
        }
    }
}