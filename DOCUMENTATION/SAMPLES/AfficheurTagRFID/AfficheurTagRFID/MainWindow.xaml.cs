using System;
using System.Windows;
using MahApps.Metro.Controls;
using ModbusTCP;
using System.Windows.Threading;
using System.Threading;

namespace AfficheurTagRFID
{
    public partial class MainWindow : MetroWindow
    {
        private ModbusTCP.Master MBmaster;
        public delegate void D_UpdateUI_tagRFID(byte[] values);
        public delegate void D_UpdateUI_empty();

        //===============================================================
        #region MAIN
        public MainWindow()
        {
            InitializeComponent();
            //===============================================================
            #region APELLE DE LA METHODE CONNECTION A LA STATION
            RFID_Connect();
            #endregion

            //===============================================================
            #region CREATION ET LANCEMENT DU THREAD DE LECTURE RFID
            Thread thread_ReadRFID = new Thread(ReadRFID);
            thread_ReadRFID.Start();
            #endregion
        }
        #endregion

        //===================================================================
        #region ACTION CLICK BOUTON SETTINGS
        private void openSettings(object sender, RoutedEventArgs e)
        {
            Settings Popup_settings = new Settings();
            Popup_settings.ShowDialog();
        }
        #endregion

        //===================================================================
        #region CONNECTION A LA STATION RFID
        private void RFID_Connect()
        {
            try
            // Create new modbus master and add event functions
            {
                MBmaster = new Master((string)Properties.Settings.Default["adresseIP"], 502);
                MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
                MBmaster.OnException += new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
                MessageBox.Show("Connexion a la station RFID réussie","Connexion réussie",MessageBoxButton.OK);
            }
            catch { MessageBox.Show("Connexion a la station RFID échouée", "Connexion échouée", MessageBoxButton.OK); }
        }
        #endregion

        //===================================================================
        #region THREAD LECTURE DE LA STATION RFID
        private void ReadRFID()
        {
            while (true) {
                ushort ID = ushort.Parse("3");
                byte unit = Convert.ToByte(Properties.Settings.Default.Unit);
                ushort StartAddress = ushort.Parse(Properties.Settings.Default.startAddress);
                byte Length = Convert.ToByte(Properties.Settings.Default.Size);

                MBmaster.ReadInputRegister(ID, unit, StartAddress, Length);
                Thread.Sleep(50);
            }

        }
        #endregion

        //===================================================================
        #region ACTION EFFECTUE SI UNE CARTE EST LUE 
        private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
        {
            Dispatcher.Invoke((D_UpdateUI_tagRFID)UpdateUI, values);
        }
        #endregion

        //===================================================================
        #region ACTION EFFECTUE SI AUCUNE CARTE EST PRESENTE
        private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
        {
            Dispatcher.Invoke((D_UpdateUI_empty)UpdateUI);
        }
        #endregion

        //===================================================================
        #region DELEGATE DE MISE A JOUR UI
        private void UpdateUI(byte[] values) { labelTAGRFID.Content = values[0] + " | " + values[1] + " | " + values[2] ; }
        private void UpdateUI() { labelTAGRFID.Content = "- - -"; }
        #endregion

    }
}
