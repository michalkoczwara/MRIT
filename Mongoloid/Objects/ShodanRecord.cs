namespace Mongoloid
{
    public class ShodanRecord
    {
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Banner { get; set; }
        public string Timestamp { get; set; }
        public string Hostnames { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string OperatingSystem { get; set; }
        public string Organization { get; set; }


        public ShodanRecord() { }

        public ShodanRecord(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }
    }
}
