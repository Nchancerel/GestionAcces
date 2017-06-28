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
        private bool etat_Connection_AUTOMATE = false;

        private bool status_RFID_process = false;

        private int etat_CYCLE = 3;
        private bool etat_gache = false;

        // ---- THREAD
        Thread thread_ReadRFID;
        
        Thread T_Test_BDD;
        Thread T_Test_RFID;
        Thread T_Test_AUTOMATE;

        Thread T_AffichageEtat_CYCLE;
        Thread T_AffichageEtat_GACHE;
        Thread T_MAJ_Automate;
        #endregion

        //===============================================================
        #region MAIN
        public MainWindow()
        {
            InitializeComponent();
            
            //====================================
            #region thread lecture RFID
            thread_ReadRFID = new Thread(ReadRFID);
            thread_ReadRFID.IsBackground = true;
            thread_ReadRFID.Start();
            #endregion

            //====================================
            #region Thread Test BDD
            T_Test_BDD = new Thread(Test_BDD);
            T_Test_BDD.IsBackground = true;
            T_Test_BDD.Start();
            #endregion

            //====================================
            #region Thread Test RFID
            T_Test_RFID = new Thread(Test_RFID);
            T_Test_RFID.IsBackground = true;
            T_Test_RFID.Start();
            #endregion

            //====================================
            #region Thread Test AUTOMATE
            T_Test_AUTOMATE = new Thread(Test_AUTOMATE);
            T_Test_AUTOMATE.IsBackground = true;
            T_Test_AUTOMATE.Start();
            #endregion

            //====================================
            #region Thread Affichage Etat CYCLE
            T_AffichageEtat_CYCLE = new Thread(AffichageEtat_CYCLE);
            T_AffichageEtat_CYCLE.IsBackground = true;
            T_AffichageEtat_CYCLE.Start();
            #endregion

            //====================================
            #region Thread Affichage Etat GACHE
            T_AffichageEtat_GACHE = new Thread(AffichageEtat_GACHE);
            T_AffichageEtat_GACHE.IsBackground = true;
            T_AffichageEtat_GACHE.Start();
            #endregion




        }
        #endregion

        //===============================================================
        #region procedure de test connection BDD
        private void Test_BDD() {
            while (true)
            {
                try
                {
                    conn_BDD = new MySql.Data.MySqlClient.MySqlConnection(info_BDD);
                    conn_BDD.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText  = "SELECT * FROM `utilisateur`";
                    cmd.Connection   = conn_BDD;

                    MySqlDataReader rdrUser = cmd.ExecuteReader();
                    rdrUser.Read();
                    conn_BDD.Close();

                    etat_Connection_BDD = true;
                    Dispatcher.Invoke(new Action(() => UpdateUI_BDD(true)));
                }
                catch
                {
                    etat_Connection_BDD = false;
                    Dispatcher.Invoke(new Action(() => UpdateUI_BDD(false)));
                }
                Task.Delay(1000).Wait();
            }
            
        }
        #endregion

        //===============================================================
        #region procedure de test connection Test_AUTOMATE
        private void Test_AUTOMATE()
        {
            while (true)
            {
                try
                {
                    //

                    Dispatcher.Invoke(new Action(() => UpdateUI_RFID(false)));
                }
                catch
                {
                    Dispatcher.Invoke(new Action(() => UpdateUI_RFID(true)));
                }
                Task.Delay(1000).Wait();
            }
        }
        #endregion

        //===============================================================
        #region procedure de test connection Test_RFID
        private void Test_RFID()
        {
            while (true) {
                if (RFID_Connect())
                {
                    etat_Connection_RFID = true;
                    Dispatcher.Invoke(new Action(() => UpdateUI_RFID(true)));
                }
                else
                {
                    etat_Connection_RFID = false;
                    Dispatcher.Invoke(new Action(() => UpdateUI_RFID(false)));
                }
                Task.Delay(1000).Wait();
            }
            
        }
        #endregion

        //===============================================================
        #region procedure d'Affichage Etat CYCLE
        private void AffichageEtat_CYCLE()
        {


            while (true) { 
                Dispatcher.Invoke(new Action(() => UpdateUI_CYCLE(1)));
                Task.Delay(5000).Wait();

                Dispatcher.Invoke(new Action(() => UpdateUI_CYCLE(2)));
                Task.Delay(5000).Wait();

                Dispatcher.Invoke(new Action(() => UpdateUI_CYCLE(3)));
                Task.Delay(5000).Wait();

            }

        }
        #endregion

        //===============================================================
        #region procedure d'Affichage Etat GACHE
        private void AffichageEtat_GACHE()
        {
            while (true) {

                Dispatcher.Invoke(new Action(() => UpdateUI_GACHE(true)));
                Task.Delay(5000).Wait();

                Dispatcher.Invoke(new Action(() => UpdateUI_GACHE(false)));
                Task.Delay(5000).Wait();
            }

        }
        #endregion

        //===============================================================
        #region THREAD de lecture de la sation RFID tout les 500ms
        private void ReadRFID()
        {
            while (true) { 
                while (etat_Connection_RFID)
                {
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
            
        }
        #endregion



        //===================================================================
        #region CONNECTION A LA STATION RFID
        private bool RFID_Connect()
        {
            try
            {
                if (MBmaster.connected) {
                    return true;
                }
                return false;
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
                catch {
                    return false;
                }
            }
        }
        #endregion

        //===================================================================
        #region ACTION EFFECTUE SI UNE CARTE EST LUE 
        private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values) { Dispatcher.Invoke((DEL_RFID_process)RFID_process, values); }
        #endregion

        //===================================================================
        #region ACTION EFFECTUE SI AUCUNE CARTE EST PRESENTE
        private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception) { }
        #endregion


        //===================================================================
        #region METHODE DE MISE A JOUR DE L'AFFICHAGE 
        #region | AUTOMATE
        private void UpdateUI_AUTOMATE(bool _etat)
        {
            if (_etat)
            {
                BitmapImage img_AUTOMATE_ON = new BitmapImage();
                img_AUTOMATE_ON.BeginInit();
                img_AUTOMATE_ON.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/AUTOMATE_ON.png", UriKind.Relative);
                img_AUTOMATE_ON.EndInit();
                AUTOMATE_status.Source = img_AUTOMATE_ON;
            }
            else
            {
                BitmapImage img_AUTOMATE_OFF = new BitmapImage();
                img_AUTOMATE_OFF.BeginInit();
                img_AUTOMATE_OFF.UriSource = new Uri("/AfficherEtatTransgerbeur;component/img/AUTOMATE_OFF.png", UriKind.Relative);
                img_AUTOMATE_OFF.EndInit();
                AUTOMATE_status.Source = img_AUTOMATE_OFF;
            }

        }
        #endregion
        #region | RFID
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
        #endregion
        #region | BDD
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
                BDD_status.Source = img_BDD_OFF;
            }
        }
        #endregion
        #region | BDD
        private void UpdateUI_CYCLE(int _etat)
        {
            var bc = new BrushConverter();
            switch (_etat)
            {
                case 1:
                    LBL_etatCycle.Content = "EN CYCLE";
                    LBL_etatCycle.Foreground = (Brush)bc.ConvertFrom("#3FFFFFFF");
                    BR_etatCycle.Background = (Brush)bc.ConvertFrom("#FF23BD2A");
                    BR_etatCycle.BorderBrush = (Brush)bc.ConvertFrom("#FF208525");
                    break;
                case 2:
                    LBL_etatCycle.Content = "PAS DE CYCLE";
                    LBL_etatCycle.Foreground = (Brush)bc.ConvertFrom("#3FFFFFFF");
                    BR_etatCycle.Background = (Brush)bc.ConvertFrom("#FFFF8C00");
                    BR_etatCycle.BorderBrush = (Brush)bc.ConvertFrom("#FFFF6400");
                    break;
                case 3:
                    LBL_etatCycle.Content = "ARRET";
                    LBL_etatCycle.Foreground = (Brush)bc.ConvertFrom("#3FFFFFFF");
                    BR_etatCycle.Background = (Brush)bc.ConvertFrom("#FFE22828");
                    BR_etatCycle.BorderBrush = (Brush)bc.ConvertFrom("#FF800808");
                    break;
                default:
                    
                    break;
            }
        }
        #endregion
        #region | BDD
        private void UpdateUI_GACHE(bool _etat)
        {
            var bc = new BrushConverter();
            if (_etat)
            {
                
                //LBL_etatAcces.Content = "AUTORISÉ";
                LBL_etatAcces.Content = "OUVERT";
                LBL_etatAcces.Foreground = (Brush)bc.ConvertFrom("#3FFFFFFF");
                BR_etatAcces.Background = (Brush)bc.ConvertFrom("#FF23BD2A");
                BR_etatAcces.BorderBrush = (Brush)bc.ConvertFrom("#FF008919");
            }
            else
            {
                //LBL_etatAcces.Content = "REFUSÉ";
                LBL_etatAcces.Content = "FERMÉ";
                LBL_etatAcces.Foreground = (Brush)bc.ConvertFrom("#3FFFFFFF");
                BR_etatAcces.Background = (Brush)bc.ConvertFrom("#FFE22828");
                BR_etatAcces.BorderBrush = (Brush)bc.ConvertFrom("#FF890000");
            }
        }
        #endregion
        #endregion

        //===================================================================
        #region PROCEDURE DE TRAITEMENT D'UN TAG RFID 
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
        #endregion

        //===================================================================
        #region PROCEDURE CLIC BOUTTON CONFIGURATION
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
        #endregion
    }
}
