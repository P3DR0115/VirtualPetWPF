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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();

            StaminaBar.Maximum = MainWindow.OGPet.maxStamina;
            StaminaBar.Value = (MainWindow.OGPet.maxStamina - MainWindow.OGPet.stamina);

            if(MainWindow.OGPet.experiencePoints < 100)
            {
                ExperienceBar.Value = MainWindow.OGPet.experiencePoints;
            }
            else
            {
                while(MainWindow.OGPet.experiencePoints >= 100)
                {
                    MainWindow.OGPet.level += 1;
                    MainWindow.OGPet.experiencePoints -= 100;
                    ExperienceBar.Value = MainWindow.OGPet.experiencePoints;
                    // Don't forget to make a label to display the current pet level! 
                }
            }

            GemsIndicator.Content = "Gems: " + MainWindow.OGPet.Gems;
        }

        private void WalkPetButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("WalkPage.xaml", UriKind.Relative));
        }

        private void StaminaBar_ToolTipOpening(object sender, ToolTipEventArgs e)
        {

        }

        private void StoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StorePage.xaml", UriKind.Relative));
        }

        private void PlayPetButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PlayPage.xaml", UriKind.Relative));
        }

        private void FeedPetButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("FeedPage.xaml", UriKind.Relative));
        }

        private void PetSkills_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SkillsPage.xaml", UriKind.Relative));
        }
    }
}
