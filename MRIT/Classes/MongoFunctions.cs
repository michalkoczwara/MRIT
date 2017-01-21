using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MRIT
{
    internal class MongoFunctions
    {
        /// <summary>
        /// Get the details of a MongoDB ransom demand
        /// </summary>
        /// <param name="ip">The MongoDB server IP</param>
        /// <param name="port">The listening port for MongoDB</param>
        /// <param name="ransomSchemas">A list of known database schemas for ransom demands</param>
        /// <returns></returns>
        public static List<RansomDemand> GetRansomDemands(string ip, int port, List<RansomSchema> ransomSchemas)
        {
            var output = new List<RansomDemand>();
            try
            {
                var mongoConnection = new MongoClient(new MongoClientSettings { Server = new MongoServerAddress(ip, port) });
                foreach (var schema in ransomSchemas)
                {
                    try
                    {
                        var newRansomDemand = new RansomDemand { Ip = ip, Port = port, DatabaseName = schema.DatabaseName, BitcoinWallet = string.Empty, Email = string.Empty, RansomText = string.Empty };
                        var database = mongoConnection.GetDatabase(schema.DatabaseName);
                        var collections = database.GetCollection<BsonDocument>(schema.CollectionName);
                        var cursor = collections.Find(new BsonDocument());
                        var doc = cursor.FirstOrDefault();
                        foreach (var element in doc.Elements)
                        {
                            if (!schema.BitcoinField.Equals("NONE") && element.Name.ToLower().Equals(schema.BitcoinField.ToLower()))
                                newRansomDemand.BitcoinWallet = element.Value.ToString();
                            if (!schema.EmailField.Equals("NONE") && element.Name.ToLower().Equals(schema.EmailField.ToLower()))
                                newRansomDemand.Email = element.Value.ToString();
                            if (!schema.NotesField.Equals("NONE") && element.Name.ToLower().Equals(schema.NotesField.ToLower()))
                                newRansomDemand.RansomText = element.Value.ToString();
                        }
                        if (string.IsNullOrEmpty(newRansomDemand.BitcoinWallet) && string.IsNullOrEmpty(newRansomDemand.RansomText) && string.IsNullOrEmpty(newRansomDemand.Email))
                            continue;
                        output.Add(newRansomDemand);
                    }
                    catch
                    {
                        //Do nothing, the host does not contain this database or collection.
                    }
                }
            }
            catch
            {
                //Do nothing, the host is now secured or is no longer alive.
            }
            return output;
        }
    }
}
