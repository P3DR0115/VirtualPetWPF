using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetWPF
{
    public class Item
    {

        public string name; // Name of product
        public string ItemType; // Type of item
        public int price; // price of the product
        public int durability; // single use items will have 1
        public int XPReward;
        public int staminaRegen;
        public int temp;

        public static void ChargeHappyPoints(Pet pet, Item product)
        {
            pet.Gems -= product.price;
        }

        public class Food : Item
        {

            // Setting up the price, staminaRegen and durabilty
            public Food()
            {
                // Constructor
                Random rnd = new Random();
                name = "Food";
                ItemType = "Food";
                price = 10;
                durability = 1; // Single Use

                System.Threading.Thread.Sleep(50); // To help randomize the number
                staminaRegen = rnd.Next(2, 5);
                staminaRegen *= 10;

                System.Threading.Thread.Sleep(50); // To help randomize the number
                temp = rnd.Next(0, 5); // For fluxuation in pricing
                price += temp;

            } // Food Constructor

            public void FoodGiveStamina(Pet pet)
            {
                pet.stamina += staminaRegen;
            }
        } // Food Item

        public class Toy : Item
        {
            public Toy()
            {
                // Constructor
                Random rnd = new Random();
                //name = "Toy";
                ItemType = "Toy";
                temp = rnd.Next(1, 5);

                switch (temp)
                {
                    case 1:
                        {
                            name = "Ball";
                            price = 15;
                            durability = 5;

                            System.Threading.Thread.Sleep(50); // To help randomize the number
                            temp = rnd.Next(0, 4); // For fluxuation in pricing
                            price += temp;

                            temp = rnd.Next(10, 30);
                            XPReward = temp;
                            break;
                        }
                    case 2:
                        {
                            name = "Chew Toy";
                            price = 15;
                            durability = 3;

                            System.Threading.Thread.Sleep(50); // To help randomize the number
                            temp = rnd.Next(0, 5); // For fluxuation in pricing
                            price += temp;

                            temp = rnd.Next(15, 25);
                            XPReward = temp;
                            break;
                        }
                    case 3:
                        {
                            name = "Stuffed Animal";
                            price = 10;
                            durability = 2;

                            System.Threading.Thread.Sleep(50); // To help randomize the number
                            temp = rnd.Next(0, 3); // For fluxuation in pricing
                            price += temp;

                            temp = rnd.Next(5, 20);
                            XPReward = temp;
                            break;
                        }
                    case 4:
                        {
                            name = "Ultra Ball";
                            price = 25;
                            durability = 12;

                            System.Threading.Thread.Sleep(50); // To help randomize the number
                            temp = rnd.Next(0, 8); // For fluxuation in pricing
                            price += temp;

                            temp = rnd.Next(35, 50);
                            XPReward = temp;
                            break;
                        }
                    case 5:
                        {
                            name = "Lazer Pointer";
                            price = 50;
                            durability = 3;

                            System.Threading.Thread.Sleep(50); // To help randomize the number
                            temp = rnd.Next(0, 15); // For fluxuation in pricing
                            price += temp;

                            temp = rnd.Next(50, 75);
                            XPReward = temp;
                            break;
                        }
                } // Switch

            } // Toy Constructor

        } // Toy Item

        public class Treat : Item
        {
            public Treat()
            {
                // Constructor
                Random rnd = new Random();
                name = "Treat";
                ItemType = "Treat";
                price = 25;
                durability = 1; // Single Use
                staminaRegen = (MainWindow.OGPet.maxStamina);

                System.Threading.Thread.Sleep(50); // To help randomize the number
                temp = rnd.Next(0, 10); // For fluxuation in pricing
                price += temp;

            } // Treat Ctor

            public void TreatGiveStamina(Pet pet)
            {
                // Give the pet full stamina + a 20% boost
                pet.stamina = (pet.maxStamina + (pet.maxStamina / 5) );
            }
        } // Treat Item
    }
}
