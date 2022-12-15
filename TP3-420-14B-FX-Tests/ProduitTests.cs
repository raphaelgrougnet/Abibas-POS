
using TP3_420_14B_FX.classes;
using System;
using Xunit;

namespace TP3_420_14B_FX_Tests
{
    public class ProduitTests
    {

        /// <summary>
        /// Permet d'obtenir une catégorie
        /// </summary>
        /// <returns>Catégorie créer</returns>
        private Categorie CreerCategorie()
        {
            return new Categorie(1, "Vêtements");
        }

     

        [Fact]
        public void SetCode_Devrait_Lancer_ArguementNullException_Quand_Null_Ou_Vide()
        {
            //Arrange
            //Arrange Act et Assert

            Categorie categorie = CreerCategorie();

            //Test code vide ou null
            Assert.Throws<ArgumentNullException>(() => new Produit(1,null,"T-Shirt", categorie,10,"t-shirt.png"));
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "", "T-Shirt", categorie, 10, "t-shirt.png"));
            Assert.Throws<ArgumentNullException>(() => new Produit(1, " ", "T-Shirt", categorie, 10, "t-shirt.png"));

        }

        [Fact]
        public void SetCode_Devrait_Retitrer_Espaces_Inutiles()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            string code = " TS123 ";
            string resultatAttendu = "TS123";
            
            //Act
            Produit produit = new Produit(1, code, "T-Shirt", categorie, 10, "t-shirt.png");

            //Assert
            Assert.Equal(resultatAttendu, produit.Code);

        }

        [Fact]
        public void SetNom_Devrait_Lancer_ArguementOutOfRangeException_Quand_Nbr_Caracteres_Pas_Entre_Min_et_Max()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            //Création d'un code avec moins de caractères que permis.
            string nomMin = new string('x', Produit.NOM_NB_CARAC_MIN - 1);

            //Création d'un code avec plus de caractères quie permis.
            string nomMax = new string('x', Produit.NOM_NB_CARAC_MAX + 1);
        

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Produit(1, "T1234", nomMin, categorie, 10, "t-shirt.png"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Produit(1, "T1234", nomMax, categorie, 10, "t-shirt.png"));

        }

        

        [Fact]
        public void SetNom_Devrait_Lancer_ArguementNullException_Quand_Null_Ou_Vide()
        {
            //Arrange
            //Arrange Act et Assert

            Categorie categorie = CreerCategorie();

            //Test code vide ou null
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "T1234", null, categorie, 10, "t-shirt.png"));
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "T1234", "", categorie, 10, "t-shirt.png"));
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "T1234", " ", categorie, 10, "t-shirt.png"));

        }

        [Fact]
        public void SetNom_Devrait_Retitrer_Espaces_Inutiles()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            string nom = " T-Shirt ";
            string resultatAttendu = "T-Shirt";

            //Act
            Produit produit = new Produit(1, "T1234", nom, categorie, 10, "t-shirt.png");

            //Assert
            Assert.Equal(resultatAttendu, produit.Nom);

        }

        [Fact]
        public void SetCode_Devrait_Lancer_ArguementOutOfRangeException_Quand_Nbr_Caracteres_Pas_Entre_Min_et_Max()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            //Création d'un code avec moins de caractères que permis.
            string codeMin = new string('x', Produit.CODE_NB_CARAC_MIN - 1);

            //Création d'un code avec plus de caractères quie permis.
            string codeMax = new string('x', Produit.CODE_NB_CARAC_MAX + 1);


            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Produit(1, codeMin, "T-Shirt", categorie, 10, "t-shirt.png"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Produit(1, codeMax, "T-Shirt", categorie, 10, "t-shirt.png"));

        }

        [Fact]
        public void SetCode_Devrait_Transformer_Le_Code_En_Majuscule()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            string code = "ts123";
            string resultatAttendu = "TS123";

            //Act
            Produit produit = new Produit(1, code, "T-Shirt", categorie, 10, "t-shirt.png");

            //Assert
            Assert.Equal(resultatAttendu, produit.Code);

        }



        [Fact]
        public void SetCategorie_Devrait_Lancer_ArguementNullException_Quand_Null()
        {
            //Arrange
            //Arrange Act et Assert

            Categorie categorie = CreerCategorie();

            //Test code vide ou null
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "TS123", "T-Shirt", null, 10, "t-shirt.png"));
           
        }

        [Fact]
        public void SetPrix_Devrait_Lancer_ArguementOutOfRangeException_Quand_Prix_Inferieur_A_Prix_Min()
        {
            //Arrange
            //Arrange Act et Assert

            Categorie categorie = CreerCategorie();

            //Création de prix sous la valeur minimal
            decimal prixMin = Produit.PRIX_MIN_VAL - 1;


            //Test code vide ou null
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "TS123", "T-Shirt", null, prixMin, "t-shirt.png"));

        }

        [Fact]
        public void SetImage_Devrait_Lancer_ArguementNullException_Quand_Null_Ou_Vide()
        {
            //Arrange
            //Arrange Act et Assert

            Categorie categorie = CreerCategorie();

            //Test code vide ou null
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "TS123", "T-Shirt", categorie, 10, null));
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "TS123", "T-Shirt", categorie, 10, ""));
            Assert.Throws<ArgumentNullException>(() => new Produit(1, "TS123", "T-Shirt", categorie, 10, " "));

        }


        [Fact]
        public void Constructeur_Devrait_Creer_Produit_Quand_Proprietes_Valides()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            uint id = 1;
            string code = "TS123";
            string nom = "T-Shirt";
            decimal prix = 10m;
            string image = "t-shirt.png";

            
            //Act
            Produit produit = new Produit(id, code, nom, categorie, prix, image);

            //Assert
            Assert.Equal(id, produit.Id);
            Assert.Equal(code, produit.Code);
            Assert.Equal(nom, produit.Nom);
            Assert.Equal(prix, produit.Prix);
            Assert.Equal(image, produit.Image);
           

        }


        [Fact]
        public void ToStrig_Devrait_Retourner_Le_Bon_Format()
        {
            //Arrange
            Categorie categorie = CreerCategorie();

            uint id = 1;
            string code = "TS123";
            string nom = "T-Shirt";
            decimal prix = 10m;
            string image = "t-shirt.png";

            string resultatAttendu = string.Format($"{code} {nom} {prix}");

            //Act
            Produit produit = new Produit(id, code, nom, categorie, prix, image);

            //Assert
            Assert.Equal(resultatAttendu, produit.ToString());
            


        }


        [Fact]
        public void Equals_Devrait_Retourner_True_Si_Produits_Identiques()
        {
            //Arrange
            Categorie categorie = CreerCategorie();


            //Act
            Produit produit1 = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            Produit produit2 = new Produit(produit1.Id, produit1.Code, produit1.Nom, produit1.Categorie, produit1.Prix, produit1.Image);

            //Assert
            Assert.True(produit1.Equals(produit2));

        }

        [Fact]
        public void Equals_Devrait_Retourner_False_Si_Produits_Differents()
        {
            //Arrange
            Categorie categorie = CreerCategorie();


            //Act
            Produit produit1 = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            Produit produit2 = new Produit(2, produit1.Code, produit1.Nom, produit1.Categorie, produit1.Prix, produit1.Image);

            Produit produit3 = new Produit(produit1.Id, "TS456", produit1.Nom, produit1.Categorie, produit1.Prix, produit1.Image);



            //Assert
            Assert.True(produit1.Equals(produit2));
            Assert.False(produit1.Equals(produit3));

        }



    }
}
