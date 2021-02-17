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

namespace Ascensore_thread_school
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ascensore _ascensore;
        Dictionary<string, int> personePiano;
        public MainWindow()
        {
            InitializeComponent();
            _ascensore = new Ascensore(this);
            personePiano = new Dictionary<string, int>();
            personePiano.Add("Piano1",0);
            personePiano.Add("Piano2", 0);
            personePiano.Add("Piano3", 0);
            personePiano.Add("Piano4", 0);
            personePiano.Add("Piano5", 0);
        }

        


        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Persona p = new Persona("Persona", int.Parse(from_txt.Text));
                Prenotazione pren = new Prenotazione(p, int.Parse(to_txt.Text));
                _ascensore.AddPrenotazione(pren);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void lancia_btn_Click(object sender, RoutedEventArgs e)
        {
            Add_btn.IsEnabled = false;

            foreach(Prenotazione a in _ascensore.Prenotazioni)
            {
                string nomePiano = "";
                nomePiano = "Piano" + (int)a.Partenza;
                personePiano[nomePiano]++;
            }

            AggiornaPiani();

            _ascensore.Avanza();
            Add_btn.IsEnabled = true;
        }

        public void AggiornaPiani()
        {
            Piano1_lbl.Content = "Piano 1: " + personePiano["Piano1"];
            Piano2_lbl.Content = "Piano 2: " + personePiano["Piano2"];
            Piano3_lbl.Content = "Piano 3: " + personePiano["Piano3"];
            Piano4_lbl.Content = "Piano 4: " + personePiano["Piano4"];
            Piano5_lbl.Content = "Piano 5: " + personePiano["Piano5"];
        }
    }
}
