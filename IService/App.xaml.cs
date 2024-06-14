using System.Configuration;
using System.Data;
using System.Windows;
using System;
using System.IO;
using IService.Models;
using MQTT;

namespace IService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() 
        {
            int count = 0;
            string dir = Environment.CurrentDirectory;
            while (count < 3) { dir = Directory.GetParent(dir).FullName; count++; }
            DB.InitMonitoringDatabase(dir + "/DataBase");
            DB.InitManagerDatabase(dir + "/DataBase");
        }
    }

}
