using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ascensore_thread_school
{
    public class Ascensore
    {
        enum Piano { terra = 0, primo = 1, secondo = 2, terzo = 3, quarto = 4}
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
            _posizione = 0;
            

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
        
        }

        private void CambiaPiano(int a)
        {
            int tmp = (int)_posizione;
            _posizione = (Piano)a;
            int spostamento = ((tmp - (int)_posizione) * DISTANZA_PIANO) +  QUOTA; // differenza tra i piani + la distanza tra due piani e una quota statica
            

        }

        private void CaricaPersona()
        {
            while (_prenotazioni.Count > 0)
            {
                if (_personeDentroAscensore.Count < 3)
                {
                    lock (_blocco)
                    {
                        Prenotazione p = _prenotazioni.Dequeue();
                        CambiaPiano(p.Partenza);
                        _personeDentroAscensore.Enqueue(p);                        
                    }
                }
            }
               
        }

        private void ScaricaPersona()
        {
            while(_personeDentroAscensore.Count > 0 && _prenotazioni.Count > 0)
            {
                if (_personeDentroAscensore.Count > 0)
                {
                    lock (_blocco)
                    {
                        Prenotazione p = _personeDentroAscensore.Peek();
                        CambiaPiano(p.Arrivo);
                        _personeDentroAscensore.Dequeue();
                    }
                }
            }            
        }


    }
}