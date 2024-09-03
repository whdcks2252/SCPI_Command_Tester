using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe.Settings
{
    public class RArb_OnOff : ISGCommand
    {
        private static string ArbOnCommand { get; } = "SOURce1:BB:ARBitrary:STATe ON";
        private static string ArbOFFCommand { get; } = "SOURce1:BB:ARBitrary:STATe OFF";

        private string Command { get; }

        private RArb_OnOff(string _command)
        {
            Command = _command;
        }

        public static RArb_OnOff SetArbOn()
        {
            return new RArb_OnOff(ArbOnCommand);
        }
        public static RArb_OnOff SetArbOff()
        {
            return new RArb_OnOff(ArbOFFCommand);
        }
        public string GetMessage()
        {
            return Command;
        }
    }
}
