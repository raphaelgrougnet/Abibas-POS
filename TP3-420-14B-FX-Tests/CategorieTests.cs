
using TP3_420_14B_FX.classes;
using Xunit;

namespace TP3_420_14B_FX_Tests
{
    public class CategorieTests
    {

        [Fact]
        public void Equals_Devrait_Retourner_True_Si_Categories_Identique()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Chaussures");
            Categorie categorie2 = new Categorie(1, "Chaussures");

            //Act
            bool egal = categorie1.Equals(categorie2);

            //Assert
            Assert.True(egal);
           

        }


        [Fact]
        public void Equals_Devrait_Retourner_False_Si_Categories_Diffentes()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Chaussures");
            Categorie categorie2 = new Categorie(1, "Pantalons");
            Categorie categorie3 = new Categorie(2, "Pantalons");

            //Act & Assert     
            Assert.False(categorie1.Equals(categorie2));
            Assert.False(categorie1.Equals(categorie3));



        }
    }
}
