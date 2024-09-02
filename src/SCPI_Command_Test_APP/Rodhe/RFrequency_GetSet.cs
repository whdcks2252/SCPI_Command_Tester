using SCPI_Command_Test_APP.CommandUtil;

namespace SCPI_Command_Test_APP.Rodhe
{
    public class RFrequency_GetSet : ISGCommand
    {
        private static string ChangeFrequencyCommand { get; } = ":FREQ:CW ";
        private static string ReceiveFrequencyCommand { get; } = ":FREQ:CW?";

        private string Command { get; }

        private RFrequency_GetSet(string _commnd)
        {
            Command = _commnd;
        }

        public static RFrequency_GetSet SetFrequencyHz(decimal frequency)
        {
            return new RFrequency_GetSet(ChangeFrequencyCommand + frequency + " " + CommonUnit.Hz);
        }

        public static RFrequency_GetSet SetFrequencyMHz(decimal frequency)
        {
            return new RFrequency_GetSet(ChangeFrequencyCommand + frequency + " " + CommonUnit.MHz);
        }

        public static RFrequency_GetSet SetFrequencyGHz(decimal frequency)
        {
            return new RFrequency_GetSet(ChangeFrequencyCommand + frequency + " " + CommonUnit.GHz);
        }

        public static RFrequency_GetSet GetFrequency()
        {
            return new RFrequency_GetSet(ReceiveFrequencyCommand);
        }

        public string GetMessage()
        {
            return Command;
        }
    }
}
