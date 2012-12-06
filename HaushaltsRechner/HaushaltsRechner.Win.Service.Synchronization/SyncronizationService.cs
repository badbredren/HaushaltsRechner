using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;

namespace HaushaltsRechner.Win.Service.Synchronization
{
    public partial class SyncronizationService : ServiceBase
    {
        public SyncronizationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var timer = new Timer(300000);
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Ordner immer neu auslesen, damit Dienst nicht neu gestartet werden muss, falls Ordner geändert wird
                var folder = System.Configuration.ConfigurationManager.AppSettings.GetValues("SyncFolder");
                if (folder.Length == 1)
                {
                    SyncFiles(folder[0]);
                }
            }
            catch
            {
                //TODO: Fehler loggen
            }
        }

        private void SyncFiles(string folder)
        {
            var dir = new DirectoryInfo(folder);

            //alle *.json Dateien auslesen
            foreach (var file in dir.GetFiles("*.json"))
            {
                var str = File.ReadAllText(file.FullName);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
