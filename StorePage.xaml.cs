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
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        public StorePage()
        {
            InitializeComponent();

            GemsIndicator.Content = "Gems: " + MainWindow.OGPet.Gems;

            if (MainWindow.OGPet.lastStore <= DateTime.Now.AddMinutes(-10))
            {
                // Should have loaded Store stock. Don't restock
                Store.RestockStore();
            }// If they've never visited store, stock first time
            else if (MainWindow.OGPet.lastStore == null)
            {
                Store.RestockStore();
            } // if it's been less than 10 minutes
            else if (MainWindow.OGPet.lastSignIn >= DateTime.Now.AddMinutes(-9) && Store.JustOpened == false)
            {
                Store.RestockStore();
                Store.JustOpened = true;
            } // Did they just re-open the game? 
            else
            {
                // nothing?
                //RestockStore();
            }

            for (int j = 0; j < Store.Stock.Count; j++)
                ProductPicker.Items.Add(Convert.ToString(Store.Stock[j].name + ": " + Store.Stock[j].price + " Gems"));
        }

        private void PurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            bool purchaseOne = false;

            // Cycle through the possible choices to find a match for what player is buying
            for(int p = 0; p < Store.Stock.Count; p++)
            {
                if ((Convert.ToString(Store.Stock[p].name + ": " + Store.Stock[p].price + " Gems")) == Convert.ToString(ProductPicker.SelectionBoxItem))
                {
                    // Check if Player has enough gems
                    if(MainWindow.OGPet.Gems >= Store.Stock[p].price && (purchaseOne == false))
                    {
                        // Add the item into the inventory. then remove from stock
                        purchaseOne = true;
                        MainWindow.OGPet.InventoryList.Add(Store.Stock[p]);
                        MainWindow.OGPet.Gems -= Store.Stock[p].price;
                        Store.Stock.Remove(Store.Stock[p]);
                        ProductPicker.Text = "";
                        ProductPicker.Items.Clear();

                        for (int j = 0; j < Store.Stock.Count; j++)
                            ProductPicker.Items.Add(Convert.ToString(Store.Stock[j].name + ": " + Store.Stock[j].price + " Gems"));

                        ProductPicker.Items.Refresh();
                        GemsIndicator.Content = "Gems: " + MainWindow.OGPet.Gems;
                    }
                }
            }
        }

        private void ReturnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.OGPet.lastStore = DateTime.Now;
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }
    }
}
