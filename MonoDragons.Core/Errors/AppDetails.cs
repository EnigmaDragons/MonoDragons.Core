namespace MonoDragons.Core.Errors
{
    public class AppDetails
    {
        public string Name { get; }
        public string Version { get; }
        public string OS { get; }

        public AppDetails(string name, string version, string os)
        {
            Name = name;
            Version = version;
            OS = os;
        }
    }
}
