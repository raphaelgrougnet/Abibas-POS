using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            AfficherListeProduits(GestionFacture.ObtenirListeProduits());
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        private void AfficherListeProduits(List<Produit> pListeProduit)
        {
            //_listeProduit = new List<Produit>() { new Produit(0, "123-4567", "Chaussures Cali de UGG", new Categorie(0, "Chaussures"), 150.55M, @"C:\data-420-14B-FX\data-tp3-420-14b\Images\6f6eec5c-3f7b-496d-8ee4-7a3864e4fe2c.jpg") };/*GestionFacture.ObtenirListeProduits();*/
            wpProduits.Children.Clear();
            

            foreach (Produit produit in pListeProduit)
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Gray);
                border.BorderThickness = new Thickness(1);
                border.CornerRadius = new CornerRadius(5);
                border.Margin = new Thickness(5);
                border.Padding = new Thickness(10);
                

                StackPanel stackPanel = new StackPanel();
                stackPanel.VerticalAlignment = VerticalAlignment.Center;
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Width = 90;
                stackPanel.Height = 250;
                

                BitmapImage bi = new BitmapImage(new Uri(GestionFacture.CHEMIN_IMAGES_PRODUITS+ produit.Image));
                Image imageProd = new Image();
                imageProd.Source = bi;
                imageProd.Width = 120;
                imageProd.Height = 80;
                imageProd.HorizontalAlignment = HorizontalAlignment.Center;
                imageProd.Tag = produit;
                imageProd.MouseLeftButtonDown += new MouseButtonEventHandler(imgEdit_MouseLeftButtonDown);


                TextBlock txtNomProd = new TextBlock();
                txtNomProd.Text = produit.Nom;
                txtNomProd.FontSize = 15;
                txtNomProd.TextWrapping = TextWrapping.Wrap;
                txtNomProd.TextAlignment = TextAlignment.Left;
                txtNomProd.Padding = new Thickness(5, 7, 5, 7);
                txtNomProd.HorizontalAlignment = HorizontalAlignment.Center;
                txtNomProd.Height = 60;
                

                TextBlock txtPrixProd = new TextBlock();
                txtPrixProd.Text = produit.Prix + " $";
                txtPrixProd.FontSize = 15;
                txtPrixProd.FontWeight = FontWeights.Bold;
                txtPrixProd.Foreground = new SolidColorBrush(Colors.Green);
                txtPrixProd.TextAlignment = TextAlignment.Right;
                txtPrixProd.Margin = new Thickness(0, 50, 4, 5);
                txtPrixProd.Height = 25;
                

                StackPanel spImgEditSuppr = new StackPanel();
                spImgEditSuppr.Orientation = Orientation.Horizontal;
                spImgEditSuppr.HorizontalAlignment= HorizontalAlignment.Center;
                

                Image imgEdit = new Image();
                bi = new BitmapImage(new Uri(@"\Resources\edit.png", UriKind.Relative));
                imgEdit.Source = bi;
                imgEdit.Width = 30;
                imgEdit.MouseLeftButtonDown += new MouseButtonEventHandler(imgEdit_MouseLeftButtonDown);
                imgEdit.Tag= produit;

                Image imgDelete = new Image();
                bi = new BitmapImage(new Uri(@"\Resources\delete.png",UriKind.Relative));
                imgDelete.Source = bi;
                imgDelete.Width = 26;
                imgDelete.MouseLeftButtonDown += new MouseButtonEventHandler(imgDelete_MouseLeftButtonDown);
                imgDelete.Tag= produit;

                spImgEditSuppr.Children.Add(imgEdit);
                spImgEditSuppr.Children.Add(imgDelete);

                stackPanel.Children.Add(imageProd);
                stackPanel.Children.Add(txtNomProd);
                stackPanel.Children.Add(txtPrixProd);
                stackPanel.Children.Add(spImgEditSuppr);

                border.Child= stackPanel;

                wpProduits.Children.Add(border);


            }
            
            
        }

        private void imgDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Produit prod = image.Tag as Produit;
            //TODO À REVOIR
            GestionFacture.SupprimerProduit(prod);
            throw new NotImplementedException();
            AfficherListeProduits();
            
        }

        private void imgEdit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Produit prod = image.Tag as Produit;
            //TODO
            throw new NotImplementedException();
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
        /// Affiche la liste des catégories
        /// </summary>
        private void AfficherListeCategorie()
        {
            List<Categorie> _listeCategories = GestionFacture.ObtenirListeCategories();
            _listeCategories.ForEach(categorie => { AjouterBoutonCategorie(categorie); });
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
            List<Produit> listProd = GestionFacture.ObtenirListeProduits("",border.Tag as Categorie);
            AfficherListeProduits(listProd);
            
        }

        private void spAjouterProduit_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FormProduit frmProduit = new FormProduit();
            frmProduit.ShowDialog();
            if (frmProduit.DialogResult == true)
            {
                GestionFacture.AjouterProduit(frmProduit.ProduitAjoutModif);
                AfficherListeProduits(GestionFacture.ObtenirListeProduits());
            }


        }


        
    }
}
