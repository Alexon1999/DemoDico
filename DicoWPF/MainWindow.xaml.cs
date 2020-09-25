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

namespace DicoWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Dictionary<string, List<Commercial>> dico;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //1)                           List Collection
            List<Commercial> mesCommerciaux = new List<Commercial>();

            // il faut donner un object de type commercial
            //mesCommerciaux.Remove(Commercial c)

            //2)                      Dictionnary collection
            // clé = unique (doit etre de type simple -- int , string ,  bool , double etc )
            // valeur peut etre n'importe quel valur int , string etc ou une liste ou juste un object
            //Dictionary<string, List<Commercial>> dico = new Dictionary<string, List<Commercial>>();

            dico = new Dictionary<string, List<Commercial>>();

            List<Commercial> commercialSud = new List<Commercial>();
            List<Commercial> commercialNord = new List<Commercial>();
            List<Commercial> commercialEst = new List<Commercial>();


            Commercial com1 = new Commercial("Jean");
            Commercial com2 = new Commercial("Louis");
            Commercial com3 = new Commercial("Steph");

            Vente v1 = new Vente("jack buffeteau", 25);
            Vente v2 = new Vente("Elizabeth", 25);
            Vente v3 = new Vente("David", 25);
            Vente v4 = new Vente("Romain", 25);
            Vente v5 = new Vente("pascal", 25);


            com1.AjouterVente(v1);
            com2.AjouterVente(v2);
            com3.AjouterVente(v3);
            com3.AjouterVente(v4);
            com1.AjouterVente(v5);

            commercialSud.Add(com1);
            commercialNord.Add(com2);
            commercialEst.Add(com3);

            dico.Add("Sud", commercialSud);
            dico.Add("Nord", commercialNord);
            dico.Add("Est", commercialEst);

            // Keys est une propriéte de dico
            lstSecteurs.ItemsSource = dico.Keys;

            // donne le value qui correspond à la clé Sud , il va donc donner une liste de type Commercial
            // dico[clé]
            //dico["Sud"]
        }

        private void lstSecteurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstSecteurs.SelectedItem != null)
            {
                // on veut afficher la liste des commerciaux
                lstCommerciaux.ItemsSource = dico[lstSecteurs.SelectedItem as string];

                lstCommerciaux.Items.Refresh();
            }
        }

        private void lstCommerciaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstCommerciaux.SelectedItem != null)
            {
                //                                         plusieurs façon de faire

                //lstVentes.ItemsSource = (lstCommerciaux.SelectedItem as Commercial).LesVentes;
                //lstVentes.ItemsSource = dico[lstSecteurs.SelectedItem as string].ElementAt(lstCommerciaux.SelectedIndex).LesVentes;
                //lstVentes.ItemsSource = dico[lstSecteurs.SelectedItem as string][lstCommerciaux.SelectedIndex].LesVentes;
                lstVentes.ItemsSource = dico[lstSecteurs.SelectedItem as string].Find(commercial => commercial.NomCommercial == (lstCommerciaux.SelectedItem as Commercial).NomCommercial).LesVentes;

                lstVentes.Items.Refresh();
            }
            else
            {
                lstVentes.ItemsSource = null;
            }
        }

        private void btnInserer_Click(object sender, RoutedEventArgs e)
        {

            // Instancier un objet vente : nom Client = "truc" ,  montant = 600
            // inserer le vente a un commercial selectionne
            // Vente v1 = new Vente("truc", 600);

            //                              plusieurs facon de faire
            //(lstCommerciaux.SelectedItem as Commercial).AjouterVente(v1);
            // dico[lstSecteurs.SelectedItem as string].Find(commercial => commercial.NomCommercial == (lstCommerciaux.SelectedItem as Commercial).NomCommercial).AjouterVente(v1);


            // plusieurs facon de rafraichir les ventes
            // lstVentes.Items.Refresh();

            //lstVentes.ItemsSource = null;
            //lstVentes.ItemsSource = (lstCommerciaux.SelectedItem as Commercial).LesVentes;

            Vente v1 = new Vente(txtNomClient.Text, Convert.ToInt16(txtMontant.Text));

            // on aurait pu l'inverse avec if(!dico.ContainsKey(txtNomSecteur.Text))
            if (dico.ContainsKey(txtNomSecteur.Text))
            {
                 //Vente v1 = new Vente(txtNomClient.Text, Convert.ToInt16(txtMontant.Text));
                 Commercial commercialExiste = dico[txtNomSecteur.Text].Find(commercial => commercial.NomCommercial == txtNomCommercial.Text);

                //              differentes façon de faire
                //bool trouve = dico[txtNomSecteur.Text].Contains())
                // exists
                // 1) 
                //if(commercialExiste != null)
                //{
                //    commercialExiste.AjouterVente(v1);
                //}
                //else
                //{
                //    Commercial c = new Commercial(txtNomCommercial.Text);
                //    c.AjouterVente(v1);

                //    dico[txtNomSecteur.Text].Add(c);
                //}

                // 2)
                // Exists : donne un boolean avec un prédicat (expression lambda => arrow function)
                if(dico[txtNomSecteur.Text].Exists(com => com.NomCommercial == txtNomCommercial.Text))
                {
                    // Le commercial existe
                    dico[txtNomSecteur.Text].Find(commercial => commercial.NomCommercial == txtNomCommercial.Text).AjouterVente(v1);
                }
                else
                {
                    // commercial existe pas
                    Commercial c = new Commercial(txtNomCommercial.Text);
                    c.AjouterVente(v1);

                    dico[txtNomSecteur.Text].Add(c);

                }
            }
            else
            {
                // Vente v1 = new Vente(txtNomClient.Text, Convert.ToInt16(txtMontant.Text));
                List<Commercial> nouveauCommerciaux = new List<Commercial>();
                Commercial c = new Commercial(txtNomCommercial.Text);

                c.AjouterVente(v1);
                nouveauCommerciaux.Add(c);

                dico.Add(txtNomSecteur.Text, nouveauCommerciaux);
            }

            lstVentes.Items.Refresh();
            lstSecteurs.Items.Refresh();
            lstCommerciaux.Items.Refresh();


        }
    }
}
