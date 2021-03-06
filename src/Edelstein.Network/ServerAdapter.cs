using System;
using DotNetty.Transport.Channels;
using Edelstein.Network.Crypto;
using Edelstein.Network.Logging;
using Edelstein.Network.Packets;

namespace Edelstein.Network
{
    public class ServerAdapter<T> : ChannelHandlerAdapter
        where T : Socket
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        private readonly Server<T> _server;
        private readonly ISocketFactory<T> _socketFactory;

        public ServerAdapter(
            Server<T> server,
            ISocketFactory<T> socketFactory
        )
        {
            _server = server;
            _socketFactory = socketFactory;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            var random = new Random();
            var socket = _socketFactory.Build(
                context.Channel,
                (uint) random.Next(),
                (uint) random.Next()
            );

            using (var p = new OutPacket())
            {
                p.Encode<short>(AESCipher.Version);
                p.Encode<string>("1");
                p.Encode<int>((int) socket.SeqRecv);
                p.Encode<int>((int) socket.SeqSend);
                p.Encode<byte>(8);
                
                socket.SendPacket(p);
            }

            context.Channel.GetAttribute(Socket.SocketKey).Set(socket);
            _server.Sockets.Add(socket);

            Logger.Debug($"Accepted connection from {context.Channel.RemoteAddress}");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            var socket = (T) context.Channel.GetAttribute(Socket.SocketKey).Get();

            socket?.OnDisconnect();
            _server.Sockets.Remove(socket);
            base.ChannelInactive(context);

            Logger.Debug($"Released connection from {context.Channel.RemoteAddress}");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var socket = (T) context.Channel.GetAttribute(Socket.SocketKey).Get();
            var p = (InPacket) message;

            socket?.OnPacket(p);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Error(exception.ToString);
        }
    }
}