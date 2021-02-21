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
        public Dictionary<string, int> personePiano;
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
                prenotazione_lst.Items.Add(p.Nome+"  "+pren.Partenza +"  "+pren.Arrivo);
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


            int b = 0;
            _ascensore.Avanza();
            Add_btn.IsEnabled = true;
        }

        public void AggiornaPiani()
        {
            int a = personePiano["Piano1"];
            Piano1_lbl.Content = "Piano 1: " + a;
            int b =personePiano["Piano2"];
            Piano2_lbl.Content = "Piano 2: " + b;
            int c =personePiano["Piano3"];
            Piano3_lbl.Content = "Piano 3: " + c;
            int d =personePiano["Piano4"];
            Piano4_lbl.Content = "Piano 4: " + d;
            int e =personePiano["Piano5"];
            Piano5_lbl.Content = "Piano 5: " +e;
        }

       

    }
}
