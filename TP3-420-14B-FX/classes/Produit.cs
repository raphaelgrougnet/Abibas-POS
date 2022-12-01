
#region USING
using System;

#endregion

namespace TP3_420_14B_FX.classes
{
    /// <summary>
    /// Classe représentant un produit
    /// </summary>
    public class Produit
    {

        #region CONSTANTES

        public const int NOM_NB_CARAC_MIN = 3;
        public const int NOM_NB_CARAC_MAX = 30;

        public const int CODE_NB_CARAC_MIN = 5;
        public const int CODE_NB_CARAC_MAX = 10;

        public const int PRIX_MIN_VAL = 0;

        #endregion

        #region ATTRIBUTS


        /// <summary>
        /// Identifiant unique du produit
        /// </summary>
        private uint _id;

        /// <summary>
        /// Code du produit
        /// </summary>
        private string _code;

        /// <summary>
        ///Nom du produit
        /// </summary>
        private string _nom;

        /// <summary>
        /// Catégorie du produit
        /// </summary>
        private Categorie _categorie;

       
        /// <summary>
        /// Prix de vente du produit
        /// </summary>
        private decimal _prix;

        /// <summary>
        /// Nom de l'image du produit
        /// </summary>
        private string _image;

        #endregion

        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit l'indentifiant unique du produit.
        /// </summary>
        public uint Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// Obtient ou définit le code du produit.
        /// </summary>
        /// <remarks>Le code doit toujours être en majuscule et sans espaces inutiles</remarks>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le code est nul ou vide</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque le code ne contient pas entre CODE_NB_CARC_MIN et CODE_NB_CARC_MAX</exception>

        public string Code
        {
            get { return this._code; }
            set 
            {
                //Todo: Implémenter validation code produit. FAIT
                if (value.Trim().Length < CODE_NB_CARAC_MIN ||value.Trim().Length > CODE_NB_CARAC_MAX)
                {
                    throw new ArgumentOutOfRangeException("Code", 
                                                         $"La longeur du code doit être comprise entre {CODE_NB_CARAC_MIN} et {CODE_NB_CARAC_MAX} caratères");
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Code", "Le code ne peut pas être nul ou vide");
                }
                _code = value.Trim().ToUpper();
            }

        }

        /// <summary>
        /// Obtient ou définit le nom du produit
        /// </summary>
        /// <remarks>Le nom ne doit pas contenir d'espaces inutiles</remarks>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le nom est nul ou vide</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque le nom ne contient pas entre NOM_NB_CARC_MIN et NOM_NB_CARC_MAX</exception>
        public string Nom
        {
            get { return this._nom; }
            set 
            {
                //todo: implémenter validation nom; FAIT

                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Nom", "Le nom ne peut pas être nul ou vide");
                }
                if (value.Trim().Length < NOM_NB_CARAC_MIN || value.Trim().Length > NOM_NB_CARAC_MAX)
                {
                    throw new ArgumentOutOfRangeException("Nom", $"La longeur du nom doit être comprise entre {NOM_NB_CARAC_MIN} et {NOM_NB_CARAC_MAX} caratères");
                }
                _nom = value.Trim();
            }
        }

        /// <summary>
        /// Obtient ou définit la catérorie à laquelle appartient le produit
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque la catégorie est nulle</exception>
        public Categorie Categorie
        {
            get { return this._categorie; }
            set 
            {
                //todo: implémenter validation catégorie FAIT
                if (value is null)
                {
                    throw new ArgumentNullException("Categorie", "La catégorie ne peut pas être nulle");
                }
                _categorie = value;
            }
        }


        /// <summary>
        /// Obtient ou définit le prix de vente du produit
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancées lorsque le prix est inférieur ou égale à 0</exception>
        public decimal Prix
        {
            get { return this._prix; }
            set
            {
                //todo: implémenter validation prix FAIT
                if(value <= PRIX_MIN_VAL)
                {
                    throw new ArgumentOutOfRangeException("Prix", $"Le prix doit être strictement supérieur à {PRIX_MIN_VAL} $");
                }
                _prix = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le nom du fichier image du produit.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque l'image est nulle ou vide</exception>
        public String Image
        {
            get { return this._image; }
            set 
            {
                //todo: implémenter validation image FAIT
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Image", "L'image ne peut pas être nulle");
                }
                _image = value;


            }
        }

        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// Permet de construire un nouveau produit
        /// </summary>
        /// <param name="code">identifiant unique du produit</param>
        /// <param name="nom">Nom du produit</param>
        /// <param name="categorie">Catégorie du produit</param>
        /// <param name="prix">Prix de vente du produit</param>
        /// <param name="image">Nom du fichier image du produit</param>
        public Produit(uint id, string code, String nom, Categorie categorie, decimal prix, string image)
        {
            //todo: implémenter Constructeur Produit
            throw new NotImplementedException();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir une représentation du produit.
        /// </summary>
        /// <returns>Une représentation de produit sous forme de chaîne de caractères.</returns>
        public override string ToString()
        {

            return string.Format($"{this.Code} {this.Nom} {this.Prix}");
        }

        /// <summary>
        /// Permet de vérifier si deux objets de type Produit sont égaux.
        /// </summary>
        /// <param name="obj">Objet de type Produit à comparer avec l'objet courant</param>
        /// <returns>true si les deux objets sont égaux; false, autrement. Deux produits sont égaux si leur id et leur code sont identiques</returns>
        /// <remarks>
        /// Redéfinition de la méthode de la classe Object qui compare les références des objets uniquement.
        /// </remarks>
        public override bool Equals(Object obj)
        {
            //todo : Implémenter Equals pour Produit
            throw new NotImplementedException();


        }



        #endregion

    } // Fin de la classe
} // Fin du namespace