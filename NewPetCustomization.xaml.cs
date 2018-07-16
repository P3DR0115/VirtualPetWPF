using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VirtualPetWPF
{
    /// <summary>
    /// Interaction logic for NewPetCustomization.xaml
    /// </summary>
    public partial class NewPetCustomization : Page
    {
        public NewPetCustomization()
        {
            InitializeComponent();

            // Display all the species options
            for (int j = 0; j < MainWindow.OGPet.speciesList.Length; j++)
                SpeciesPicker.Items.Add(MainWindow.OGPet.speciesList[j]);

            // Display all the color options
            for (int i = 0; i < MainWindow.OGPet.colorsList.Length; i++)
                ColorPicker.Items.Add(MainWindow.OGPet.colorsList[i]);

            GenderPicker.Items.Add("Male");
            GenderPicker.Items.Add("Female");
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Assign Pet Information!
            {
                MainWindow.OGPet.species = Convert.ToString(SpeciesPicker.SelectionBoxItem);
                MainWindow.OGPet.breed = Convert.ToString(BreedPicker.SelectionBoxItem);
                MainWindow.OGPet.color = Convert.ToString(ColorPicker.SelectionBoxItem);
                MainWindow.OGPet.name = Convert.ToString(NameTxtBx.Text);
                MainWindow.OGPet.gender = Convert.ToString(GenderPicker.SelectionBoxItem);

                // Assign Size
                if (MainWindow.OGPet.species == "Hamster")
                    MainWindow.OGPet.size = "Tiny";
                else if (MainWindow.OGPet.species == "Cat")
                    MainWindow.OGPet.size = "Small";
                else if (MainWindow.OGPet.species == "Dinosaur")
                    MainWindow.OGPet.size = "Small";
                else if (MainWindow.OGPet.species == "Bird" && (MainWindow.OGPet.breed == "Eagle" || MainWindow.OGPet.breed == "Hawk"))
                    MainWindow.OGPet.size = "Medium";
                else if (MainWindow.OGPet.species == "Bird")
                    MainWindow.OGPet.size = "Small";
                else if (MainWindow.OGPet.species == "Dog" && (MainWindow.OGPet.breed == "Wire Hair Fox Terrier" || MainWindow.OGPet.breed == "French Bulldog"))
                    MainWindow.OGPet.size = "Medium";
                else if (MainWindow.OGPet.species == "Dog")
                    MainWindow.OGPet.size = "Large";

                // Assign Stamina values
                if(MainWindow.OGPet.size == "Tiny")
                {
                    MainWindow.OGPet.maxStamina = 50;
                    MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
                }
                else if (MainWindow.OGPet.size == "Small")
                {
                    MainWindow.OGPet.maxStamina = 75;
                    MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
                }
                else if (MainWindow.OGPet.size == "Medium")
                {
                    MainWindow.OGPet.maxStamina = 100;
                    MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
                }
                else if (MainWindow.OGPet.size == "Large")
                {
                    MainWindow.OGPet.maxStamina = 120;
                    MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
                }

                // Save Data
                MainWindow.OGPet.SavePet(MainWindow.OGPet);
            }

            
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }

        // This will dynamically change the available breed lists to correspond the species selected
        private void SpeciesPicker_DropDownClosed(object sender, EventArgs e)
        {
            //int a;
            //int b;

            // This was going to be a clever way of displaying all the options
            // in code that would take up less verical space.
            // Until I thought about the super long argument in the if() XD

            //for(a = 0; a < MainWindow.OGPet.speciesList.Length; a++)
            //{
            //    if(SpeciesPicker.Text == MainWindow.OGPet.speciesList[a])
            //    {
            //        for(b = 0; b < MainWindow.OGPet.br)
            //    }
            //}

            if(SpeciesPicker.Text == MainWindow.OGPet.speciesList[0])
            {
                // Dog Picked
                BreedPicker.Items.Clear();

                for (int k = 0; k < MainWindow.OGPet.DogBreedsList.Length; k++)
                    BreedPicker.Items.Add(MainWindow.OGPet.DogBreedsList[k]);
            }
            else if(SpeciesPicker.Text == MainWindow.OGPet.speciesList[1])
            {
                // Cat Picked
                BreedPicker.Items.Clear();

                for (int k = 0; k < MainWindow.OGPet.DogBreedsList.Length; k++)
                    BreedPicker.Items.Add(MainWindow.OGPet.CatBreedsList[k]);
            }
            else if (SpeciesPicker.Text == MainWindow.OGPet.speciesList[2])
            {
                // Hamster Picked
                BreedPicker.Items.Clear();

                for (int k = 0; k < MainWindow.OGPet.DogBreedsList.Length; k++)
                    BreedPicker.Items.Add(MainWindow.OGPet.HamsterBreedsList[k]);
            }
            else if (SpeciesPicker.Text == MainWindow.OGPet.speciesList[3])
            {
                // Bird Picked 
                BreedPicker.Items.Clear();

                for (int k = 0; k < MainWindow.OGPet.DogBreedsList.Length; k++)
                    BreedPicker.Items.Add(MainWindow.OGPet.BirdBreedsList[k]);
            }
            else if (SpeciesPicker.Text == MainWindow.OGPet.speciesList[4])
            {
                // Dino picked ¯\_(ツ)_/¯
                BreedPicker.Items.Clear();

                for (int k = 0; k < MainWindow.OGPet.DogBreedsList.Length; k++)
                    BreedPicker.Items.Add(MainWindow.OGPet.DinosaurBreedsList[k]);
            }
        }
    }
}
