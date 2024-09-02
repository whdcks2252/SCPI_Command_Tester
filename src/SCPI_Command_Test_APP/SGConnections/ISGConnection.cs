namespace SCPI_Command_Test_APP.SGConnections
{
    public interface ISGConnection
    {
        public Task SGConnect(CancellationToken token);
        public bool IsConnect();
        public void Dispose();
        public string GetSGType();

        public Task SGInitSetting(CancellationToken token);
        public Task ChangeFrequency(long frequency, CancellationToken token);
        public Task ChangeAmplitude(decimal amplitude, CancellationToken token);
        public Task ChangeRF(bool onoff, CancellationToken token);
        public Task ChangeMod(bool onoff, CancellationToken token);
        public Task ChangeALC(bool onoff, CancellationToken token);
        public Task ChangeAttenHold(bool onoff, CancellationToken token);
        public Task ChangeArb(bool onoff, CancellationToken token);


        public Task SendCommand(string command, CancellationToken token);

    }
}
