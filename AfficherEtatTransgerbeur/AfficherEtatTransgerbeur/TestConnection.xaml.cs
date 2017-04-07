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
using ModbusTCP;
using System.Windows.Threading;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AfficherEtatTransgerbeur
{
    /// <summary>
    /// Logique d'interaction pour TestConnection.xaml
    /// </summary>
    public partial class TestConnection : MetroWindow
    {
        protected MySql.Data.MySqlClient.MySqlConnection conn_BDD;
        string info_BDD = "server=" + Properties.Settings.Default.BDD_adresse + ";"
                              + "uid=" + Properties.Settings.Default.BDD_nomUtilisateur + ";"
                              + "pwd=" + Properties.Settings.Default.BDD_motDePasse + ";"
                              + "database=" + Properties.Settings.Default.BDD_nomBase + ";"
                              + "Charset=latin1;";

        private ModbusTCP.Master MBmaster;
        private int nb_typeTest;
        private bool resultat;

        public delegate void D_UpdateUI();

        public TestConnection() {
            InitializeComponent();
        }

        public TestConnection(int _nb_typeTest)
        {
            InitializeComponent();
            nb_typeTest = _nb_typeTest;


            resultat = true;
            LabelMessage.Visibility = Visibility.Hidden;

            switch (_nb_typeTest)
            {
                case 1:
                    Thread thread_BDD = new Thread(test_BDD);
                    thread_BDD.Start();
                    break;
                case 2:
                    test_AUTOMATE();
                    break;
                case 3:
                    Thread thread_RFID = new Thread(test_RFID);
                    thread_RFID.Start();
                    break;
            }

        }

        private void test_BDD() {
            try
            {
                // 
                conn_BDD = new MySql.Data.MySqlClient.MySqlConnection(info_BDD);

                conn_BDD.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT * FROM `utilisateur`";

                cmd.Connection = conn_BDD;
                MySqlDataReader rdrUser = cmd.ExecuteReader();

                rdrUser.Read();

                conn_BDD.Close();




                resultat = true;
            }
            catch
            {
                resultat = false;
            }

            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                Dispatcher.Invoke((D_UpdateUI)UpdateUI_BDD);
            }
            catch  { return; }

        }

        private void test_AUTOMATE() {


        }

        private void test_RFID() {

            try {
                // Create new modbus master and add event functions

                MBmaster = new Master((string)Properties.Settings.Default["IP_stationRFID"], 502);
                resultat = true;
            }
            catch {
                resultat = false;
            }

            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                Dispatcher.Invoke((D_UpdateUI)UpdateUI_RFID);
            }
            catch { return; }
                                            
            
            
            

        }

        private void UpdateUI_BDD()
        {
            if (resultat)
            {
                LabelMessage.Content = "Connexion réussie avec la Base de données : " + (string)Properties.Settings.Default["BDD_adresse"];
                LoadingGif.Visibility = Visibility.Hidden;
                LabelMessage.Visibility = Visibility.Visible;
            }
            else
            {
                LabelMessage.Content = "Connexion impossible avec la Base de données : " + (string)Properties.Settings.Default["BDD_adresse"];
                LoadingGif.Visibility = Visibility.Hidden;
                LabelMessage.Visibility = Visibility.Visible;
            }
        }

        private void UpdateUI_RFID()
        {
            if (resultat)
            {
                LabelMessage.Content = "Connexion réussie avec la station RFID : " + (string)Properties.Settings.Default["IP_stationRFID"];
                LoadingGif.Visibility = Visibility.Hidden;
                LabelMessage.Visibility = Visibility.Visible;
            }
            else
            {
                LabelMessage.Content = "Connexion impossible avec la station RFID : " + (string)Properties.Settings.Default["IP_stationRFID"];
                LoadingGif.Visibility = Visibility.Hidden;
                LabelMessage.Visibility = Visibility.Visible;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
