using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascensore_thread_school
{
    public class Prenotazione
    {
        public enum Piano { terra = 0, primo = 1, secondo = 2, terzo = 3, quarto = 4 }
        Piano _pianoPartenza;
        Piano _pianoArrivo;
        Persona _persona;

        public Prenotazione(Persona pers, int pianoArrivo)
        {
            _persona = pers;

            _pianoPartenza =(Piano) pers.Posizione;
            _pianoArrivo =(Piano) pianoArrivo;

            if (_pianoArrivo == _pianoPartenza)
                throw new Exception("partenza e arrivo sono uguali");


        }
    }
}