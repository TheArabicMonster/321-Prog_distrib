using System;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ntpServer = "0.ch.pool.ntp.org";

            byte[] ntpData = new byte[48];
            ntpData[0] = 0x1B;

            IPEndPoint ntpEndpoint = new IPEndPoint(Dns.GetHostAddresses(ntpServer)[0], 123);

            UdpClient ntpClient = new UdpClient();
            ntpClient.Connect(ntpEndpoint);

            ntpClient.Send(ntpData, ntpData.Length);

            ntpData = ntpClient.Receive(ref ntpEndpoint);
            DateTime ntpTime = ToDateTime(ref ntpData);

            ntpClient.Close();

            Console.WriteLine("Heure atm : " + ntpTime.ToString());


            Console.WriteLine("Heure actuelle (format personnalisé) : " + ntpTime.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine("- " + ntpTime.ToString("D"));
            Console.WriteLine("- " + ntpTime.ToString("g"));
            Console.WriteLine("- " + ntpTime.ToString("d"));
        }
        public static DateTime ToDateTime(ref byte[] ntpData)
        {
            ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
            ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
            var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);
            return networkDateTime;
        }
    }
}