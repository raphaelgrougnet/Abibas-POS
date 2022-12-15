using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Media.Imaging;
using TP3_420_14B_FX.classes;
using TP3_420_14B_FX.enums;

namespace TP3_420_14B_FX
{
    /// <summary>
    /// Logique d'interaction pour FormProduit.xaml
    /// </summary>
    public partial class FormProduit : Window
    {
        private Produit _produitAjoutModif;
        
        public Produit ProduitAjoutModif
        {
            get { return _produitAjoutModif; }
            private set { _produitAjoutModif = value; }
        }

        private List<Categorie> _categories;

        private EtatFormulaire _etatFormulaire;

        

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="etat">Etat du formulaire</param>
        /// <param name="pProduit">Produit a modifier ou ajouter</param>
        public FormProduit(EtatFormulaire etat = EtatFormulaire.Ajouter, Produit pProduit = null)
        {
            InitializeComponent();
            _categories = GestionFacture.ObtenirListeCategories();
            ProduitAjoutModif = pProduit;
            _etatFormulaire = etat;


            foreach (Categorie categorie in _categories)
            {
                cboCategories.Items.Add(categorie);
            }

            btnAjouterModifier.Content = etat.ToString();
            lblTitre.Text = etat.ToString() + " un produit";


            


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
            List<Categorie> categorie = GestionFacture.ObtenirListeCategories();
            cboCategories.Items.Clear();
            categorie.ForEach(c => { cboCategories.Items.Add(c); });
            if (ProduitAjoutModif != null)
            {
                txtCode.Text = ProduitAjoutModif.Code;
                txtNom.Text = ProduitAjoutModif.Nom;
                txtPrix.Text = ProduitAjoutModif.Prix.ToString();
                cboCategories.SelectedItem = ProduitAjoutModif.Categorie;
                imgProduit.Source = new BitmapImage(new Uri(ProduitAjoutModif.Image));
                BitmapImage biImageAlbum = new BitmapImage();
                biImageAlbum.BeginInit();
                biImageAlbum.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                biImageAlbum.UriSource = new Uri(ProduitAjoutModif.Image);
                biImageAlbum.CacheOption = BitmapCacheOption.OnLoad;
                biImageAlbum.EndInit();
                cboCategories.SelectedItem = ProduitAjoutModif.Categorie;

                imgProduit.Source = biImageAlbum;
            }

            
        }


        /// <summary>
        /// Valide le produit, envoie un message d'erreur si invalide
        /// </summary>
        /// <returns>True si valide, False sinon</returns>
        private bool ValiderProduit()
        {
            string message = "";
            if (String.IsNullOrWhiteSpace(txtCode.Text) || 
                txtCode.Text.Trim().Length < Produit.CODE_NB_CARAC_MIN ||
                txtCode.Text.Trim().Length > Produit.CODE_NB_CARAC_MAX)
            {
                message += $"- Le code du produit ne peut pas être nul ou vide et doit contenir entre {Produit.CODE_NB_CARAC_MIN} et {Produit.CODE_NB_CARAC_MAX} caractères.\n";
            }
            if (String.IsNullOrWhiteSpace(txtNom.Text) ||
                txtNom.Text.Trim().Length < Produit.NOM_NB_CARAC_MIN ||
                txtNom.Text.Trim().Length > Produit.NOM_NB_CARAC_MAX)
            {
                message += $"- Le nom du produit ne peut pas être nul ou vide et doit contenir entre {Produit.NOM_NB_CARAC_MIN} et {Produit.NOM_NB_CARAC_MAX} caractères.\n";
            }
            if (string.IsNullOrWhiteSpace(txtPrix.Text) || 
                !Decimal.TryParse(txtPrix.Text, out decimal result) ||
                Decimal.Parse(txtPrix.Text) < Produit.PRIX_MIN_VAL)
            {
                message += $"- Le prix ne peut pas être nul, doit être un nombre et doit être supérieur à {Produit.PRIX_MIN_VAL}.\n";
            }
            if (cboCategories.SelectedIndex == -1)
            {
                message += "- La catégorie doit être sélectionnée.\n";
            }
            if (imgProduit.Source is null)
            {
                message += "Vous devez sélectionner une image pour le produit.";
            }
                
            if (message != "")
            {
                MessageBox.Show(message, "Validation du produit",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            return true;
            
            
        }
        
        /// <summary>
        /// Fonction qui creer le produit si il est valide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterModifier_Click(object sender, RoutedEventArgs e)
        {
            if (ProduitAjoutModif == null)
            {
                if (ValiderProduit())
                {
                    uint id = 0;
                    string code = txtCode.Text.Trim().ToUpper();
                    string nom = txtNom.Text.Trim();
                    Categorie categorie = (Categorie)cboCategories.SelectedItem;
                    decimal prix = Decimal.Parse(txtPrix.Text);
                    BitmapImage imgProd = imgProduit.Source as BitmapImage;
                    string cheminFichier = imgProd.UriSource.LocalPath;
                    string image = Path.GetFileName(cheminFichier);

                    string ext = Path.GetExtension(image);
                    image = Guid.NewGuid() + ext;
                    File.Copy(cheminFichier, GestionFacture.CHEMIN_IMAGES_PRODUITS + image, true);
                    
                    ProduitAjoutModif = new Produit(id, code, nom, categorie, prix, image);
                    DialogResult = true;
                }
            }
            else
            {
                if (ValiderProduit())
                {
                    ProduitAjoutModif.Code = txtCode.Text.Trim().ToUpper();
                    ProduitAjoutModif.Nom = txtNom.Text.Trim();
                    ProduitAjoutModif.Categorie = (Categorie)cboCategories.SelectedItem;
                    ProduitAjoutModif.Prix = Decimal.Parse(txtPrix.Text);
                    BitmapImage biImageProd = imgProduit.Source as BitmapImage;
                    string cheminImage = biImageProd.UriSource.LocalPath;
                    if (cheminImage != GestionFacture.CHEMIN_IMAGES_PRODUITS + ProduitAjoutModif.Image)
                    {
                        string ext = Path.GetExtension(cheminImage);
                        string image = Path.GetFileNameWithoutExtension(ProduitAjoutModif.Image);
                        image += ext;

                        File.Copy(cheminImage, GestionFacture.CHEMIN_IMAGES_PRODUITS + image, true);

                        ProduitAjoutModif.Image = image;
                    }
                    DialogResult = true;
                }
            }
            
        }
            
        
        /// <summary>
        /// Fonction qui ferme le formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Voulez-vous annuler?", "Annuler", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DialogResult = false;
            }
            
        }

        /// <summary>
        /// Fonction qui permet d'envoyer une image au formulaire, pour le produuit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAjouterImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choisissez une image";
            openFileDialog.Filter = "Fichier JPG (*.jpg)|*.jpg|Fichier JPEG (*.jpeg)|*.jpeg|Fichier PNG (*.png)|*.png";

            if ((bool)openFileDialog.ShowDialog())
            {
                string ficher = openFileDialog.FileName;

                BitmapImage biProd = new BitmapImage();

                biProd.BeginInit();
                biProd.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                biProd.UriSource = new Uri(ficher);
                biProd.CacheOption = BitmapCacheOption.OnLoad;
                biProd.EndInit();

                imgProduit.Source = biProd;
            }
        }
    }
}
