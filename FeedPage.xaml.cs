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
    /// Interaction logic for FeedPage.xaml
    /// </summary>
    public partial class FeedPage : Page
    {
        public FeedPage()
        {
            InitializeComponent();

            StaminaIndicator.Content = "Stamina: " + MainWindow.OGPet.stamina;

            for (int j = 0; j < MainWindow.OGPet.InventoryList.Count; j++)
            {
                if (MainWindow.OGPet.InventoryList[j].ItemType != "Toy" )
                {
                    ProductPicker.Items.Add(Convert.ToString(MainWindow.OGPet.InventoryList[j].name + " " + MainWindow.OGPet.InventoryList[j].staminaRegen));
                }
            }

        }

        private void ReturnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            bool alreadyAte = false;

            // Cycle through the possible choices to find a match for what player is buying
            for (int p = 0; p < ProductPicker.Items.Count; p++)
            {

                for(int c = 0; c < MainWindow.OGPet.InventoryList.Count; c++)
                {
                    if ((Convert.ToString(MainWindow.OGPet.InventoryList[c].ItemType + " " + MainWindow.OGPet.InventoryList[c].staminaRegen)) == Convert.ToString(ProductPicker.SelectionBoxItem) &&
                        ((Convert.ToString(MainWindow.OGPet.InventoryList[c].ItemType + " " + MainWindow.OGPet.InventoryList[c].staminaRegen)) == (Convert.ToString(ProductPicker.Items.GetItemAt(p)))))
                    {
                        // Only eat food if stamina isn't full
                        if (MainWindow.OGPet.stamina < MainWindow.OGPet.maxStamina && (alreadyAte == false))
                        {
                            // Remove from player inventory and add the stamina regen amount to stamina
                            MainWindow.OGPet.stamina += MainWindow.OGPet.InventoryList[c].staminaRegen;

                            if (MainWindow.OGPet.stamina > MainWindow.OGPet.maxStamina)
                            {
                                // Don't allow the stamina to go over
                                MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
                            }

                            alreadyAte = true;
                            
                            MainWindow.OGPet.InventoryList.Remove(MainWindow.OGPet.InventoryList[c]);
                            ProductPicker.Text = "";
                            ProductPicker.Items.RemoveAt(p);
                            ProductPicker.Items.Refresh();

                            for (int j = 0; j < MainWindow.OGPet.InventoryList.Count; j++)
                            {
                                if (MainWindow.OGPet.InventoryList[j].ItemType != "Toy")
                                {
                                    ProductPicker.Items.Add(Convert.ToString(MainWindow.OGPet.InventoryList[j].ItemType + " " + MainWindow.OGPet.InventoryList[j].staminaRegen));
                                }
                            }

                            StaminaIndicator.Content = "Stamina: " + MainWindow.OGPet.stamina;
                        }

                    }
                }
                
            }
        }
    }
}
