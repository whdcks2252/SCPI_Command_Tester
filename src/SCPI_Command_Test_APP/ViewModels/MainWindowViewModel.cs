using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using SCPI_Command_Test_APP.Models;
using SCPI_Command_Test_APP.SGConnections;
using System.Collections.ObjectModel;
using System.Windows;

namespace SCPI_Command_Test_APP
{
    public partial class MainWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private string iPTxt;
        [ObservableProperty]
        private ObservableCollection<SGType> _sGTypes;
        [ObservableProperty]
        private SGType _sgTypeSelected;

        [ObservableProperty]
        private bool connectionStatus;

        [ObservableProperty]
        private string commandTxt;

        [ObservableProperty]
        private string frequencyTxt;
        [ObservableProperty]
        private string amplitudeTxt;

        [ObservableProperty]
        private ObservableCollection<DataLogModel> logList;

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task Connection(CancellationToken token)
        {
            try
            {
                if (ConnectionStatus)
                {

                    SG_Domain sG_Domain = SG_Domain.InitInstance(IPTxt, SgTypeSelected);

                    iSGConnection = CreateSGConnecter(sG_Domain);

                    await iSGConnection.SGConnect(token);

                    return;
                }

                if (iSGConnection is null) return;

                iSGConnection.Dispose();
            }
            catch (Exception e)
            {
                LogMarker.Info(e.Message);
            }
        }

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task Send(CancellationToken token)
        {
            if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
            if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }
            if (CommandTxt == null || CommandTxt == "") { MessageBox.Show("잘못된 command"); return; }

            LogMarker.Info("Command  전송  " + CommandTxt);
            await iSGConnection.SendCommand(CommandTxt, token);
            LogMarker.Info("Command  완료");

        }

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task FrequencyChange(CancellationToken token)
        {
            try
            {
                if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
                if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }
                if (FrequencyTxt == null || FrequencyTxt == "") { MessageBox.Show("잘못된 Frequency"); return; }

                LogMarker.Info("Frequency 변경  전송  " + FrequencyTxt);
                await iSGConnection.ChangeFrequency((long)(decimal.Parse(FrequencyTxt) * 1000000), token);
                LogMarker.Info("Frequency  완료");
            }
            catch (Exception)
            {
                if (CommandTxt == null || CommandTxt == "") { MessageBox.Show("잘못된 Frequency"); return; }

            }

        }
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task AmplitudeChange(CancellationToken token)
        {
            try
            {
                if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
                if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }
                if (AmplitudeTxt == null || AmplitudeTxt == "") { MessageBox.Show("잘못된 Amplitude"); return; }

                LogMarker.Info("Amplitude 변경  전송  " + AmplitudeTxt);
                await iSGConnection.ChangeAmplitude(decimal.Parse(AmplitudeTxt), token);
                LogMarker.Info("Amplitude  완료");
            }
            catch (Exception)
            {
                if (CommandTxt == null || CommandTxt == "") { MessageBox.Show("잘못된 Amplitude"); return; }

            }


        }
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task RFChange(object o, CancellationToken token)
        {

            if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
            if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }

            bool flag = (bool)o;
            if (flag)
            {
                LogMarker.Info("RF On  전송  ");
                await iSGConnection.ChangeRF(flag, token);
                LogMarker.Info("RF On  완료");
            }
            if (!flag)
            {
                LogMarker.Info("RF Off  전송  ");
                await iSGConnection.ChangeRF(flag, token);
                LogMarker.Info("RF Off  완료");
            }
        }
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task ModChange(object o, CancellationToken token)
        {
            if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
            if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }
            bool flag = (bool)o;
            if (flag)
            {
                LogMarker.Info("Mod On  전송  ");
                await iSGConnection.ChangeMod(flag, token);
                LogMarker.Info("Mod On  완료");
            }
            if (!flag)
            {
                LogMarker.Info("Mod Off  전송  ");
                await iSGConnection.ChangeMod(flag, token);
                LogMarker.Info("Mod Off  완료");
            }
        }
        [RelayCommand(IncludeCancelCommand = true)]
        private async Task ALCChange(object o, CancellationToken token)
        {
            if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
            if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }
            bool flag = (bool)o;
            if (flag)
            {
                LogMarker.Info("ALC On  전송  ");
                await iSGConnection.ChangeALC(flag, token);
                LogMarker.Info("ALC On  완료");
            }
            if (!flag)
            {
                LogMarker.Info("ALC Off  전송  ");
                await iSGConnection.ChangeALC(flag, token);
                LogMarker.Info("ALC Off  완료");
            }
        }

        [RelayCommand(IncludeCancelCommand = true)]
        private async Task ArbChange(object o, CancellationToken token)
        {
            if (iSGConnection is null) { MessageBox.Show("SG 미연결"); return; }
            if (!iSGConnection.IsConnect()) { MessageBox.Show("SG 미연결"); return; }
            bool flag = (bool)o;
            if (flag)
            {
                LogMarker.Info("Arb On  전송  ");
                await iSGConnection.ChangeArb(flag, token);
                LogMarker.Info("Arb On  완료");
            }
            if (!flag)
            {
                LogMarker.Info("Arb Off  전송  ");
                await iSGConnection.ChangeArb(flag, token);
                LogMarker.Info("Arb Off  완료");
            }
        }

        private ISGConnection iSGConnection;

        public MainWindowViewModel()
        {
            Init();
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>, string>(this, "LogMessage", LogMessage);

        }

        private void LogMessage(object recipient, ValueChangedMessage<string> message)
        {
            DateTime currentTime = DateTime.Now;

            // 다양한 형식으로 문자열로 변환
            string formattedTime1 = currentTime.ToString("yyyy-MM-dd HH:mm:ss"); // "2024-08-30 15:45:32" 형식
            LogList.Add(new DataLogModel() { TimeLog = formattedTime1, DataLog = message.Value });
        }

        private void Init()
        {
            SGTypes = new ObservableCollection<SGType>();
            SGTypes.Add(SGType.KeySight);
            SGTypes.Add(SGType.Rodhe);
            SgTypeSelected = SGTypes[1];

            IPTxt = "192.168.123.";


            LogList = new ObservableCollection<DataLogModel>();
        }


        private ISGConnection CreateSGConnecter(SG_Domain sG_Domain)
        {
            //if (sG_Domain.GetSGType() is SGType.KeySight) ;
            if (sG_Domain.GetSGType() is SGType.Rodhe) return new RodheConnection(sG_Domain);

            throw new NotImplementedException();

        }

    }
}
