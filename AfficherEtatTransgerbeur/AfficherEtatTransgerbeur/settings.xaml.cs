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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace AfficherEtatTransgerbeur
{
    /// <summary>
    /// Logique d'interaction pour settings.xaml
    /// </summary>
    public partial class settings : MetroWindow
    {
        public settings()
        {
            InitializeComponent();
            BDD_adresse.Text = Properties.Settings.Default.BDD_adresse;
            BDD_nomBase.Text = Properties.Settings.Default.BDD_nomBase;
            BDD_nomUtilisateur.Text = Properties.Settings.Default.BDD_nomUtilisateur;
            BDD_motDePasse.Password = Properties.Settings.Default.BDD_motDePasse;

            IP_automate.Text = Properties.Settings.Default.IP_automate;
            IP_stationRFID.Text = Properties.Settings.Default.IP_stationRFID;
        }

        private void BT_VALIDER_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["BDD_adresse"] = BDD_adresse.Text;
            Properties.Settings.Default["BDD_nomBase"] = BDD_nomBase.Text;
            Properties.Settings.Default["BDD_nomUtilisateur"] = BDD_nomUtilisateur.Text;
            Properties.Settings.Default["BDD_motDePasse"] = BDD_motDePasse.Password;

            Properties.Settings.Default["IP_automate"] = IP_automate.Text;
            Properties.Settings.Default["IP_stationRFID"] = IP_stationRFID.Text;
            Properties.Settings.Default.Save();
        }

        private void BT_FERMER_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TestConnection_BDD_Click(object sender, RoutedEventArgs e)
        {
            TestConnection TestConnection_BDD_Click = new TestConnection(1)   
            {
                Topmost = true
            };
            TestConnection_BDD_Click.ShowDialog();
            TestConnection_BDD_Click.Focus();
        }

        private void TestConnection_AUTOMATE_Click(object sender, RoutedEventArgs e)
        {
            TestConnection TestConnection_AUTOMATE_Click = new TestConnection(2)
            {
                Topmost = true
            };
            TestConnection_AUTOMATE_Click.ShowDialog();
            TestConnection_AUTOMATE_Click.Focus();
        }

        private void TestConnection_RFID_Click(object sender, RoutedEventArgs e)
        {
            TestConnection TestConnection_RFID_Click = new TestConnection(3)
            {
                Topmost = true
            };
            TestConnection_RFID_Click.ShowDialog();                                                                         
            TestConnection_RFID_Click.Focus();
        }
    }
}
