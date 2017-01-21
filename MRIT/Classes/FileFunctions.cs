using CsvHelper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MRIT
{
    /// <summary>
    /// Used for reading the Shodan export to CSV.
    /// </summary>
    internal class FileFunctions
    {
        /// <summary>
        /// Get all Shodan records from a CSV export.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<ShodanRecord> GetAllRecordsFromCsvFile(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            using (var csvReader = new CsvReader(streamReader))
                return csvReader.GetRecords<ShodanRecord>().ToList();
        }

        /// <summary>
        /// Get all Shodan records from a JSON export.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<ShodanRecord> GetAllRecordsFromJsonFile(string fileName)
        {
            var hosts = new List<ShodanMongoHost>();
            using (var streamReader = new StreamReader(fileName))
            using (var reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    try
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                            hosts.Add(serializer.Deserialize<ShodanMongoHost>(reader));
                    }
                    catch
                    {
                        //Error serializing record.  Skip it and move on.
                    }
                }
            }
            return hosts.Select(host => new ShodanRecord
            {
                Hostnames = string.Join(",", host.hostnames),
                Ip = host.ip_str,
                Port = host.port,
                Organization = host.org ?? string.Empty,
                OperatingSystem = host.os ?? string.Empty,
                Timestamp = host.timestamp ?? string.Empty,
                Banner = host.data ?? string.Empty,
                City = host.location.city ?? string.Empty,
                Country = host.location.country_name ?? string.Empty
            }).ToList();
        }
    }
}
