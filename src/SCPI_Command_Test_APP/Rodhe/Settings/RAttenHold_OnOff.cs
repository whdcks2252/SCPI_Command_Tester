using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe.Settings
{
    public class RAttenHold_OnOff : ISGCommand
    {
        private static string AttenHoldOnCommand { get; } = ":POWer:ATTenuation:AUTO ON";
        private static string AttenHoldOffCommand { get; } = ":POWer:ATTenuation:AUTO OFF";


        private string Command { get; }

        private RAttenHold_OnOff(string _command)
        {
            Command = _command;
        }

        public static RAttenHold_OnOff SetAttenHoldOn()
        {
            return new RAttenHold_OnOff(AttenHoldOnCommand);
        }
        public static RAttenHold_OnOff SetAttenHoldOff()
        {
            return new RAttenHold_OnOff(AttenHoldOffCommand);
        }
        public string GetMessage()
        {
            return Command;
        }
    }
}
