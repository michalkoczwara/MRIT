namespace Mongoloid
{
    internal class ShodanMongoHost
    {
        public string version { get; set; }

        public int port { get; set; }

        public string[] hostnames { get; set; }

        public string[] domains { get; set; }

        public string org { get; set; }

        public string ip_str { get; set; }

        public string os { get; set; }

        public HostLocation location { get; set; }

        public string timestamp { get; set; }

        public string data { get; set; }

        public ShodanMongoHost()
        {
            location = new HostLocation();
        }
    }
}
