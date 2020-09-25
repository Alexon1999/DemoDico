using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicoWPF
{
    public class Vente
    {
        private string nomClient;
        private int montant;

        public Vente(string unNom , int unMontant)
        {
            NomClient = unNom;
            Montant = unMontant;
        }

        public string NomClient { get => nomClient; set => nomClient = value; }
        public int Montant { get => montant; set => montant = value; }
    }
}
