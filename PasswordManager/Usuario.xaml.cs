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
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Lógica de interacción para Usuario.xaml
    /// </summary>
    public partial class Usuario : Window
    {
        AES ads = new AES();
        static String pass = ((MainWindow)Application.Current.MainWindow).psha;
        static String path = ((MainWindow)Application.Current.MainWindow).path;
        int lineCount;
        String plain, crypto, all = "";
        Byte[] key = Encoding.UTF8.GetBytes(pass.Substring(0, 16)), coded;

        public Usuario()
        {
            InitializeComponent();

        }

        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void btConsulta_Click(object sender, RoutedEventArgs e)
        {
            //String res;
            //lineCount = File.ReadLines(path).Count();
            //if (lineCount == 2)
            //    res = "No hay contraseñas";
            //else
            //{
            //    for (int i = 2; i < lineCount; i++)
            //    {
            //        String p = File.ReadLines(path).Skip(i).First();
            //        MessageBox.Show(p);
            //        coded = System.Text.Encoding.ASCII.GetBytes(p);
            //        //all = all + ads.DecryptStringFromBytes_Aes(coded, key);
            //    }
            //}
            String p = File.ReadLines(path).Skip(2).Take(1).First().ToString();
            MessageBox.Show(p);

            coded = System.Text.Encoding.UTF8.GetBytes(p);
            printA(coded);
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            plain = "";
            crypto = "";
            if (pw1.Password.Equals(pw2.Password))
            {
                plain = tbUsuario.Text + ":" + pw1.Password;
                //MessageBox.Show(plain);
                coded = ads.EncryptStringToBytes_Aes(plain, key);
                crypto = printA(coded);
                //MessageBox.Show(crypto);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(crypto);
                    tw.Close();
                }
            }
            else
                MessageBox.Show("Las contraseñas no coinciden");
        }

        private void getAllPasswords()
        {
            all = "";
            lineCount = File.ReadLines(path).Count();
            for(int i = 3; i < lineCount; i++)
            {
                all = all + File.ReadLines(path).Skip(i).First() + "\n";
            }
            MessageBox.Show(all);
        }


        public String printA(Byte[] a)
        {
            String res = "";
            for(int i = 0; i < a.Length; i++)
            {
                res = res + a[i];
            }
            //MessageBox.Show(res);
            return res;
        }
    }
}
