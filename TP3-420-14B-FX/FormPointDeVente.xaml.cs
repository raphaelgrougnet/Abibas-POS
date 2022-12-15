using System;
using System.Collections.Generic;
using System.IO;
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
        private Facture _factureCourante;
        private Categorie _categorieSelect;

        /// <summary>
        /// Constructeur sans paramètre du Formulaire Point de Vente
        /// </summary>
        public FormPointDeVente()
        {
            InitializeComponent(); 
        }
        
        /// <summary>
        /// Fonction qui s'execute au lancement de l'application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialiserFormulaire();
           
        }

        /// <summary>
        /// Fonction qui initialise l'affichage de l'application
        /// </summary>
        private void InitialiserFormulaire()
        {
            Categorie Tous = new Categorie(0, "Tous");
            AjouterBoutonCategorie(Tous);
            _categorieSelect = Tous;
            AfficherListeCategorie();
            _factureCourante = new Facture();
            lstProduitsFacture.ItemsSource = _factureCourante.ProduitsFacture;
            AfficherListeProduits(GestionFacture.ObtenirListeProduits());
            AfficherMontant();
        }

        /// <summary>
        /// Affiche la liste de tous les produits classé en ordre alphabétiqie
        /// </summary>
        /// <param name="pListeProduit">Une liste de produits à afficher</param>
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
                

                BitmapImage bi = new BitmapImage(/*new Uri(GestionFacture.CHEMIN_IMAGES_PRODUITS+ produit.Image)*/);
                Image imageProd = new Image();
                BitmapImage biImageAlbum = new BitmapImage();
                biImageAlbum.BeginInit();
                biImageAlbum.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                biImageAlbum.UriSource = new Uri(produit.Image);
                biImageAlbum.CacheOption = BitmapCacheOption.OnLoad;
                biImageAlbum.EndInit();
                imageProd.Source = biImageAlbum;
                imageProd.Width = 120;
                imageProd.Height = 80;
                imageProd.HorizontalAlignment = HorizontalAlignment.Center;
                imageProd.Tag = produit;
                imageProd.MouseLeftButtonDown += new MouseButtonEventHandler(imgProd_MouseLeftButtonDown);


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

        /// <summary>
        /// Fonction qui s'execute quand on clique sur l'image de suppression d'un produit dans la liste de produits et supprime l'objet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgDelete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Produit prod = image.Tag as Produit;
            //TODO FAIT
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce produit ?", "Supprimer le produit" ,MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                
                GestionFacture.SupprimerProduit(prod);
                
                MessageBox.Show("Le produit à été supprimé", "Suppression du produit", MessageBoxButton.OK,MessageBoxImage.Information);
            }
            
            AfficherListeProduits(GestionFacture.ObtenirListeProduits());

        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur l'image de modification d'un produit dans la liste de produits
        /// et ouvre un nouveau formulaire pour modifier le produit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEdit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Produit prod = image.Tag as Produit;
            //TODO FAIT
            FormProduit frmProduit = new FormProduit(enums.EtatFormulaire.Modifier,prod);
            frmProduit.ShowDialog();
            if (frmProduit.DialogResult == true)
            {
                GestionFacture.ModifierProduit(frmProduit.ProduitAjoutModif);
                AfficherListeProduits(GestionFacture.ObtenirListeProduits());
            }
        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur l'image du produit, et ajoute a la facture courante, le produit 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgProd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO FAIT
            Image image = sender as Image;
            Produit prod = image.Tag as Produit;
            _factureCourante.AjouterProduit(prod, prod.Prix, 1);
            lstProduitsFacture.Items.Refresh();
            AfficherMontant();
            
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
            if ((border.Tag as Categorie).Id != 0)
            {
                List<Produit> listProd = GestionFacture.ObtenirListeProduits(txtRechercher.Text, border.Tag as Categorie);
                AfficherListeProduits(listProd);
            }
            else
            {
                List<Produit> listProd = GestionFacture.ObtenirListeProduits(txtRechercher.Text);
                AfficherListeProduits(listProd);
            }

            _categorieSelect = border.Tag as Categorie;
            
            
        }


        /// <summary>
        /// Fonction qui s'execute quand on clique sur la loupe de recherche dans la section facture, et recherche une facture en fonction de son id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgRechercherFacture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoFacture.Text) || !UInt32.TryParse(txtNoFacture.Text, out uint result))
            {
                MessageBox.Show("Impossible de trouver la facture, veulllez saisir un numéro de facture valide.", "Recherche d'une facture", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Facture fact = GestionFacture.ObtenirFacture(UInt32.Parse(txtNoFacture.Text));
                //Facture fact = new Facture();

                //fact.AjouterProduit(new Produit(1, "1111-111", "test", new Categorie(1, "pants"), 120M, "C:\\Users\\rapha\\Desktop\\TP3 Prog2\\TP3-420-14B-FX\\Resources\\edit.png"), 120M, 2);
                if (fact != null)
                {
                    _factureCourante= fact;
                    lstProduitsFacture.ItemsSource = null;
                    lstProduitsFacture.ItemsSource = _factureCourante.ProduitsFacture;
                    lblDateFacture.Text = "Date " + _factureCourante.DateCreation; 
                    wpProduits.IsEnabled= false;
                    
                    lstProduitsFacture.IsEnabled= false;
                    
                    

                }
                else
                {
                    MessageBox.Show("Impossible de charger la facture. Ce numéro de facture n'existe pas.", "Recherche d'une facture", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
            
        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur le texte Ajouter produit, et ouvre un nouveau formulaire pour ajouter un produit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spAjouterProduit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FormProduit frmProduit = new FormProduit();
            frmProduit.ShowDialog();
            if (frmProduit.DialogResult == true)
            {
                GestionFacture.AjouterProduit(frmProduit.ProduitAjoutModif);
                AfficherListeProduits(GestionFacture.ObtenirListeProduits());
            }
        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur le texte Nouvelle facture,
        /// Active tous les controls et vide la facture courante afin d'un creer une nouvelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spNouvelleFacture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if(MessageBox.Show("Voulez-vous vraiment créer une nouvelle facture", "Nouvelle Facture", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _factureCourante = new Facture();

                lstProduitsFacture.ItemsSource = null;
                lstProduitsFacture.ItemsSource = _factureCourante.ProduitsFacture;
                wpProduits.IsEnabled = true;
                lstProduitsFacture.IsEnabled = true;
                txtRechercher.Text = "";
                lblDateFacture.Text = "Date ";
                txtNoFacture.Text = "";
                btnEnregistrer.IsEnabled= true;
                AfficherMontant();
            }
            
        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur la loupe de recherche dans la section produits, 
        /// et recherche tous les produits correspondant au nom rentré et a sa catégorie selectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgRechercherProduit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List<Produit> listProd;
            if (_categorieSelect.Id == 0)
            {
                listProd = GestionFacture.ObtenirListeProduits(txtRechercher.Text);
            }
            else
            {
                listProd = GestionFacture.ObtenirListeProduits(txtRechercher.Text, _categorieSelect);
            }

            AfficherListeProduits(listProd);
        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur le boutton - d'un produit de la facture, et réduit sa quantité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetirerQte_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            ProduitFacture produitFacture = image.Tag as ProduitFacture;

            if (produitFacture.Quantite > 1)
            {
                produitFacture.Quantite -= 1;
            }
            else
            {
                _factureCourante.RetirerProduit(produitFacture.Produit);
            }
            lstProduitsFacture.Items.Refresh();
            AfficherMontant();
        }

        /// <summary>
        /// Fonction qui s'execute quand on clique sur le boutton + d'un produit de la facture, et augmente sa quantité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjouterQte_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            ProduitFacture produitFacture = image.Tag as ProduitFacture;

            _factureCourante.AjouterProduit(produitFacture.Produit, produitFacture.PrixUnitaire, 1);
            lstProduitsFacture.Items.Refresh();
            AfficherMontant();
        }

        private void AfficherMontant()
        {
            lblSousTotal.DataContext = null;
            lblSousTotal.DataContext = _factureCourante;
            lblTPS.DataContext = null;
            lblTPS.DataContext = _factureCourante;
            lblTVQ.DataContext = null;
            lblTVQ.DataContext = _factureCourante;
            lblTotal.DataContext = null;
            lblTotal.DataContext = _factureCourante;
        }

        private void EnregistrerFacture()
        {
            GestionFacture.AjouterFacture(_factureCourante);
            _factureCourante.DateCreation = DateTime.Now;
            txtNoFacture.Text = _factureCourante.Id.ToString();
            lblDateFacture.Text = "Date " + _factureCourante.DateCreation.ToString();
            lstProduitsFacture.IsEnabled = false;
            btnEnregistrer.IsEnabled = false;
            MessageBox.Show("Votre facture a bien été ajouté à la base de données", "Ajout Facture");
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            EnregistrerFacture();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lstProduitsFacture.Items.Count > 0 && !lstProduitsFacture.IsEnabled)
            {
                MessageBoxResult result = MessageBox.Show("Une facture est en cours d'utilisation, voulez-vous sauvegarder avant de quitter ?", "Quitter l'application", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    EnregistrerFacture();
                    
                }
                Close();
            }
        }
    }
}
