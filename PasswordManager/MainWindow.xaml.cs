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
using System.Security.Cryptography;

namespace PasswordManager
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String path;
        SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
        public String usha;
        public String psha;

        public MainWindow()
        {
            InitializeComponent();
            refreshPath();
            Directory.CreateDirectory(path);
        }

        private void btRegistro_Click(object sender, RoutedEventArgs e)
        {
            refreshPath();
            path = System.IO.Path.Combine(path, SHA256Encrypt(tbUsuarioR.Text) + ".txt");
            if (File.Exists(path))
                MessageBox.Show("El usuario ya existe");
            else if(pw1.Password == "" || pw2.Password == "")
                MessageBox.Show("Faltan datos");
            else if (pw1.Password.Equals(pw2.Password))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(DateTime.Now.ToString());
                    tw.WriteLine(SHA256Encrypt(pw1.Password));
                    tw.Close();
                }
                MessageBox.Show("Registro exitoso");
                pw1.Clear();
                pw2.Clear();
                tbUsuarioR.Clear();

            }
            else
                MessageBox.Show("Las contraseñas no coinciden");
        }

        public string SHA256Encrypt(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = provider.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            usha = SHA256Encrypt(tbuser.Text);
            pathFileSHA(usha);
            if (File.Exists(path))
            {
                psha = File.ReadLines(path).Skip(1).Take(1).First().ToString();
                if (SHA256Encrypt(tbpw.Password).Equals(psha))
                {
                    Usuario u = new Usuario();
                    u.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Contraseña incorrecta");
            }
            else
            {
                MessageBox.Show("No registrado");
            }
        }

        private void refreshPath()
        {
            path = @"C:\Users\" + Environment.UserName + "\\AppData\\Roaming\\PasswordManager"; // Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        private void pathFileSHA(string s)
        {
            refreshPath();
            path = System.IO.Path.Combine(path, s + ".txt");
        }
    }
}
