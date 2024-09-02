using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe
{
    public class RAmplitude_GetSet : ISGCommand
    {
        private static string ChangeAmplitudeCommand { get; } = ":POWer:LEVEL ";
        private static string ReceiveAmplitudeCommand { get; } = ":POWer:LEVEL?";

        private string Command { get; }

        private RAmplitude_GetSet(string _commnd)
        {
            Command = _commnd;
        }


        public static RAmplitude_GetSet SetAmplitude_dBm(decimal Amplitude)
        {
            return new RAmplitude_GetSet(ChangeAmplitudeCommand + Amplitude + " " + CommonUnit.dBm);
        }

        public static RAmplitude_GetSet GetAmplitude()
        {
            return new RAmplitude_GetSet(ReceiveAmplitudeCommand);
        }

        public string GetMessage()
        {
            return Command;
        }
    }
}
