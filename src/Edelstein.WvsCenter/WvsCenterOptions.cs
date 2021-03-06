using Edelstein.Network;

namespace Edelstein.WvsCenter
{
    public class WvsCenterOptions
    {
        public CenterInfo CenterInfo { get; set; }

        public ServerOptions InteropServerOptions { get; set; }
        public string ConnectionString { get; set; }
    }

    public class CenterInfo
    {
        public byte ID { get; set; }
        public string Name { get; set; }
        public byte State { get; set; }
        public string EventDesc { get; set; }
        public short EventEXP { get; set; }
        public short EventDrop { get; set; }
        public bool BlockCharCreation { get; set; }
    }
}