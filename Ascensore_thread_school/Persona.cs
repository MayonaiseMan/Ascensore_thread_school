using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascensore_thread_school
{
    public class Persona
    {
        public enum Piano { terra = 0, primo = 1, secondo = 2, terzo = 3, quarto = 4 }
        private Piano _posizione;
        string _nome;

        public Persona(string nome, int piano)
        {
            if (!string.IsNullOrEmpty(nome))
                _nome = nome;
            else throw new Exception("nome non valido");

            Posizione = (Piano)piano;            
        }

        public string Nome
        {
            get
            {
                return _nome;
            }
        }

        public Piano Posizione 
        { 
            get => _posizione; 
            private set => _posizione = value; 
        }
    }
}