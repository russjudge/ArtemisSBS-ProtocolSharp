using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace BigRedButtonOfDeath.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            if (this.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
            {
                OnUnhandledException(sender, e);
            }
            else
            {
                MessageBox.Show("The Big Red Button of Death! has crashed.\r\n\r\nError:\r\n\r\n" + e.Exception.Message + "\r\n\r\n\r\nStack Trace:" + e.Exception.StackTrace,
                    "The Big Red Button of Death!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static string AppDataPath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Russ Judge", "BigRedButtonOfDeath");
                //return new System.IO.FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName;
            }
        }
            
    }
}
