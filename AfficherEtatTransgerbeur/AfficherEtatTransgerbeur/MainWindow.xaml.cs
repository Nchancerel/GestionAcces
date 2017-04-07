using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModbusTCP;
using System.Windows.Threading;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace AfficherEtatTransgerbeur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //===============================================================
        #region Déclaration des variables

        // ---- DELEGATE
        public delegate void DEL_RFID_process(byte[] values);

        public delegate bool DEL_UpdateUI(bool _etat);

        // ---- BDD
        protected MySql.Data.MySqlClient.MySqlConnection conn_BDD;
        string info_BDD = "server=" + Properties.Settings.Default.BDD_adresse + ";"
                              + "uid=" + Properties.Settings.Default.BDD_nomUtilisateur + ";"
                              + "pwd=" + Properties.Settings.Default.BDD_motDePasse + ";"
                              + "database=" + Properties.Settings.Default.BDD_nomBase + ";"
                              + "Charset=latin1;";

        // ---- MODBUSTCP
        private ModbusTCP.Master MBmaster;

        // ---- ETAT DU SYSTEME
        private bool etat_Connection_BDD = false;
        private bool etat_Connection_RFID = false;
        private bool status_RFID_process = false;

        // ---- THREAD
        Thread thread_ReadRFID;
        Thread Test_BDD;
        #endregion

        //===============================================================
        #region MAIN
        public MainWindow()
        {
            InitializeComponent();

            Thread thread_ReadRFID = new Thread(ReadRFID);
            thread_ReadRFID.Start();

            Thread Test_BDD = new Thread(TestBDD);
            Test_BDD.Start();


        }
        #endregion

        //===============================================================
        #region procedure de test connection BDD
        private void TestBDD() {
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
                
                Dispatcher.Invoke(new Action(() => UpdateUI_BDD(false)));
            }
            catch
            {
                Dispatcher.Invoke(new Action(() => UpdateUI_BDD(true)));
            }
        }
        #endregion

        private void ReadRFID()
        {
            while (!etat_Connection_RFID) {

                //-------------------------------------------------
                // Verify RFID ModbusTCP connection
                if (RFID_Connect()) {
                    Dispatcher.Invoke(new Action(() => UpdateUI_RFID(false)));
                }

                else {
                    Dispatcher.Invoke(new Action(() => UpdateUI_RFID(true)));
                } 
            }

            while (etat_Connection_RFID) {
                ushort ID = ushort.Parse("3");

                //byte unit = Convert.ToByte(Properties.Settings.Default.Unit);
                //ushort StartAddress = ushort.Parse(Properties.Settings.Default.startAddress);
                //byte Length = Convert.ToByte(Properties.Settings.Default.Size);

                byte unit = Convert.ToByte(1);
                ushort StartAddress = 0;
                byte Length = Convert.ToByte(32);


                MBmaster.ReadHoldingRegister(ID, unit, StartAddress, Length);
                Task.Delay(500).Wait();
            }
        }



        //===================================================================
        #region CONNECTION A LA STATION RFID
        private bool RFID_Connect()
        {
            try
            {
                if (MBmaster.connected) { return true; }
                else return false;
            }
            catch
            {
                try
                // Create new modbus master and add event functions
                {
                    MBmaster = new Master((string)Properties.Settings.Default["IP_stationRFID"], 502);
                    MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
                    MBmaster.OnException += new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
                    return true;
                }
                catch { return false; }
            }
        }
        #endregion

        //===================================================================
        #region ACTION EFFECTUE SI UNE CARTE EST LUE 
        private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            Dispatcher.Invoke((DEL_RFID_process)RFID_process, values);
        }
        #endregion

        //===================================================================
        #region ACTION EFFECTUE SI AUCUNE CARTE EST PRESENTE
        private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
        {
            
        }
        #endregion


        //===================================================================
        #region ACTION EFFECTUE SI AUCUNE CARTE EST PRESENTE
        private void UpdateUI_AUTOMATE(bool _etat)
        {
            if (_etat)
            {
                BitmapImage img_RFID_ON = new BitmapImage();
                img_RFID_ON.BeginInit();
                img_RFID_ON.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/AUTOMATE_ON.png", UriKind.Relative);
                img_RFID_ON.EndInit();
                RFID_status.Source = img_RFID_ON;
            }
            else
            {
                BitmapImage img_RFID_OFF = new BitmapImage();
                img_RFID_OFF.BeginInit();
                img_RFID_OFF.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/AUTOMATE_OFF.png", UriKind.Relative);
                img_RFID_OFF.EndInit();
                RFID_status.Source = img_RFID_OFF;
            }

        }
        private void UpdateUI_RFID(bool _etat)
        {
            if (_etat)
            {
                BitmapImage img_RFID_ON = new BitmapImage();
                img_RFID_ON.BeginInit();
                img_RFID_ON.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/RFID_ON.png", UriKind.Relative);
                img_RFID_ON.EndInit();
                RFID_status.Source = img_RFID_ON;
            }
            else
            {
                BitmapImage img_RFID_OFF = new BitmapImage();
                img_RFID_OFF.BeginInit();
                img_RFID_OFF.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/RFID_OFF.png", UriKind.Relative);
                img_RFID_OFF.EndInit();
                RFID_status.Source = img_RFID_OFF;
            }
        }
        private void UpdateUI_BDD(bool _etat)
        {
            if (_etat)
            {
                BitmapImage img_BDD_ON = new BitmapImage();
                img_BDD_ON.BeginInit();
                img_BDD_ON.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/BDD_ON.png", UriKind.Relative);
                img_BDD_ON.EndInit();
                BDD_status.Source = img_BDD_ON;
            }
            else
            {
                BitmapImage img_BDD_OFF = new BitmapImage();
                img_BDD_OFF.BeginInit();
                img_BDD_OFF.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/BDD_OFF.png", UriKind.Relative);
                img_BDD_OFF.EndInit();
                RFID_status.Source = img_BDD_OFF;
            }
        }
        #endregion

        private void RFID_process(byte[] values)
        {
            if (!status_RFID_process)
            {
                status_RFID_process = true;
                PopUpRFID Popup = new PopUpRFID(values[0],values[1])
                {
                    Topmost = true
                };
                var darkwindow = new Window()
                {
                    Background = Brushes.Black,
                    Opacity = 0.4,
                    AllowsTransparency = true,
                    WindowStyle = WindowStyle.None,
                    WindowState = WindowState.Maximized

                };
                darkwindow.Show();
                Popup.ShowDialog();
                darkwindow.Close();
                Popup.Close();
                Task.Delay(100).Wait();
                status_RFID_process = false;
            }

        }

        private void bt_Gear_Click(object sender, RoutedEventArgs e)
        {
            settings PopupSettings = new settings()
            {
                Topmost = true
            };
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized
            };
            darkwindow.Show();
            PopupSettings.ShowDialog();
            darkwindow.Close();
            PopupSettings.Focus();
        }
    }
}
