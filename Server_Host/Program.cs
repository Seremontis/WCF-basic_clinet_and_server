﻿using System;
using System.ServiceModel;

namespace Server_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ProjectSever.ServiceImplementations.MiniDB));
            host.Open();
            Console.WriteLine("Serwer uruchomiony");
            Console.ReadKey();
        }
    }
}
