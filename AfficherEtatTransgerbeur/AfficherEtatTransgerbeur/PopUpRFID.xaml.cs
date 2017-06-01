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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.Threading;
using EasyModbus;

namespace AfficherEtatTransgerbeur 
{
    /// <summary>
    /// Logique d'interaction pour MessageBox.xaml
    /// </summary>
    /// 
  

    public partial class PopUpRFID : MetroWindow
    {
        //===============================================================
        #region Déclaration des variables
        

        // ---- DELEGATE
        public delegate void D_UpdateUI_error(); 
        public delegate void D_UpdateUI_affichage(bool autorisation);
        private string msg_error;
        private string msg_acces = "Accès autorisé"; 

        // ---- TAG RFID (Hvalues = byte1 / Lvalues = byte2 )
        private byte Hvalues;
        private byte Lvalues;

        // ---- INFO UTILISATEUR
        string id_utilisateur;
        string etat_utilisateur;
        string id_role;

        // ---- BDD
        protected MySql.Data.MySqlClient.MySqlConnection conn_BDD;
        string info_BDD = "server=" + Properties.Settings.Default.BDD_adresse + ";"
                                    + "uid=" + Properties.Settings.Default.BDD_nomUtilisateur + ";"
                                    + "pwd=" + Properties.Settings.Default.BDD_motDePasse + ";"
                                    + "database=" + Properties.Settings.Default.BDD_nomBase + ";"
                                    + "Charset=latin1;";

        // ---- AUTOMATE
        private ModbusClient modbusClient;
        private string automate_IP = Properties.Settings.Default.IP_automate;
        private int automate_PORT = 502;
        int Gache_adresse = int.Parse("400000", System.Globalization.NumberStyles.HexNumber);

        // ---- TIMER (fermeture automatique au bout de 2s)
        DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        // ---- THREAD
        Thread T_connectBDD;
        #endregion


        //===============================================================
        #region MAIN
        public PopUpRFID(byte _Hvalues, byte _Lvalues)
        {
            InitializeComponent();

            modbusClient = new ModbusClient(automate_IP, automate_PORT);    //Ip-Address and Port of Modbus-TCP-Server
            
            

            this.Hvalues = _Hvalues;
            this.Lvalues = _Lvalues;

            T_connectBDD = new Thread(procedure);
            T_connectBDD.IsBackground = true;
            T_connectBDD.Start();
        }
        #endregion

