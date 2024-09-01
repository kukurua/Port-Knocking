using System.Net;
using System.Net.Sockets;

internal class Program
{
    private static void Main(string[] args)
    {
        bool IPv4correct = true;
        IPAddress connectionTarget = IPAddress.Parse("0.0.0.0");
        while (IPv4correct)
        {
            Console.Write("Введите IPv4 который нужно простучать: ");
            string IPv4 = Console.ReadLine();
            if (IPAddress.TryParse(IPv4, out connectionTarget))
            {
                IPv4correct = false;
            }
            else
            {
                Console.WriteLine("IPv4 не может быть пустым или была допущена ошибка");
            }
        }
        List<int> ports = new List<int>();
        StreamReader confFile = null;
        try
        {
            confFile = new StreamReader("port.conf");
        }
        catch
        {
            Console.WriteLine("Не найден файл port.conf в папке с приложением");
            Console.ReadKey();
            Environment.Exit(0);
        }
        while (!confFile.EndOfStream)
        {
            ports.Add(int.Parse(confFile.ReadLine()));
        }
        confFile.Close();
        TcpClient tcpClient = new TcpClient();
        foreach (int port in ports)
        {
            try
            {
                Console.WriteLine($"Стучу по {connectionTarget.ToString()}:{port}");
                tcpClient.Connect(connectionTarget, port);
            }
            catch (SocketException ex)
            {
                if(ex.ErrorCode != 10061) //Важно настроить на принимающей стороне reject tcp reset иначе программа не поймёт, что она достучалась.
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine("Если вы не получили ошибки выше - то необходимые порты должны быть открыты");
        Console.ReadKey();
    }
}