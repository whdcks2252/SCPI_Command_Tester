using System.Net;

namespace SCPI_Command_Test_APP.Models
{
   public class SG_Domain
    {
        private readonly string ip;
        private readonly SGType sgType;

        private SG_Domain(string _ip, SGType _sgType)
        {
            ip = _ip;
            sgType = _sgType;
        }


        public static SG_Domain InitInstance(string ip, SGType sGType)
        {

            if (ip is null) throw new NullReferenceException("SG IP 설정 오류");
            if (ip is "") throw new NullReferenceException("SG IP 설정 오류");
            IPAddress address;
            if (!IPAddress.TryParse(ip, out address) && address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) throw new ArgumentException("SG IP 설정 오류");
            return new SG_Domain(ip, sGType);
        }
        public string GetIp()
        {
            return ip;
        }

        public SGType GetSGType()
        {
            return sgType;
        }
    }
}
