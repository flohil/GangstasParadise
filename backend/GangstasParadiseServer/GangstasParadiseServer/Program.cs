using Grapevine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GangstasParadiseServer {
    class Program {
        static void Main(string[] args) {
            var server = new RESTServer();
            server.Start();

            while (server.IsListening) {
                Thread.Sleep(300);
            }

            // This little tidbit will keep your console open after the server
            // stops until you explicitly close it by pressing the enter key
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }
    }
}
