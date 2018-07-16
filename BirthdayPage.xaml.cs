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
    /// Interaction logic for BirthdayPage.xaml
    /// </summary>
    public partial class BirthdayPage : Page
    {
        public BirthdayPage()
        {
            InitializeComponent();

            BirthdayMessage.Text = "It's " + MainWindow.OGPet.name + "'s Birthday! Take these 25 Gems as a gift!";
            
            MainWindow.OGPet.Gems += 25;
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }
    }
}
