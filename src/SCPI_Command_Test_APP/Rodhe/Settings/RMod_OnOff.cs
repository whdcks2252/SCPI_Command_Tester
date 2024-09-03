using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe.Settings
{
    internal class RMod_OnOff : ISGCommand
    {
        private static string ModOnCommand { get; } = ":MODulation:ALL:STATe ON";
        private static string ModOffCommand { get; } = ":MODulation:ALL:STATe OFF";

        private string Command { get; }

        private RMod_OnOff(string _command)
        {
            Command = _command;
        }
        public static RMod_OnOff SetModOn()
        {
            return new RMod_OnOff(ModOnCommand);
        }
        public static RMod_OnOff SetModOff()
        {
            return new RMod_OnOff(ModOffCommand);
        }

        public string GetMessage()
        {
            return Command;
        }
    }
}
