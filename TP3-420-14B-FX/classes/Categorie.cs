#region USING

using System;

#endregion


namespace TP3_420_14B_FX.classes
{
    /// <summary>
    /// Classe représentant une catégorie
    /// </summary>
    public class Categorie
    {

        #region ATTRIBUTS
        /// <summary>
        /// Identifiant unique de la catégorie
        /// </summary>
        private uint _id;

        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        private string _nom;

        #endregion


        #region PROPRIÉTÉS ET INDEXEURS

        /// <summary>
        /// Obtient ou définit l'identifiant unique de la catégorie
        /// </summary>
        public uint Id
        {
            get { return this._id; }
            set { this._id = value; }
        }


        /// <summary>
        /// Obtient ou définit le nom de la catégorie
        /// </summary><
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le nom et nul ou vide</exception>
        public string Nom
        {
            get { return this._nom; }
            set
            {
                //todo: implémenter validation du nom
                _nom = value;
            }
        }

        #endregion


        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur paramètré
        /// </summary>
        /// <param name="id">Identifiant de la catégorie</param>
        /// <param name="nom">Nom de la catégorie</param>
        public Categorie(uint id, string nom)
        {
            this.Nom = nom;
            this.Id = id;
        }

        #endregion


        #region MÉTHODES


        /// <summary>
        /// Représentation de l'objet sous forme de chaîne de carcatère.
        /// </summary>
        /// <returns>Retourne le nom de la catégorie</returns>
        public override string ToString()
        {
            return this.Nom;
        }

        /// <summary>
        /// Permet de vérifier si deux objets de type Categorie sont égaux.
        /// </summary>
        /// <param name="obj">Objet de type Categorie à comparer avec l'objet courant</param>
        /// <returns>true si les deux objets sont égaux; false, autrement.</returns>
        /// <remarks>Deux catégories sont égales si elle ont le même nom.</remarks>
        public override bool Equals(Object obj)
        {
            //todo : Implémenter Equals pour Catégorie
            //throw new NotImplementedException();
            return true;
        }

        #endregion


    }
}
