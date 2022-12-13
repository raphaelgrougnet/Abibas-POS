using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media;

namespace TP3_420_14B_FX.classes
{
    public static class GestionFacture
    {
        #region CONSTANTES

        public const string CONNEXION = "MySQL";

        public const string CHEMIN_IMAGES_PRODUITS = @"C:\data-420-14B-FX\data-tp3-420-14b\Images\";

        #endregion

        #region CONSTRUCTEURS


        #endregion

        #region MÉTHODES

        private static MySqlConnection CreerConnection()
        {
            string connexion = ConfigurationManager.ConnectionStrings[CONNEXION].ConnectionString;

            return new MySqlConnection(connexion);
        }

        public static void FermerConnection(MySqlConnection cn)
        {
            if (cn.State == System.Data.ConnectionState.Open)
                cn.Close();
        }

        /// <summary>
        /// Permet d'obtenir liste des catégories provenant de la base de données
        /// </summary>
        /// <returns>Liste de Categorie</returns>
        public static List<Categorie> ObtenirListeCategories()
        {

            //todo : Implémenter ObtenirListeCategories FAIT 
            MySqlConnection cn = CreerConnection();

            List<Categorie> categories = new List<Categorie>();
            try
            {
                cn.Open();

                string requete = "SELECT id, nom FROM categories ORDER BY id";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categorie categorie = new Categorie(dr.GetUInt32(0), dr.GetString(1));
                    categories.Add(categorie);
                }

                dr.Close();

                return categories;
            }
            catch(Exception) { throw; }
            finally { FermerConnection(cn); }

        }

        /// <summary>
        /// Permet l'ajout d'un produit à la base de données
        /// </summary>
        /// <param name="produit">Produit à ajouter</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul.</exception>
        public static void AjouterProduit(Produit produit)
        {
            //todo : Implémenter AjouterProduit FAIT 
            if (produit is null)
                throw new ArgumentNullException("produit", "Une produit ne peut pas être nul");

            MySqlConnection cn = CreerConnection();

            try
            {
                cn.Open();

                string requete = $"INSERT INTO produits (Id, Code, Nom, Prix, Image, IdCategorie) VALUES(@id, @code, @nom, @prix, @image, @idCategorie)";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", produit.Id);
                cmd.Parameters.AddWithValue("@code", produit.Code);
                cmd.Parameters.AddWithValue("@nom", produit.Nom);
                cmd.Parameters.AddWithValue("@prix", produit.Prix);
                cmd.Parameters.AddWithValue("@image", produit.Image);
                cmd.Parameters.AddWithValue("@idCategorie", produit.Categorie.Id);

                cmd.ExecuteNonQuery();

            }
            catch(Exception) 
            {
                throw; 
            }
            finally 
            { 
                FermerConnection(cn); 
            }

        }

