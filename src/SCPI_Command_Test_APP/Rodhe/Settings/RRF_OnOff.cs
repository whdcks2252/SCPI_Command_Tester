using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe.Settings
{
    internal class RRF_OnOff : ISGCommand
    {
        private static string RFOffCommand { get; } = ":OUTPut OFF";
        private static string RFOnCommand { get; } = ":OUTPut ON";

        private string Command { get; }

        private RRF_OnOff(string _command)
        {
            Command = _command;
        }

        public static RRF_OnOff SetRFOff()
        {
            return new RRF_OnOff(RFOffCommand);
        }

        public static RRF_OnOff SetRFOn()
        {
            return new RRF_OnOff(RFOnCommand);
        }

        public string GetMessage()
        {
            return Command;
        }
    }
}
