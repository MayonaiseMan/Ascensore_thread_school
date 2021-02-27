using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
namespace Ascensore_thread_school
{
    public class Ascensore
    {
        enum Piano { Piano1 = 1, Piano2 = 2, Piano3 = 3, Piano4 = 4, Piano5 = 5 }
        Piano _posizione;
        Queue<Prenotazione> _prenotazioni;
        Queue<Prenotazione> _personeDentroAscensore;
        const int QUOTA = 50;
        const int DISTANZA_PIANO = 100;

        Thread aggiungiPersona;
        Thread Esecuzione;

        private object _blocco;
        MainWindow _main;


        public Ascensore(MainWindow main)
        {
            _prenotazioni = new Queue<Prenotazione>();
            _personeDentroAscensore = new Queue<Prenotazione>();
            _posizione = (Piano)3;


            aggiungiPersona = new Thread(new ThreadStart(CaricaPersona));
            Esecuzione = new Thread(new ThreadStart(ScaricaPersona));

            _blocco = new object();

            _main = main;
        }

        public Queue<Prenotazione> Prenotazioni
        {
            get
            {
                return _prenotazioni;
            }
        }


        public void AddPrenotazione(Prenotazione p)
        {
            if (_prenotazioni.Contains(p) == false)
                _prenotazioni.Enqueue(p);
            else
                throw new Exception("La prenotazione è già presente");
        }

        public void Avanza()
        {
            aggiungiPersona.Start();
            
            Esecuzione.Start();
            _main.lancia_btn.IsEnabled = false;
        }

        private void CambiaPiano(int a)
        {
            int tmp = (int)_posizione;
            _posizione = (Piano)a;
            int spostamento = ((tmp - a) * DISTANZA_PIANO); // differenza tra i piani + la distanza tra due piani e una quota statica
                                    
            _main.asc_img.Dispatcher.BeginInvoke(new Action(() =>
            {
                _main.asc_img.Margin = new System.Windows.Thickness(_main.asc_img.Margin.Left, _main.asc_img.Margin.Top + (spostamento), 0, 0);
            }));

            Thread.Sleep(TimeSpan.FromMilliseconds(500));
            
        }

        private void CaricaPersona()
        {
            while (true)
            {
                if (_personeDentroAscensore.Count < 3 && _prenotazioni.Count > 0)
                {
                    lock (_blocco)
                    {

                        Prenotazione p = _prenotazioni.Peek();

                        _main.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            int tmp = (int)p.Partenza;
                            string s = "Piano" + tmp;
                            _main.personePiano[s]--;
                            _main.AggiornaPiani();

                        }));

                        CambiaPiano((int)p.Partenza);

                        _prenotazioni.Dequeue();
                        _personeDentroAscensore.Enqueue(p);                       
                    }
                }

                if (_personeDentroAscensore.Count == 0 && _prenotazioni.Count == 0)
                    break;
            }

        }

        private void ScaricaPersona()
        {
            while (true)
            {
                if (_personeDentroAscensore.Count > 0)
                {
                    lock (_blocco)
                    {
                        Prenotazione p = _personeDentroAscensore.Peek();
                        CambiaPiano(p.Arrivo);

                        _main.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            int tmp = (int)p.Arrivo;
                            string s = "Piano" + tmp;
                            _main.personePiano[s]++;
                            _main.AggiornaPiani();

                        }));

                        _personeDentroAscensore.Dequeue();
                        
                    }
                }

                if (_personeDentroAscensore.Count == 0 && _prenotazioni.Count == 0)
                    break;
            }
        }


    }
}
