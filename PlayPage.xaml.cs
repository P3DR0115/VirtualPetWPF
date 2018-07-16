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
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {
        public PlayPage()
        {
            InitializeComponent();
            
            if (MainWindow.OGPet.experiencePoints < 100)
            {
                LevelIndicator.Content = "Level: " + MainWindow.OGPet.level;
                ExperienceIndicator.Content = "Experience Points: " + MainWindow.OGPet.experiencePoints;
            }
            else
            {
                MainWindow.OGPet.levelUp();

                LevelIndicator.Content = "Level: " + MainWindow.OGPet.level;
                ExperienceIndicator.Content = "Experience Points: " + MainWindow.OGPet.experiencePoints;
                
            }
            
            for (int j = 0; j < MainWindow.OGPet.InventoryList.Count; j++)
            {
                if (MainWindow.OGPet.InventoryList[j].name != "Food" && MainWindow.OGPet.InventoryList[j].durability > 0)
                {
                    ProductPicker.Items.Add(MainWindow.OGPet.InventoryList[j].name + ' ' + MainWindow.OGPet.InventoryList[j].durability);
                }
            }
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            bool alreadyUsed = false;

            // Cycle through the possible choices to find a match for what player is using
            for(int c = 0; c < MainWindow.OGPet.InventoryList.Count; c++) // lol c++
            {
                for (int p = 0; p < ProductPicker.Items.Count; p++)
                {
                    if ( ( (Convert.ToString(MainWindow.OGPet.InventoryList[c].name + ' ' + MainWindow.OGPet.InventoryList[c].durability)) == (Convert.ToString(ProductPicker.SelectionBoxItem))) &&
                        (Convert.ToString(ProductPicker.Items.GetItemAt(p)) == Convert.ToString(MainWindow.OGPet.InventoryList[c].name + ' ' + MainWindow.OGPet.InventoryList[c].durability)))
                    {
                        if (!alreadyUsed)
                        {                            
                            MainWindow.OGPet.experiencePoints += MainWindow.OGPet.InventoryList[c].XPReward;

                            MainWindow.OGPet.levelUp();

                            alreadyUsed = true;
                            MainWindow.OGPet.InventoryList[c].durability -= 1;

                            if(MainWindow.OGPet.InventoryList[c].durability < 1)
                            {
                                MainWindow.OGPet.InventoryList.RemoveAt(c);
                            }
                            
                            ProductPicker.Text = "";
                            LevelIndicator.Content = "Level: " + MainWindow.OGPet.level;
                            ExperienceIndicator.Content = "Expereince Points: " + MainWindow.OGPet.experiencePoints;
                            ProductPicker.Items.Clear();

                            //Add all the items again in order to refresh the info in dropdown menu
                            for (int j = 0; j < MainWindow.OGPet.InventoryList.Count; j++)
                            {
                                if (MainWindow.OGPet.InventoryList[j].name != "Food" && MainWindow.OGPet.InventoryList[j].durability > 0)
                                {
                                    ProductPicker.Items.Add(MainWindow.OGPet.InventoryList[j].name + ' ' + MainWindow.OGPet.InventoryList[j].durability);
                                }
                            }

                        }

                    }
                }

            }            
            
        } // end of use button()

        private void ReturnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }

    }
}
