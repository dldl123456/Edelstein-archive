using System;
using System.Linq;
using DotNetty.Transport.Channels;
using Edelstein.Common.Interop;
using Edelstein.Common.Interop.Game;
using Edelstein.Database;
using Edelstein.Database.Entities.Types;
using Edelstein.Network;
using Edelstein.Network.Packets;
using Edelstein.WvsLogin.Logging;
using Edelstein.WvsLogin.Packets;
using Lamar;

namespace Edelstein.WvsLogin.Sockets
{
    public class CenterServerSocket : Socket
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        private IContainer _container;
        private WvsLogin _wvsLogin;

        public WorldInformation WorldInformation { get; set; }

        public CenterServerSocket(IContainer container, IChannel channel, uint seqSend, uint seqRecv)
            : base(channel, seqSend, seqRecv)
        {
            _container = container;
            _wvsLogin = container.GetInstance<WvsLogin>();
        }

        public override void OnPacket(InPacket packet)
        {
            var operation = (InteropSendOperations) packet.Decode<short>();

            switch (operation)
            {
                case InteropSendOperations.ServerRegisterResult:
                    OnServerRegisterResult(packet);
                    break;
                case InteropSendOperations.ServerInformation:
                    OnServerInformation(packet);
                    break;
                case InteropSendOperations.MigrationResult:
                    OnMigrationResult(packet);
                    break;
                default:
                    Logger.Warn($"Unhandled packet operation {operation}");
                    break;
            }
        }

        public override void OnDisconnect()
        {
            throw new NotImplementedException();
        }

        private void OnServerRegisterResult(InPacket packet)
        {
            if (packet.Decode<bool>()) return; // TODO: disconnect?

            var worldInformation = new WorldInformation();

            worldInformation.Decode(packet);
            WorldInformation = worldInformation;
            Logger.Info($"Registered Center server, {worldInformation.Name}");
        }

        private void OnServerInformation(InPacket packet)
        {
            var worldInformation = new WorldInformation();

            worldInformation.Decode(packet);
            WorldInformation = worldInformation;
            Logger.Info($"Updated {worldInformation.Name} server information");
        }

        private void OnMigrationResult(InPacket packet)
        {
            var sessionKey = packet.Decode<string>();
            var client = _wvsLogin.GameServer.Sockets.Single(s => s.SessionKey == sessionKey);

            if (!packet.Decode<bool>()) return;

            using (var db = _container.GetInstance<DataContext>())
            {
                client.Account.State = AccountState.MigratingIn;
                db.Update(client.Account);
                db.SaveChanges();
            }

            using (var p = new OutPacket(LoginSendOperations.SelectCharacterResult))
            {
                var characterID = packet.Decode<int>();
                p.Encode<byte>(0);
                p.Encode<byte>(0);

                p.Encode<byte>(packet.Decode<byte>());
                p.Encode<byte>(packet.Decode<byte>());
                p.Encode<byte>(packet.Decode<byte>());
                p.Encode<byte>(packet.Decode<byte>());
                p.Encode<short>(packet.Decode<short>());

                p.Encode<int>(characterID);
                p.Encode<byte>(0);
                p.Encode<int>(0);

                client.SendPacket(p);
            }
        }
    }
}