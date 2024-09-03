using SCPI_Command_Test_APP.Models;
using SCPI_Command_Test_APP.Rodhe;
using SCPI_Command_Test_APP.Rodhe.Settings;
using System.Windows;

namespace SCPI_Command_Test_APP.SGConnections
{
    public class RodheConnection : SGConnectionBase, ISGConnection
    {
        public RodheConnection(SG_Domain sG_Domain) : base(sG_Domain)
        {
        }

        public async Task SGInitSetting(CancellationToken token)
        {


            LogMarker.Info("SG Arb On");
            await connection.SendMessage(RArb_OnOff.SetArbOn(), token);

            LogMarker.Info("SG ALC OFF");
            await connection.SendMessage(RAmplitudeALC_OnOff.SetALCOff(), token);

            LogMarker.Info("SG RF On");
            await connection.SendMessage(RRF_OnOff.SetRFOn(), token);

            LogMarker.Info("SG Mod On");
            await connection.SendMessage(RMod_OnOff.SetModOff(), token);
        }

        public async Task ChangeFrequency(long frequency, CancellationToken token)
        {
            LogMarker.Info("SG Change Frequency : " + frequency + " Hz");
            await connection.SendMessage(RFrequency_GetSet.SetFrequencyHz(frequency), token);
            await Task.Delay(100, token);

            int count = 0;

            while (true)
            {
                await connection.SendMessage(RFrequency_GetSet.GetFrequency(), token);

                await Task.Delay(100, token);

                string data = await connection.ReceiveMessageAsync(token);
                LogMarker.Info("Get SG Frequency : " + double.Parse(data));

                if ((long)(double.Parse(data)) == frequency)
                {
                    return;
                }

                if (count > 5)
                {
                    MessageBox.Show("횟수 초과 잘못된 Freqency로 변경 ");
                    return;
                }

                count++;

            }


            throw new Exception("SG Frequency 변경 오류");

        }

        public async Task ChangeAmplitude(decimal amplitude, CancellationToken token)
        {
            LogMarker.Info("SG Change Amplitude : " + amplitude + " dBm");
            await connection.SendMessage(RAmplitude_GetSet.SetAmplitude_dBm(amplitude), token);
            await Task.Delay(100, token);


            int count = 0;

            while (true)
            {
                await connection.SendMessage(RAmplitude_GetSet.GetAmplitude(), token);

                await Task.Delay(100, token);

                string data = await connection.ReceiveMessageAsync(token);
                LogMarker.Info("Get SG Amplitude : " + double.Parse(data));
                if ((decimal)double.Parse(data) == amplitude)
                {
                    return;
                }

                if (count > 5)
                {
                    MessageBox.Show("횟수 초과 잘못된 Amplitude로 변경 ");
                    return;
                }

                count++;
            }

            throw new Exception("SG Amplitude 변경 오류");
        }

        public async Task SendCommand(string command, CancellationToken token)
        {
            await connection.SendMessage(command, token);

        }

        public async Task ChangeRF(bool onoff, CancellationToken token)
        {
            if (onoff)
            {
                await connection.SendMessage(RRF_OnOff.SetRFOn(), token);
                return;
            }
            if (!onoff)
            {
                await connection.SendMessage(RRF_OnOff.SetRFOff(), token);
                return;
            }

        }

        public async Task ChangeMod(bool onoff, CancellationToken token)
        {
            if (onoff)
            {
                await connection.SendMessage(RMod_OnOff.SetModOn(), token);
                return;
            }
            if (!onoff)
            {
                await connection.SendMessage(RMod_OnOff.SetModOff(), token);
                return;
            }
        }

        public async Task ChangeALC(bool onoff, CancellationToken token)
        {
            if (onoff)
            {
                await connection.SendMessage(RAmplitudeALC_OnOff.SetALCOn(), token);
                return;
            }
            if (!onoff)
            {
                await connection.SendMessage(RAmplitudeALC_OnOff.SetALCOff(), token);
                return;
            }
        }

        public async Task ChangeArb(bool onoff, CancellationToken token)
        {
            if (onoff)
            {
                await connection.SendMessage(RArb_OnOff.SetArbOn(), token);
                return;
            }
            if (!onoff)
            {
                await connection.SendMessage(RArb_OnOff.SetArbOff(), token);
                return;
            }
        }
    }
}
