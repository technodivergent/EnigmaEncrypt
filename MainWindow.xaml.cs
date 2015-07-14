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
        public Int32 X;
        public Int32 Y;
        public Int32 Z;
        private Int32 _sum;
        
        public Int32 Sum
        {
            get { return _sum = X + Y + Z; }
        }

        public Int32 Max
        {
            get { return MAX; }
        }

        public void SetRotors(Int32 Rotor1, Int32 Rotor2, Int32 Rotor3)
        {
            this.X = Rotor1;
            this.Y = Rotor2;
            this.Z = Rotor3;
        }

        public void IncrementRotors()
        {
            X++;
            // Increment rotors
            if (X > MAX)
            {
                X = 1;
                Y++;
            }
            
            if (Y > MAX)
            {
                Y = 1;
                Z++;
            }
            
            if (Z > MAX)
            {
                Z = 1;
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
            Char[] message = txtDecrypted.Text.ToCharArray();
            Int32 alphaIndex = 0;
            Int32 alphaShifted = 0;
            Int32 shift = 0;
            String encrypted = "";

            foreach (char c in message)
            {
                alphaIndex = (char.ToUpper(c) - 64);
                shift = r.Sum;

                if (alphaIndex == -32)
                    alphaIndex = 128;

                alphaShifted = (alphaIndex + shift) + 64;

                if (alphaShifted > 100)
                    alphaShifted = 95;

                encrypted += char.ToString((char)alphaShifted);
                r.IncrementRotors();

                // Write output
                // Debug.WriteLine(alphaIndex.ToString() + " + " + shift.ToString() + " = " + (alphaShifted - 64).ToString());
                
            }
            txtEncrypted.Text = encrypted.ToString();
        }

        private void Decrypt(TextBox txtEncrypted)
        {
            Char[] message = txtEncrypted.Text.ToCharArray();
            Int32 alphaIndex = 0;
            Int32 alphaShifted = 0;
            Int32 shift = 0;
            String encrypted = "";

            foreach (char c in message)
            {
                alphaIndex = (char.ToUpper(c) - 64);
                shift = r.Sum;

                if (alphaIndex == 31)
                    alphaIndex = 128;

                alphaShifted = (alphaIndex - shift) + 64;

                if (alphaShifted > 100)
                    alphaShifted = 95;

                encrypted += char.ToString((char)alphaShifted);
                r.IncrementRotors();

                // Write output
                Debug.WriteLine(alphaIndex.ToString() + " + " + shift.ToString() + " = " + (alphaShifted - 64).ToString());

            }
            txtDecrypted.Text = encrypted.ToString();
        }

        private void WriteDebuggingInfo()
        {
            // Show Rotor values
            Debug.WriteLine(r.X.ToString() + " " + r.Y.ToString() + " " + r.Z.ToString() + " = " + r.Sum);
            //Debug.WriteLine("----");
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

            // For testing purposes:
            /*
            for (int i = 1; i <= 28; i++)
            {
                WriteDebuggingInfo();
                r.IncrementRotors();
            }
            */
        }

        

    }
}
