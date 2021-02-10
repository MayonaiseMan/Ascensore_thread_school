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
        const int PERSONE_MAX = 3;
        Queue<Prenotazione> _prenotazioni;
        Queue<Prenotazione> _personeDentroAscensore;
        Semaphore semaforo;

        Thread aggiungiPersona;
        Thread Esecuzione;


        public Ascensore()
        {
            _prenotazioni = new Queue<Prenotazione>();
            _personeDentroAscensore = new Queue<Prenotazione>();
            _posizione = 0;
            semaforo = new Semaphore(_personeDentroAscensore.Count, PERSONE_MAX);

            aggiungiPersona = new Thread(new ThreadStart(CaricaPersona));
            Esecuzione = new Thread(new ThreadStart(ScaricaPersona));
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
            
        
        }

        private void CaricaPersona()
        {

        }

        private void ScaricaPersona()
        {

        }


    }
}