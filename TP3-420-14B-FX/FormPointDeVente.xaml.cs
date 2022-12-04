using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TP3_420_14B_FX.classes;

namespace TP3_420_14B_FX
{
    /// <summary>
    /// Logique d'interaction pour FormPointDeFente.xaml
    /// </summary>
    public partial class FormPointDeVente : Window
    {
       
        public FormPointDeVente()
        {
            InitializeComponent();
 
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        /// <summary>
        /// Permet la création et l'affichage d'un bouton catégorie.
        /// </summary>
        /// <param name="categorie">La catégorie à afficher</param>
        private void AjouterBoutonCategorie(Categorie categorie)
        {
            Border b = new Border();
            if (categorie.Id == 0)
                b.BorderBrush = new SolidColorBrush(Colors.Blue);
            else
                b.BorderBrush = new SolidColorBrush(Colors.LightGray);


            b.BorderThickness = new Thickness(1);
            b.CornerRadius = new CornerRadius(5);
            b.Tag = categorie;
            b.Margin = new Thickness(5);
            b.Width = 100;
            b.MouseLeftButtonUp += new MouseButtonEventHandler(btnCategorie_MouseLeftButtonUp);


            TextBlock text = new TextBlock();
            text.Padding = new Thickness(10);
            text.Text = categorie.Nom;
            // text.Height = 25;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            //text.Margin = new Thickness(5);
            b.Child = text;

            spCategories.Children.Add(b);
        }

        /// <summary>
        /// Événement se produidant lors du clique sur un bouton catégorie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCategorie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //On remet la couleur d'origine aux bordures de toutes les catégories.
            Border border;
            foreach (var child in spCategories.Children)
            {
                border = (Border)child;
                border.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }

            //On change la couleur de la bordure sélectionnée.
            border = (Border)sender;
            border.BorderBrush = new SolidColorBrush(Colors.Blue);


            //todo : Filtrer la liste des produits selon la catégorie.
          
        }

        private void spAjouterProduit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FormProduit frmProduit = new FormProduit();
            frmProduit.ShowDialog();
            if (frmProduit.DialogResult == true)
            {
                ProduitFacture produitFactureNew = new ProduitFacture(frmProduit.ProduitAjoutModif, frmProduit.ProduitAjoutModif.Prix, 1);
            }
            
            
        }
    }
}
