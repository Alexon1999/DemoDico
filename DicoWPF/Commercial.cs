using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicoWPF
{
    public class Commercial
    {
        private string nomCommercial;
        private List<Vente> lesVentes;


        public Commercial (string unNom)
        {
            NomCommercial = unNom;
            LesVentes = new List<Vente>();
        }


        public void AjouterVente(Vente uneVente)
        {
            lesVentes.Add(uneVente);
        }

        public string NomCommercial { get => nomCommercial; set => nomCommercial = value; }
        public List<Vente> LesVentes { get => lesVentes; set => lesVentes = value; }
    }
}
