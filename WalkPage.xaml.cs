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
    /// Interaction logic for WalkPage.xaml
    /// </summary>
    public partial class WalkPage : Page
    {
        int waitIterations;
        int y = -1;
        bool alreadyWaited = false;

        public WalkPage()
        {
            InitializeComponent();

            XPLbl.Visibility = System.Windows.Visibility.Hidden;
            completeLbl.Visibility = System.Windows.Visibility.Hidden;
            //walkProgress.Visibility = System.Windows.Visibility.Hidden;

            // This should display all the options for walking while checking if pet has enough stamina
            for (int j = 0; j < MainWindow.OGPet.speciesList.Length; j++)
            {
                if(MainWindow.OGPet.stamina >= (5 + (5 * j) ) )
                    WalkPicker.Items.Add(MainWindow.OGPet.walkList[j]);
            }
        }

        private void WalkButton_Click(object sender, RoutedEventArgs e)
        {
            waitIterations = 0;

            WalkPicker.Visibility = System.Windows.Visibility.Hidden;
            WalkButton.Visibility = System.Windows.Visibility.Hidden;
            waitIterations = MainWindow.OGPet.WalkPet(Convert.ToString(WalkPicker.SelectionBoxItem));

            TimeLbl.Content = "Time Left: " + waitIterations;
            TimeLbl.UpdateLayout();
            TimeLbl.UpdateLayout();

            ActualWalkStuff();
        }

        private void ReturnToMainButton_Click(object sender, RoutedEventArgs e)
        {            
            this.NavigationService.Navigate(new Uri("MainMenu.xaml", UriKind.Relative));
        }

        public void ActualWalkStuff()
        {
            TimeLbl.Content = "Time Left: " + waitIterations;

            for (int WI = 0; WI < waitIterations; WI++)
            {
                TimeLbl.Content = "Time Left: " + (waitIterations - WI);
                TimeLbl.UpdateLayout();
                MainWindow.OGPet.DelayOneSec();

            }

            TimeLbl.Content = "Time Left: 0";
            completeLbl.Visibility = System.Windows.Visibility.Visible;
            XPLbl.Visibility = System.Windows.Visibility.Visible;
            XPLbl.Content = "XP Earned: " + MainWindow.OGPet.tempXP;
            MainWindow.OGPet.lastWalked = DateTime.Now;
        }
        
    }
}
