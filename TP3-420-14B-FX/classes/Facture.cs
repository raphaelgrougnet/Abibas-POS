
#region USING
using System;
using System.Collections.Generic;
using System.ComponentModel;
#endregion


namespace TP3_420_14B_FX.classes
{
    /// <summary>
    /// Classe représentant une facture
    /// </summary>
    public class Facture
    {
        #region CONSTANTES


        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Identifiant unique de la facture
        /// </summary>
        private uint _id;

        /// <summary>
        /// Date de création de la facture
        /// </summary>
        private DateTime _dateCreation;

        /// <summary>
        /// Liste des produits dans la 
        /// </summary>
        private List<ProduitFacture> _produitsFacture;



        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit l'identifiant unique de la facture
        /// </summary>
        public uint Id
        {
            get { return this._id; }
            set
            {
                this._id = value;
            }
        }


        /// <summary>
        /// Obtient ou définit la date de création de la facture
        /// </summary>
        public DateTime DateCreation
        {
            get { return this._dateCreation; }
            set { this._dateCreation = value; }
        }


        /// <summary>
        /// Obtient ou définit la liste des produits faisants partie de la facture
        /// </summary>
        public List<ProduitFacture> ProduitsFacture
        {
            get { return this._produitsFacture; }
            private set { this._produitsFacture = value; }
        }


        /// <summary>
        /// Obtient le montant total de la facture
        /// </summary>
        public decimal MontantSousTotal
        {
            get
            {


                //Todo: Implémenter le calcul du sous-total de la liste de produits FAIT
                decimal montantSousTotal = 0;
                foreach (ProduitFacture produit in this.ProduitsFacture)
                {
                    montantSousTotal += produit.SousTotal;
                }
                return montantSousTotal;

                


            }

        }



        /// <summary>
        /// Obtient le montant la TPS de la facture
        /// </summary>
        public decimal MontantTPS
        {
            get
            {
                //todo : Implémenter le calcul du montant de TPS de la facture
                decimal montantTPS = 0;
                montantTPS = this.MontantSousTotal * 0.05m;
                return montantTPS;
            }
            

        }


        /// <summary>
        /// Obtient le montant de la TVQ de la facture
        /// </summary>
        public decimal MontantTVQ
        {
            get
            {
                //todo : Implémenter le calcul du montant de TVQ de la facture FAIT
                decimal montantTVQ = 0;
                montantTVQ = this.MontantSousTotal * 0.09975m;
                return montantTVQ;
            }

        }


        /// <summary>
        /// Obtient le montant total de la facture inclunant le montant des taxes
        /// </summary>
        public decimal MontantTotal
        {
            get
            {
                //todo : Implémenter le calcul du montant total de la facture FAIT
                decimal montantTotal = 0;
                montantTotal = this.MontantSousTotal + this.MontantTPS + this.MontantTVQ;
                return montantTotal;
            }

        }

        #endregion;

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur sans paramètre
        /// </summary>
        /// <remarks>Initialise une liste de produits vide</remarks>
        public Facture()
        {
            //todo : Implémenter le consturcteur sans  paramètre de Facture. FAIT
            this.ProduitsFacture = new List<ProduitFacture>();
            
        }

        /// <summary>
        /// Constructeur avec paramètere. 
        /// </summary>
        /// <remarks>Initialise une facture à partir d'un id et d'une date. Initialise une liste de produit vide.</remarks>
        /// <param name="id">Identifiant de la facture</param>
        /// <param name="dateCreation">Date de créatoin de la facture</param>
        public Facture(uint id, DateTime dateCreation)
        {
            //todo : Implémenter le consturcteur avec  paramètre de Facture. FAIT
            this.Id = id;
            this.DateCreation = dateCreation;
            this.ProduitsFacture = new List<ProduitFacture>();
            

        }


        #endregion

        #region MÉTHODES


        /// <summary>
        /// Permet d'ajout un produit à une facture
        /// </summary>
        /// <param name="produit">Le produit à ajouter à la facture</param>
        /// <param name="prixUnitaire">Le prix unitaire du produit ajouter à la facture</param>
        /// <param name="quantite">La quantite du produit achetée</param>
        /// <remarks>Si le produit existe déjà dans la facture alors la quantité est ajouté au produit existant</remarks>
        /// <exception cref="System.ArgumentNullException">Lancée si le produit est null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée si la prix unitaire est inférieur à ProduitFacture.PRIX_MIN_VAL</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée si la quantité inférieur à ProduitFacture.QUANTITE_MIN_VAL</exception>
        public void AjouterProduit(Produit produit, decimal prixUnitaire, uint quantite)
        {
            bool trouve = false;
            //todo : Implémenter AjouterProduit
            if (produit == null)
            {
                throw new ArgumentNullException("Produit","Le produit ne peut pas être null");
            }
            if (prixUnitaire < ProduitFacture.PRIX_MIN_VAL)
            {
                throw new ArgumentOutOfRangeException("Produit","Le prix unitaire ne peut pas être inférieur à 0");
            }
            if (quantite < ProduitFacture.QUANTITE_MIN_VAL)
            {
                throw new ArgumentOutOfRangeException("Produit","La quantité ne peut pas être inférieur à 0");
            }
            ProduitFacture produitFacture = new ProduitFacture(produit, prixUnitaire, quantite);
            foreach (ProduitFacture pFacture in ProduitsFacture)
            {
                if (pFacture.Produit == produitFacture.Produit)
                {
                    pFacture.Quantite += 1;

                    trouve = true;
                }
                
            }
            if(!trouve)
            {
                ProduitsFacture.Add(produitFacture);
            }
            
            


        }

        /// <summary>
        /// Permet retirer un produit d'une facture.
        /// </summary>
        /// <param name="produit">Le produit à retirer de la facture</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul</exception>
        ///  <exception cref="System.InvalidOperationException">Lancée lorsque le produit n'existe pas dans la facture</exception>
        public void RetirerProduit(Produit produit)
        {
            //todo : Implémenter RetirerProduit FAIT
            bool trouve = false;
            ProduitFacture pFac = null;
            if (produit is null)
            {
                throw new ArgumentNullException("Produit","Le produit ne peut pas être null");
            }
            

            foreach (ProduitFacture pFacture in _produitsFacture)
            {
                if (pFacture.Produit == produit)
                {

                    pFac = pFacture;
                    trouve = true;
                    
                }
                
            }
            _produitsFacture.Remove(pFac);
            
            if (!trouve)
            {
                throw new InvalidOperationException("Le produit n'existe pas dans la facture");
            }

            #endregion

        }
    }
}
