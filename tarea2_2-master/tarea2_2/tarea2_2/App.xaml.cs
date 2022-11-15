using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tarea2_2.models;
using tarea2_2.controller;
using System.IO;

namespace tarea2_2
{
    public partial class App : Application
    {
        static dbfirma dbbases;

        public static dbfirma Basedb
        {
            get
            {
                if (dbbases == null)
                {
                    dbbases = new dbfirma(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FirmasDB.db3"));
                }
                return dbbases;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