        /// <summary>
        /// Permet d'obtenir la liste des produits dans la base de données selon certains critères de recherche.
        /// </summary>
        /// <param name="nomProduit">Partie du nom du produit à chercher</param>
        /// <param name="categorie">Catégorie du produit</param>
        /// <returns>La liste des produits trouvés selon les critères de recherche. Si aucun critère n'est spécifié alors on retourne tous les produits</returns>
        /// <remarks>La liste des produits est toujours triée en ordre croissant de nom.</remarks>
        public static List<Produit> ObtenirListeProduits(string nomProduit = "", Categorie categorie = null)
        {
            //todo : Implémenter ObtenirListeProduits
            MySqlConnection cn = CreerConnection();

            List<Produit> produits = new List<Produit>();

            List<Categorie> categories = new List<Categorie>();

            categories = ObtenirListeCategories();

            try
            {
                cn.Open();

                if (nomProduit == "" && categorie is null)
                {
                    string requete = "SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits ORDER BY Nom";
                    
                    MySqlCommand cmd = new MySqlCommand(requete, cn);

                    MySqlDataReader dr = cmd.ExecuteReader();
                    Categorie icategorie = null;

                    while(dr.Read())
                    {
                        uint idCate = dr.GetUInt32(5);
                        foreach(Categorie pCategorie in categories)
                        {
                            if(pCategorie.Id == idCate)
                                icategorie = pCategorie;
                        }
                        Produit produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), icategorie, dr.GetDecimal(3), CHEMIN_IMAGES_PRODUITS+dr.GetString(4));
                        produits.Add(produit);
                    }

                    dr.Close();
                }
                if (nomProduit != "" && categorie is null)
                {
                    string requete = "SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE Nom LIKE @nom ORDER BY Nom";

                    MySqlCommand cmd = new MySqlCommand(requete, cn);

                    cmd.Parameters.AddWithValue("@nom", "%"+nomProduit.ToLower() + "%");

                    MySqlDataReader dr = cmd.ExecuteReader();
                    Categorie icategorie = null;

                    while (dr.Read())
                    {
                        uint idCate = dr.GetUInt32(5);
                        foreach (Categorie pCategorie in categories)
                        {
                            if (pCategorie.Id == idCate)
                                icategorie = pCategorie;
                        }
                        Produit produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), icategorie, dr.GetDecimal(3), CHEMIN_IMAGES_PRODUITS + dr.GetString(4));
                        produits.Add(produit);
                    }

                    dr.Close();
                }
                if (nomProduit != "" && categorie != null)
                {
                    string requete = "SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE Nom LIKE @nom AND IdCategorie = @idCategorie ORDER BY Nom";

                    MySqlCommand cmd = new MySqlCommand(requete, cn);

                    cmd.Parameters.AddWithValue("@nom", "%"+nomProduit.ToLower()+"%");
                    cmd.Parameters.AddWithValue("@idCategorie", categorie.Id);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Produit produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), categorie, dr.GetDecimal(3), CHEMIN_IMAGES_PRODUITS + dr.GetString(4));
                        produits.Add(produit);
                    }

                    dr.Close();
                }
                if(nomProduit == "" && categorie != null)
                {
                    string requete = "SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE IdCategorie = @idCategorie ORDER BY Nom";

                    MySqlCommand cmd = new MySqlCommand(requete, cn);

                    cmd.Parameters.AddWithValue("@idCategorie", categorie.Id);

                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Produit produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), categorie, dr.GetDecimal(3), CHEMIN_IMAGES_PRODUITS + dr.GetString(4));
                        produits.Add(produit);
                    }

                    dr.Close();
                }

                return produits;
            }
            catch(Exception) 
            { 
                throw; 
            }
            finally 
            { 
                FermerConnection(cn);  
            }
        }

        /// <summary>
        /// Permet d'obtenir un produit dans la base de données à partir de son identifiant unique
        /// </summary>
        /// <param name="id">Identifiant unique du produit</param>
        /// <returns>Le produit trouvé ou Null si aucun produit trouvé.</returns>
        public static Produit ObtenirProduit(uint id)
        {

            ////todo : Implémenter ObtenirProduit FAIT

            List<Produit> produits = new List<Produit>();

            foreach(Produit produit in produits)
            {
                if(produit.Id == id)
                {
                    return produit;
                }
            }

            return null;
        }


        /// <summary>
        /// Permet de modifier un produit dans la base de donnnées
        /// </summary>
        /// <param name="produit">Produit à modifier</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul</exception>
        public static void ModifierProduit(Produit produit)
        {
            //todo : Implémenter ModifierProduit FAIT
            if (produit is null)
                throw new ArgumentNullException("produit", "Le produit ne peut pas être null");

            MySqlConnection cn = CreerConnection();

            try
            {
                cn.Open();

                string requete = "UPDATE produits SET Code = @code, Nom = @nom, Prix = @prix, Image = @image, IdCategorie = @idCategorie WHERE Id = @id";

                MySqlCommand cmd  = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", produit.Id);
                cmd.Parameters.AddWithValue("@code", produit.Code);
                cmd.Parameters.AddWithValue("@nom", produit.Nom);
                cmd.Parameters.AddWithValue("@prix", produit.Prix);
                cmd.Parameters.AddWithValue("@image", produit.Image);
                cmd.Parameters.AddWithValue("@idCategorie", produit.Categorie.Id);

                cmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                FermerConnection(cn);
            }
        }


        /// <summary>
        /// Permet de supprimer un produit dans la base de donnée
        /// </summary>
        /// <param name="produit">Le produite à supprimer</param>
        /// <remarks>L'image du produit est également supprimée</remarks>
        /// <exception cref="System.InvalidOperationException">Lancée lorque le produit existe dans au moins une facture</exception>
        public static void SupprimerProduit(Produit produit)
        {

            //todo : Implémenter SupprimerProduit

            MySqlConnection cn = CreerConnection();

            try
            {
                cn.Open();

                string requete = "DELETE FROM produits WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", produit.Id);
                cmd.Parameters.AddWithValue("@code", produit.Code);
                cmd.Parameters.AddWithValue("@nom", produit.Nom);
                cmd.Parameters.AddWithValue("@prix", produit.Prix);
                cmd.Parameters.AddWithValue("@image", produit.Image);
                cmd.Parameters.AddWithValue("@idCategorie", produit.Categorie.Id);

                cmd.ExecuteNonQuery();

                File.Delete(produit.Image);
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                throw new InvalidOperationException("Le produit existe déjà dans au moins une facture");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                
                FermerConnection(cn);
            }

        }


        /// <summary>
        /// Permet d'ajouter une facture dans la base de données
        /// </summary>
        /// <param name="facture">facture à ajouter</param>
        /// <exception cref="System.ArgumentNullException">Lancée si la facture est nulle.</exception>
        /// <exception cref="System.ArgumentNullException">Lancée si la liste des produitsFacture est nulle ou vide.</exception>
        public static void AjouterFacture(Facture facture)
        {

            if (facture is null)
            {
                throw new ArgumentNullException("Facture", "La facture ne peut pas être nulle");

            }

            if(facture.ProduitsFacture is null || facture.ProduitsFacture.Count == 0)
            {
                throw new ArgumentNullException("ProduitsFacture", "La liste des ProduitsFacture peut pas être nulle ou vide");
            }
            //todo : Implémenter AjouterFacture
            MySqlConnection cn = CreerConnection();


            try
            {
                cn.Open();

                string requete = "INSERT INTO factures(Id, Date, MontantSousTotal, MontantTPS, MontantTVQ, MontantTotal) VALUES (@id, @date, @st, @tps, @total)";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", facture.Id);
                cmd.Parameters.AddWithValue("@date", facture.DateCreation);
                cmd.Parameters.AddWithValue("@st", facture.MontantSousTotal);
                cmd.Parameters.AddWithValue("@tps", facture.MontantTPS);
                cmd.Parameters.AddWithValue("@total", facture.MontantTotal);
                

                cmd.ExecuteNonQuery();

                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

                FermerConnection(cn);
            }
        }

        /// <summary>
        /// Permet d'obtenir une facture dans la base de donnée à partir de son identifiant unique
        /// </summary>
        /// <param name="idFacture">Identifant uniquie de la facture</param>
        /// <remarks>Les produits faisant partie de la facture doivent être ajouté à la facture.</remarks>
        /// <returns>La facture trouvée. Null si aucune facture n'est trouvée</returns>
        public static Facture ObtenirFacture(uint idFacture)
        {
            //todo : Implémenter ObtenirFacture
            throw new NotImplementedException();
        }
        #endregion


    }
}
