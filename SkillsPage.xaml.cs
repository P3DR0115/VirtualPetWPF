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
    /// Interaction logic for SkillsPage.xaml
    /// </summary>
    public partial class SkillsPage : Page
    {
        public SkillsPage()
        {
            InitializeComponent();

            StaminaIndicator.Content = "Stamina: " + MainWindow.OGPet.stamina;
            LevelIndicator.Content = "Level: " + MainWindow.OGPet.level;
            ExperienceIndicator.Content = "Experience Points: " + MainWindow.OGPet.experiencePoints;

            for (int j = 0; j < MainWindow.OGPet.SkillList.Count; j++)
            {
                ProductPicker.Items.Add(Convert.ToString(MainWindow.OGPet.SkillList[j].skillName + " " + MainWindow.OGPet.SkillList[j].staminaCost));
            }
        }

        private void ReturnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            bool alreadyPerformed = false;
            // Cycle through the possible choices to find a match for what player is buying
            for (int p = 0; p < ProductPicker.Items.Count; p++)
            {

                for (int c = 0; c < MainWindow.OGPet.SkillList.Count; c++)
                {
                    if ((Convert.ToString(MainWindow.OGPet.SkillList[c].skillName + " " + MainWindow.OGPet.SkillList[c].staminaCost)) == Convert.ToString(ProductPicker.SelectionBoxItem))
                    {
                        // Only execute if Pet has enough stamina
                        if (MainWindow.OGPet.stamina >= MainWindow.OGPet.SkillList[c].staminaCost && (alreadyPerformed == false))
                        {
                            MainWindow.OGPet.SkillList[c].Execute(MainWindow.OGPet, MainWindow.OGPet.SkillList[c].levelRequired, MainWindow.OGPet.SkillList[c].staminaCost, MainWindow.OGPet.SkillList[c].successRate, MainWindow.OGPet.SkillList[c].maxXP);
                            
                            alreadyPerformed = true;                            

                            StaminaIndicator.Content = "Stamina: " + MainWindow.OGPet.stamina;
                            LevelIndicator.Content = "Level: " + MainWindow.OGPet.level;
                            ExperienceIndicator.Content = "Experience Points: " + MainWindow.OGPet.experiencePoints;
                        }

                    }
                }

            }
        }
    }
}
