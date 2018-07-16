using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetWPF
{
     public class Store
    {
        public static string temp;

        public static int rndStock;
        public static Random rnd = new Random();
        public static List<Item> Stock = new List<Item>();
        public static bool JustOpened = false;

        public static bool loadStockSuccess = false;
        public string[] ALLSTOCKINFO = new string[6]; 
        public static string SaveStocklocation = AppDomain.CurrentDomain.BaseDirectory + "PetStoreData.txt";

        public Store()
        {
            LoadStock();

            if (!loadStockSuccess)
                RestockStore();
        }

        public static void RestockStore()
        {
            Stock.Clear();

            for (int j = 0; j < 5; j++)
            {
                rndStock = rnd.Next(3);
                rndStock += 1;

                switch (rndStock)
                {
                    case 1:
                        {
                            // Item is food
                            Item.Food newItem = new Item.Food();

                            Stock.Add(newItem);
                            break;
                        } // case 1
                    case 2:
                        {
                            // Item is food
                            Item.Toy newItem = new Item.Toy();

                            Stock.Add(newItem);
                            break;
                        } // case 2
                    case 3:
                        {
                            // Item is food
                            Item.Treat newItem = new Item.Treat();

                            Stock.Add(newItem);
                            break;
                        } // case 3
                } // switch
            } // for loop

            // Update the last time the store restocked
            MainWindow.OGPet.lastStore = DateTime.Now;
        } // Restock store()

        public void SaveStock(Store store)
        {
            for (int w = 0; w < Stock.Count; w++)
            {
                // So I don't save Items that don't even have any durabilty
                if (Stock[w].durability > 0)
                {
                    ALLSTOCKINFO[w] = Stock.ElementAt(w).name + ',' + Stock.ElementAt(w).ItemType + ','
                        + Stock.ElementAt(w).durability + ',' + Stock.ElementAt(w).staminaRegen + ','
                        + Stock.ElementAt(w).XPReward + ',' + Stock.ElementAt(w).price;
                }
            }

            //System.IO.File.AppendAllLines(SaveInventorylocation, ALLINVENTORYINFO);
            System.IO.File.WriteAllLines(SaveStocklocation, ALLSTOCKINFO);
        }

        public void LoadStock()
        {
            try
            {
                // Get all the inventory data and place it in a temporary string array;
                ALLSTOCKINFO = System.IO.File.ReadAllLines(SaveStocklocation);

                //oneItem[] = ALLINVENTORYINFO[].Split(',');

                for (int il = 0; il < ALLSTOCKINFO.Length; il++)
                {
                    // Take a single loaded item from the array and separate the components into another array for transfer
                    string[] oneItem = ALLSTOCKINFO[il].Split(',');

                    // Check if the oneItem is blank. If not, add to inventory;
                    if (oneItem[0] != "")
                    {
                        Stock.Add(new Item());
                        Stock[il].name = oneItem[0];
                        Stock[il].ItemType = oneItem[1];
                        Stock[il].durability = Convert.ToInt32(oneItem[2]);
                        Stock[il].staminaRegen = Convert.ToInt32(oneItem[3]);
                        Stock[il].XPReward = Convert.ToInt32(oneItem[4]);
                        Stock[il].price = Convert.ToInt32(oneItem[5]);
                    }
                }

                loadStockSuccess = true;
                JustOpened = true;
            }
            catch (Exception e)
            {
                ALLSTOCKINFO.Initialize();
                loadStockSuccess = false;
            }
        }

    }
}
