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
    /// Interaction logic for TitlePage.xaml
    /// </summary>
    public partial class TitlePage : Page
    {

        public TitlePage()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
            if(MainWindow.OGPet.loadSuccess == false)
            {
                this.NavigationService.Navigate(new Uri("NewPetCustomization.xaml", UriKind.Relative));
            }
            else if(MainWindow.OGPet.loadSuccess == true && MainWindow.OGPet.CheckBirthday(MainWindow.OGPet))
            {
                // Pet's Birthday!
                this.NavigationService.Navigate(new Uri("BirthdayPage.xaml", UriKind.Relative));
            }
            else if (MainWindow.OGPet.loadSuccess == true)
            {
                // Not Pet's Birthday, just go to main menu... 
                this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
            }
        }

        private void CreditsButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Credits.xaml", UriKind.Relative));
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.OGPet.ClearAllData();
            MainWindow.OGPet = new Pet();
            this.NavigationService.Navigate(new Uri("NewPetCustomization.xaml", UriKind.Relative));
        }
    }
}
