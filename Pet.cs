using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace VirtualPetWPF
{
    public class Pet
    {
        //MainMenu mainMenu = new MainMenu();
        
        public bool loadSuccess = false; // This is to check if successfully loaded a pet file.
        public bool loadInvSuccess = false; // This is to check if successfully loaded the pet's inventory
        public bool loadSkillsSuccess = false;

        public string[] DogBreedsList = new string[5] { "Husky", "German Shephard", "Golden Retriever", "Wire Hair Fox Terrier", "French Bulldog" };
        public string[] CatBreedsList = new string[5] { "Persian", "Bengal", "Burmese", "Siamese", "Sphynx" };
        public string[] HamsterBreedsList = new string[5] { "Golden", "Campbell's Dwarf", "Djungarian", "Roborovski", "Chinese" };
        public string[] DinosaurBreedsList = new string[5] { "Velociraptor", "Stegosaurus", "Triceretops", "Tyrannosaurus Rex", "Allosaurus" };
        public string[] BirdBreedsList = new string[5] { "Parakeet", "Cardinal", "Parrot", "Hawk", "Eagle" };
        public string[] colorsList = new string[13] { "Black", "White", "Brown", "Spotted", "Zebra-Striped", "Magenta", "Cyan", "Yellow", "Red", "Blue", "Green", "Gold", "Diamond" };
        public string[] speciesList = new string[5] { "Dog", "Cat", "Hamster", "Bird", "Dinosaur" };

        public string[] walkList = new string[5] { "Brisk", "Short", "Moderate", "Long", "Very Long" };

        public string name = "";
        public string breed = "";
        public string gender = "";
        public string color = "";
        public string species = "";
        public string size = "";
        public static int age;
        public int stamina = 100; // used for feeding / doing things
        public int maxStamina = 100; // Pet's max Stamina
        public int experiencePoints; // The pet's experience points
        public int level = 0; // The Pet's Level, 1 gained every 100 EXP
        public int Gems = 150; // THIS IS MONEY $$$
        DateTime today = DateTime.Now;
        public DateTime bornDate;
        public int temp;

        // Pet data and save location
        public static string[] ALLINFO = new string[19];
        public static string[] ALLINFOTEMP = new string[19]; // for test loading
        public static string Savelocation = AppDomain.CurrentDomain.BaseDirectory + "PetData.txt";

        // Pet Inventory and save location
        public List<Item> InventoryList = new List<Item>();
        public string[] ALLINVENTORYINFO = new string[20];
        public static string SaveInventorylocation = AppDomain.CurrentDomain.BaseDirectory + "PetInventoryData.txt";

        // Pet Skills and save location;
        public List<Skill> SkillList = new List<Skill>();
        public string[] ALLSKILLINFO = new string[20];
        public static string SaveSkillLocation = AppDomain.CurrentDomain.BaseDirectory + "PetSkillData.txt";

        // A bunch of DateTimes to keep track of the player's actions
        public DateTime lastGemsSpin; // Last time the Player did the Gem Spin
        public DateTime lastSignIn;   // Last time the Player signed in 
        public DateTime lastWalked;   // Last time the Player walked their pet
        public DateTime lastFed;      // Last time the Player fed their pet
        public DateTime lastPlayed;   // Last time the Player played with their pet
        public DateTime lastStore;    // Last time the Player visited the store. Used to generate new items
        public DateTime lastPet;      // Last time the Player pet their pet

        static Random rnd = new Random(); // random number generator

        public int NumberOfTimesPet = 0;

        public Pet()
        {
            // Constructor
            // Try to load the pet first, if unsuccessful, begin customizing new pet
            LoadPet();

            LoadSkills();
            
            LoadInventory();

            //test();

            if (!loadSkillsSuccess)
            {
                Sit sit = new Sit();
                SkillList.Add(sit);
            }

            // The birthday is in the future. Can't be in the past if you create the pet today...
            if (loadSuccess == false)
            {
                age = rnd.Next(84, 168);
                bornDate = today.AddDays(age);
                //CustomizationMenu(this);
            }

        }

        public void ClearAllData()
        {
            System.IO.File.Delete(Savelocation);
            System.IO.File.Delete(SaveInventorylocation);
            System.IO.File.Delete(SaveSkillLocation);
            ALLINFO.Initialize();
            ALLINVENTORYINFO.Initialize();
            ALLSKILLINFO.Initialize();

        }

        public void WriteToALLINFO()
        {
            // Bunch of Stats
            ALLINFO[0] = name;
            ALLINFO[1] = species;
            ALLINFO[2] = breed;
            ALLINFO[3] = size;
            ALLINFO[17] = gender;
            ALLINFO[4] = Convert.ToString(stamina);
            ALLINFO[16] = Convert.ToString(maxStamina);
            ALLINFO[5] = color;
            ALLINFO[6] = Convert.ToString(experiencePoints);
            ALLINFO[18] = Convert.ToString(level);
            ALLINFO[7] = Convert.ToString(bornDate);
            ALLINFO[8] = Convert.ToString(Gems);

            // Bunch of Times
            ALLINFO[9] = Convert.ToString(lastGemsSpin);
            ALLINFO[10] = Convert.ToString(lastSignIn);
            ALLINFO[11] = Convert.ToString(lastWalked);
            ALLINFO[12] = Convert.ToString(lastFed);
            ALLINFO[13] = Convert.ToString(lastPlayed);
            ALLINFO[14] = Convert.ToString(lastStore);
            ALLINFO[15] = Convert.ToString(lastPet);

        }

        // Just wanna say, I can't believe I came up with this X'D
        public void SaveInventory(Pet pet)
        {
            // At the risk of possible data loss, I have to clear the inventory save file in order to prevent the player
            // from basically never losing an item if the last item in their inventory happens to reach 0 durability
            //InventoryList.Reverse();
            //System.IO.File.Create(SaveInventorylocation);

            for (int w = 0; w < InventoryList.Count; w++)
            {
                // So I don't save Items that don't even have any durabilty
                if(InventoryList[w].durability > 0)
                {
                    ALLINVENTORYINFO[w] = InventoryList.ElementAt(w).name + ',' + InventoryList.ElementAt(w).ItemType + ','
                        + InventoryList.ElementAt(w).durability + ',' + InventoryList.ElementAt(w).staminaRegen + ','
                        + InventoryList.ElementAt(w).XPReward;
                }
            }

            //System.IO.File.AppendAllLines(SaveInventorylocation, ALLINVENTORYINFO);
            System.IO.File.WriteAllLines(SaveInventorylocation, ALLINVENTORYINFO);
        }

        // Just wanna say, I can't believe I came up with this X'D
        public void LoadInventory()
        {
            try
            {
                // Get all the inventory data and place it in a temporary string array;
                ALLINVENTORYINFO = System.IO.File.ReadAllLines(SaveInventorylocation);                

                //oneItem[] = ALLINVENTORYINFO[].Split(',');

                for (int il = 0; il < ALLINVENTORYINFO.Length; il++)
                {
                    // Take a single loaded item from the array and separate the components into another array for transfer
                    string[] oneItem = ALLINVENTORYINFO[il].Split(',');

                    // Check if the oneItem is blank. If not, add to inventory;
                    if(oneItem[0] != "")
                    {
                        InventoryList.Add(new Item());
                        InventoryList[il].name = oneItem[0];
                        InventoryList[il].ItemType = oneItem[1];
                        InventoryList[il].durability = Convert.ToInt32(oneItem[2]);
                        InventoryList[il].staminaRegen = Convert.ToInt32(oneItem[3]);
                        InventoryList[il].XPReward = Convert.ToInt32(oneItem[4]);
                    }                    
                }
                
                loadInvSuccess = true;
            }
            catch(Exception e)
            {
                ALLINVENTORYINFO.Initialize();
                loadInvSuccess = false;
            }
        }

        public void SaveSkills(Pet pet)
        {
            for (int w = 0; w < SkillList.Count; w++)
            {

                ALLSKILLINFO[w] = SkillList.ElementAt(w).skillName + ',' + SkillList.ElementAt(w).levelRequired + ','
                    + SkillList.ElementAt(w).staminaCost + ',' + SkillList.ElementAt(w).successRate + ','
                    + SkillList.ElementAt(w).maxXP;
                
            }

            //System.IO.File.AppendAllLines(SaveInventorylocation, ALLINVENTORYINFO);
            System.IO.File.WriteAllLines(SaveSkillLocation, ALLSKILLINFO);

        }

        public void LoadSkills()
        {
            try
            {
                // Get all the skill data 
                ALLSKILLINFO = System.IO.File.ReadAllLines(SaveSkillLocation);
                
                for (int il = 0; il < ALLSKILLINFO.Length; il++)
                {
                    // Take a single loaded item from the array and separate the components into another array for transfer
                    string[] oneItem = ALLSKILLINFO[il].Split(',');

                    // Check if the oneItem is blank. If not, add to inventory;
                    if (oneItem[0] != "")
                    {
                        
                        SkillList.Add(new Skill());
                        SkillList[il].skillName = oneItem[0];
                        SkillList[il].levelRequired = Convert.ToInt32(oneItem[1]);
                        SkillList[il].staminaCost = Convert.ToInt32(oneItem[2]);
                        SkillList[il].successRate = Convert.ToInt32(oneItem[3]);
                        SkillList[il].maxXP = Convert.ToInt32(oneItem[4]);

                    }
                }

                loadSkillsSuccess = true;
            }
            catch (Exception e)
            {
                ALLSKILLINFO.Initialize();
                //loadInvSuccess = false;
            }

        }

        // SAVING FEATURE [GOLD]
        public void SavePet(Pet pet)
        {
            WriteToALLINFO();
            System.IO.File.WriteAllLines(Savelocation, ALLINFO);
        }

        // LOADING FEATURE [GOLD]
        public void LoadPet()
        {
            try
            {
                ALLINFOTEMP = System.IO.File.ReadAllLines(Savelocation);
                ALLINFO = ALLINFOTEMP;

                // "Stats" first
                name = ALLINFO[0];
                species = ALLINFO[1];
                breed = ALLINFO[2];
                size = ALLINFO[3];
                gender = ALLINFO[17];
                stamina = Convert.ToInt32(ALLINFO[4]);
                maxStamina = Convert.ToInt32(ALLINFO[16]);
                color = ALLINFO[5];
                experiencePoints = Convert.ToInt32(ALLINFO[6]);
                level = Convert.ToInt32(ALLINFO[18]);
                bornDate = Convert.ToDateTime(ALLINFO[7]);
                Gems = Convert.ToInt32(ALLINFO[8]);

                // "Times" Second
                lastGemsSpin = Convert.ToDateTime(ALLINFO[9]);
                lastSignIn = Convert.ToDateTime(ALLINFO[10]);
                lastWalked = Convert.ToDateTime(ALLINFO[11]);
                lastFed = Convert.ToDateTime(ALLINFO[12]);
                lastPlayed = Convert.ToDateTime(ALLINFO[13]);
                lastStore = Convert.ToDateTime(ALLINFO[14]);
                lastPet = Convert.ToDateTime(ALLINFO[15]);

                loadSuccess = true;
            }
            catch (Exception e)
            {
                ALLINFOTEMP.Initialize();
                loadSuccess = false;
            }
        }

        public bool CheckBirthday(Pet pet)
        {
            bool BirthdayToday = false;

            if (pet.lastSignIn.Date != DateTime.Today.Date)
            {
                // If not farming today... check if "monthly" birthday 
                // (Farming for the gift...)
                if (pet.bornDate <= DateTime.Now)
                {
                    if (bornDate.Day == DateTime.Today.Day)
                    {
                        BirthdayToday = true;
                        // Yes correct day
                        //WriteLine("It's " + name + " birthday! Woot!");
                        //WriteLine("As a small gift, please take these 25 Happy Points!");
                        //pet.happyPoints += 25;
                    }

                }
            }

            return BirthdayToday;
        }

        public void levelUp()
        {
            bool leveledUp = false;

            while(experiencePoints > 100)
            {
                leveledUp = true;
                level += 1;
                experiencePoints -= 100;

                if(level == 2)
                {
                    Speak speak = new Speak();
                    SkillList.Add(speak);
                }
                else if(level == 4)
                {
                    Shake shake = new Shake();
                    SkillList.Add(shake);
                }
                else if(level == 5)
                {
                    Roll roll = new Roll();
                    SkillList.Add(roll);
                }
                else if(level == 15)
                {
                    FileTaxes fileTaxes = new FileTaxes();
                    SkillList.Add(fileTaxes);
                }

            }
        }

        public void DelayOneSec()
        {
            //move to its own function for ex Delay Progress
            //neilinglese@gmail.com

            // Credit to Neil Inglese for helping me with programming this method

            // As much as I wanted to use this instead of Thread.Sleep, I couldn't get it to work for my purposes.
            // Thus, I resorted to Thread.Sleep, Something I was more familiar with. 

            //var delay = Task.Run(async () =>
            //{
            //    Stopwatch sw = new Stopwatch();
            //    await Task.Delay(1000);
            //    sw.Stop();
            //});

            System.Threading.Thread.Sleep(1000);

        }

        public int WalkPet(string walkLength)
        {
            // This is how much xp is earned
            temp = 50;
            int walkChoice = 0;

            for(int q = 0; q < walkList.Length; q++)
            {
                if(walkLength == walkList[q])
                {
                    switch(q)
                    {
                        case 0:
                            {
                                MainWindow.OGPet.stamina -= 5;
                                //System.Threading.Thread.Sleep(5000);
                                //MainWindow._time = TimeSpan.FromSeconds(10);

                                //MainWindow._Timer.Equals(MainWindow._time);
                                //MainWindow._Timer.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, delegate 
                                //{
                                //    if (MainWindow._time == TimeSpan.Zero) MainWindow._Timer.Stop();
                                //    MainWindow._time = MainWindow._time.Add(TimeSpan.FromSeconds(-1));
                                //}, Application.Current.Dispatcher);
                                walkChoice = q;
                                temp /= 3;
                                break;
                            }
                        case 1:
                            {
                                MainWindow.OGPet.stamina -= 10;
                                //System.Threading.Thread.Sleep(10000);
                                walkChoice = q;
                                temp /= 2;
                                break;
                            }
                        case 2:
                            {
                                MainWindow.OGPet.stamina -= 15;
                                //System.Threading.Thread.Sleep(15000);
                                walkChoice = q;
                                break;
                            }
                        case 3:
                            {
                                MainWindow.OGPet.stamina -= 20;
                                //System.Threading.Thread.Sleep(20000);
                                walkChoice = q;
                                temp = (temp + (temp / 2));
                                break;
                            }
                        case 4:
                            {
                                MainWindow.OGPet.stamina -= 25;
                                //System.Threading.Thread.Sleep(25000);
                                walkChoice = q;
                                temp *= 2;
                                break;
                            }
                    }
                }
            }
            
            MainWindow.OGPet.lastWalked = DateTime.Now;
            MainWindow.OGPet.experiencePoints += temp;

            walkChoice = ((5 * walkChoice) + 5);

            return walkChoice;
        }
                
    } // Pet

    // Dog Subclass
    public class Dog : Pet
    {
        public Dog()
        {
            MainWindow.OGPet.size = "Large";
            MainWindow.OGPet.maxStamina = 120;
            MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;

            if ((MainWindow.OGPet.breed == "Wire Hair Fox Terrier" || MainWindow.OGPet.breed == "French Bulldog"))
            {
                MainWindow.OGPet.size = "Medium";
                MainWindow.OGPet.maxStamina = 100;
                MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
            }

        }
    }

    // Cat Subclass
    public class Cat : Pet
    {
        public Cat()
        {
            MainWindow.OGPet.size = "Small";
            MainWindow.OGPet.maxStamina = 75;
            MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;

        }
    }

    // Hamster Subclass
    public class Hamster : Pet
    {
        public Hamster()
        {
            MainWindow.OGPet.size = "Tiny";
            MainWindow.OGPet.maxStamina = 50;
            MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;
        }

    }

    // Bird Subclass
    public class Bird : Pet
    {
        public Bird()
        {
            MainWindow.OGPet.size = "Small";

            MainWindow.OGPet.maxStamina = 75;
            MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;

            if ((MainWindow.OGPet.breed == "Eagle" || MainWindow.OGPet.breed == "Hawk"))
            {
                MainWindow.OGPet.size = "Medium";
                MainWindow.OGPet.maxStamina = 100;
                MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;

            }
        }
    }

    // Dinosaur Subclass
    public class Dinosaur : Pet
    {
        public Dinosaur()
        {
            MainWindow.OGPet.size = "Small";
            MainWindow.OGPet.maxStamina = 75;
            MainWindow.OGPet.stamina = MainWindow.OGPet.maxStamina;

        }
    }


} // Namespace
