using System;
using System.Threading.Tasks;
using Edelstein.Network.Packets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;

namespace Edelstein.Network
{
    public abstract class Socket : IDisposable
    {
        public static readonly AttributeKey<Socket> SocketKey = AttributeKey<Socket>.ValueOf("Socket");

        public IChannel Channel;
        public uint SeqSend { get; set; }
        public uint SeqRecv { get; set; }
        public bool EncryptData { get; set; } = true;
        public string SessionKey => Channel.Id.AsLongText();

        public readonly object LockSend = new object();
        public readonly object LockRecv = new object();

        public Socket(IChannel channel, uint seqSend, uint seqRecv)
        {
            Channel = channel;
            SeqSend = seqSend;
            SeqRecv = seqRecv;
        }

        public abstract void OnPacket(InPacket packet);
        public abstract void OnDisconnect();

        public Task SendPacket(OutPacket packet) => Channel.WriteAndFlushAsync(packet);

        public void Dispose() => Channel.CloseAsync();
    }
}