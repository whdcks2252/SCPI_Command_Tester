using SCPI_Command_Test_APP.Models;

namespace SCPI_Command_Test_APP.SGConnections
{
    public class SGConnectionBase
    {
        protected readonly SGConnection connection;
        protected readonly string ip;
        protected readonly int port = SGConfig.Keysight_Port;
        protected readonly SGType sgType;

        public SGConnectionBase(SG_Domain sG_Domain)
        {
            ip = sG_Domain.GetIp();
            sgType = sG_Domain.GetSGType();
            connection = new SGConnection();
        }

        public async Task SGConnect(CancellationToken token)
        {
            LogMarker.Info($"SG 연결 시도 IP : {ip} , Port : {port} , Type : {sgType.ToString()}");
            await connection.SGConnect(ip, port, token);

        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public bool IsConnect()
        {
            return connection.IsConnect();
        }

        public string GetSGType()
        {
            return sgType.ToString();
        }

        private int SetPort()
        {
            if (sgType == SGType.KeySight)
            {
                return SGConfig.Keysight_Port;
            }
            if (sgType == SGType.Rodhe)
            {
                return SGConfig.Keysight_Port;
            }
            if (sgType == SGType.Dabin)
            {
                return SGConfig.Keysight_Port;
            }
            if (sgType == SGType.Anritsu)
            {
                return SGConfig.Keysight_Port;
            }

            throw new InvalidOperationException("Invalid SG type");
        }
    }
}
