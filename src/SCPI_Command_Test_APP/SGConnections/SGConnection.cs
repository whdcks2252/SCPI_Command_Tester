using SCPI_Command_Test_APP.CommandUtil;
using TCP_Lib;

namespace SCPI_Command_Test_APP.SGConnections
{
    public class SGConnection
    {
        private SG_TCP sg;


        public async Task SGConnect(string _ip, int _port, CancellationToken token)
        {

            if (sg is not null) sg.Dispose();

            sg = SG_TCP.InitInstance(_ip, _port);

            bool connectResult = await sg.TCPConnect(token);

            if (connectResult) { LogMarker.Info("SG 연결 성공"); }

            if (connectResult is false) throw new Exception("SG 연결 실패");
        }

        public async Task SendMessage(ISGCommand command, CancellationToken token)
        {
            await sg.SendMessage(command.GetMessage(), token);
        }

        public async Task SendMessage(string command, CancellationToken token)
        {
            await sg.SendMessage(command, token);
        }

        public async Task<string> ReceiveMessageAsync(CancellationToken token)
        {
            return await sg.ReceiveLineAsync(token);
        }

        public bool IsConnect()
        {
            if (sg is null) return false;

            return sg.IsConnect();
        }

        public void Dispose()
        {
            if (sg is null) return;

            sg.Dispose();

            LogMarker.Info("SG 연결 종료");
        }

    }
}
