using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btRegistro_Click(object sender, RoutedEventArgs e)
        {
            string userName = Environment.UserName;
            string path = @"C:\Users\"+ userName + "\\AppData\\Roaming\\PasswordManager"; // Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Directory.CreateDirectory(path);
            int f = Directory.GetFiles(path).Length;
            string path2 = System.IO.Path.Combine(path, "Test.txt");
            if (!File.Exists(path2))
            {
                File.Create(path2);
                TextWriter tw = new StreamWriter(path
                    );
                tw.WriteLine("The very first line!");
                tw.Close();
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("The next line!");
                    tw.Close();
                }
            }
        }
    }
}