        //===============================================================
        #region Procedure de lecture RFID (THREAD) 
        private void procedure() {
            try
            {
                // ----  a la base de donnée
                conn_BDD = new MySql.Data.MySqlClient.MySqlConnection(info_BDD);
                conn_BDD.Open();

                // ---- assignation du TAGRFID
                string myHex = Hvalues.ToString("X") + Lvalues.ToString("X");  // Gives you hexadecimal

                int TagRFID = Convert.ToInt32(myHex, 16);  // Back to int again.

                // ---- creation de la requete SQL
                MySqlCommand cmd        = new MySqlCommand();
                cmd.CommandText         = "SELECT `etat_utilisateur` , `id_role` , `id` FROM `utilisateur` WHERE `tag_rfid` = " + TagRFID;
                cmd.Connection          = conn_BDD;
                MySqlDataReader rdrUser = cmd.ExecuteReader();
                
                try
                {
                    // ---- lecture de la response SQL
                    rdrUser.Read();
                    etat_utilisateur = rdrUser[0].ToString();
                    id_role          = rdrUser[1].ToString();
                    id_utilisateur   = rdrUser[2].ToString();

                    // ---- fermeture du reader et BDD
                    rdrUser.Close();
                    conn_BDD.Close();

                    try{
                        //modbusClient.Connect();
                        modbusClient.UnitIdentifier = 2;

                        try {
                            int etatMachine = get_EtatMachine();

                            try
                            {
                                // ---- ouverture BDD
                                conn_BDD.Open();

                                // ---- création de la requete INSERT SQL
                                MySqlCommand cmd_archivage = new MySqlCommand("INSERT INTO archive(etat_machine, etat_acces, id_utilisateur) VALUES(@etat_machine,@etat_acces,@id_utilisateur)", conn_BDD);
                                cmd_archivage.Parameters.AddWithValue("@etat_machine", RandomNumber(1, 4).ToString());
                                cmd_archivage.Parameters.AddWithValue("@etat_acces", RandomNumber(1, 3).ToString());
                                cmd_archivage.Parameters.AddWithValue("@id_utilisateur", id_utilisateur);

                                // ---- éxecution de la requete
                                cmd_archivage.ExecuteNonQuery();
                                cmd_archivage.Parameters.Clear();

                                try
                                {

                                    if (id_role == "1")
                                    {
                                        //modbusClient.WriteSingleRegister(Gache_adresse, 0);
                                        Dispatcher.Invoke((D_UpdateUI_affichage)UpdateUI_acces, true);
                                    }
                                    else if (id_role == "2" && etatMachine == 1)
                                    {
                                        Dispatcher.Invoke((D_UpdateUI_affichage)UpdateUI_acces, true);
                                    }
                                    else
                                    {
                                        msg_acces = "Accès refusé";
                                        Dispatcher.Invoke((D_UpdateUI_affichage)UpdateUI_acces, false);
                                    }
                                }
                                catch
                                {
                                    // ---- affichage de l'erreur
                                    msg_error = "Erreur lors de l'ouverture de l'accès";
                                    Dispatcher.Invoke((D_UpdateUI_error)UpdateUI_error);
                                }

                            }
                            catch {
                                // ---- affichage de l'erreur
                                msg_error = "Erreur à l'archivage de la demande";
                                Dispatcher.Invoke((D_UpdateUI_error)UpdateUI_error);
                            }
                        }
                        catch
                        {                                // ---- affichage de l'erreur
                            msg_error = "Impossible de récupérer l'état de l'automate";
                            Dispatcher.Invoke((D_UpdateUI_error)UpdateUI_error);
                        }
                    }
                    catch
                    {                                 // ---- affichage de l'erreur
                        msg_error = "Connexion a l'automate impossible";
                        Dispatcher.Invoke((D_UpdateUI_error)UpdateUI_error);
                    }
                    
                }
                catch {
                    // ---- affichage de l'erreur
                    msg_error = "Carte non assignée, merci de contacter le chef de chantier.";
                    Dispatcher.Invoke((D_UpdateUI_error)UpdateUI_error);
                }
            }
            catch
            {
                // ---- affichage de l'erreur
                msg_error = "Connexion a la base de données impossible !";
                Dispatcher.Invoke((D_UpdateUI_error)UpdateUI_error);
            }

            // ---- fermeture de la BDD
            conn_BDD.Close();

        }
        #endregion

        public int get_EtatMachine()
        {
            int registre;
            try
            {
                //string data = "";
                int[] readHoldingRegisters = modbusClient.ReadInputRegisters(0, 2);
                registre = readHoldingRegisters[1];

                //data += registre;

                //MessageBox.Show(data);
            }
            catch
            {
                registre = 0;
                //MessageBox.Show("Connexion automate interrompue.");
            }
            return registre;

        }

        //===============================================================
        #region DELEGATE AFFICHAGE MESSAGE ERREUR
        private void UpdateUI_error() {
            this.loader.Visibility = Visibility.Hidden;
            this.label_info.Content = msg_error;
            this.label_info.Visibility = Visibility.Visible;
            timer.Interval = TimeSpan.FromSeconds(4d);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        #endregion

        //===============================================================
        #region DELEGATE AFFICHAGE MESSAGE ACCES
        private void UpdateUI_acces(bool autorisation)
        {
            this.loader.Visibility = Visibility.Hidden;
            this.label_info.Content = msg_acces;
            this.label_info.Visibility = Visibility.Visible;

            if (autorisation == true)
            {
                this.AUTORISE.Visibility = Visibility.Visible;

            } else
            {
                this.REFUSE.Visibility = Visibility.Visible;
            }

            timer.Interval = TimeSpan.FromSeconds(4d);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        #endregion

        //===============================================================
        #region TIMER FERMETURE 2S
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }
        #endregion

        //===================================================================
        #region CLOSE EVENT
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            T_connectBDD.Abort();
        }
        #endregion

        //===============================================================
        #region GENERATION NOMBRE ALEATOIRE
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        #endregion
    }
}
