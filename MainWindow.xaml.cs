using System;
using System.Collections.Generic;
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

using System.Diagnostics;

namespace Enigma
{
    public class Rotor
    {
        private const Int32 MAX = 3;
        public Int32 Rotor1;
        public Int32 Rotor2;
        public Int32 Rotor3;
        private Int32 _sum;
        
        public Int32 Sum
        {
            get { return _sum = Rotor1 + Rotor2 + Rotor3; }
        }

        public Int32 Max
        {
            get { return MAX; }
        }

        public void SetRotors(Int32 Rotor1, Int32 Rotor2, Int32 Rotor3)
        {
            this.Rotor1 = Rotor1;
            this.Rotor2 = Rotor2;
            this.Rotor3 = Rotor3;
        }

        public void IncrementRotors()
        {
            // Increment rotors
            if (Rotor3 + 1 > MAX)
            {
                this.Rotor3 = 1;
                this.Rotor1++;
            }
            else if (Rotor2 + 1 > MAX)
            {
                this.Rotor2 = 1;
                this.Rotor3++;
            }
            else if (Rotor1 + 1 > MAX)
            {
                this.Rotor1 = 1;
                this.Rotor2++;
            } 
            else
            {
                Rotor1++;
            }
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Initialize the rotor object
        Rotor r = new Rotor();

        public MainWindow()
        {
            InitializeComponent();

            // Populate rotor list from 1-26
            for (int i = 1; i <= r.Max; i++)
            {
                R1.Items.Add(i.ToString());
                R2.Items.Add(i.ToString());
                R3.Items.Add(i.ToString());
            }

            // First index should be selected by default
            // Possible modification: random index by default
            R1.SelectedIndex = 0;
            R2.SelectedIndex = 0;
            R3.SelectedIndex = 0;
        }

        private void Encrypt(TextBox txtDecrypted)
        {
            MessageBox.Show(txtDecrypted.Text);
        }

        private void Decrypt(TextBox txtEncrypted)
        {
            MessageBox.Show(txtEncrypted.Text);
        }

        private void WriteDebuggingInfo()
        {
            /*
            Debug.WriteLine("------------------------------");
            Debug.WriteLine("Rotor1: " + r.Rotor1.ToString());
            Debug.WriteLine("Rotor2: " + r.Rotor2.ToString());
            Debug.WriteLine("Rotor3: " + r.Rotor3.ToString());
            */

            Debug.WriteLine(r.Rotor1.ToString() + " " + r.Rotor2.ToString() + " " + r.Rotor3.ToString());

        }

        // Click Events

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            Encrypt(txtDecrypted);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            Decrypt(txtEncrypted);
        }

        private void btnSetRotor_Click(object sender, RoutedEventArgs e)
        {
            Int32[] rotors = new Int32[] 
            { 
                Int32.Parse(R1.SelectedItem.ToString()), 
                Int32.Parse(R2.SelectedItem.ToString()), 
                Int32.Parse(R3.SelectedItem.ToString())
            };

            // Initialize rotor values
            r.SetRotors(rotors[0], rotors[1], rotors[2]);

            for (int i = 1; i <= 243; i++)
            {
                WriteDebuggingInfo();
                r.IncrementRotors();
            }
        }

        

    }
}
