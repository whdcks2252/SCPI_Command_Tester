using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe.Settings
{
    public class RAmplitudeALC_OnOff : ISGCommand
    {
        private static string ALCOffCommand { get; } = ":POWer:ALC OFF";
        private static string ALCONCommand { get; } = ":POWer:ALC ON";


        private string Command { get; }

        private RAmplitudeALC_OnOff(string _command)
        {
            Command = _command;
        }

        public static RAmplitudeALC_OnOff SetALCOff()
        {
            return new RAmplitudeALC_OnOff(ALCOffCommand);
        }
        public static RAmplitudeALC_OnOff SetALCOn()
        {
            return new RAmplitudeALC_OnOff(ALCONCommand);
        }

        public string GetMessage()
        {
            return Command;
        }
    }
}
