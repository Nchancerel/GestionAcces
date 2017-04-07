using System.Windows;
using MahApps.Metro.Controls;

namespace AfficheurTagRFID
{
    public partial class Settings : MetroWindow
    {
        //===============================================================
        #region MAIN
        public Settings()
        {
            InitializeComponent();
            //===============================================================
            #region INITIALISATION DES LABELS
            labelUNIT.Text         = Properties.Settings.Default.Unit;
            labelSTARTADDRESS.Text = Properties.Settings.Default.startAddress;
            labelSIZE.Text         = Properties.Settings.Default.Size;
            labelSTATIONRFID.Text  = Properties.Settings.Default.adresseIP;
            #endregion
        }
        #endregion

        //===============================================================
        #region ACTION BOUTON VALIDER
        private void Button_Valider(object sender, RoutedEventArgs e)
        {
            //===============================================================
            #region ENREGISTREMENT DES VALEURS
            Properties.Settings.Default["Unit"]         = labelUNIT.Text;
            Properties.Settings.Default["startAddress"] = labelSTARTADDRESS.Text;
            Properties.Settings.Default["Size"]         = labelSIZE.Text;
            Properties.Settings.Default["adresseIP"]    = labelSTATIONRFID.Text;

            Properties.Settings.Default.Save();
            #endregion
            this.Close();
        }
        #endregion

    }
}
