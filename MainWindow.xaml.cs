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
using System.Windows.Threading;

namespace VirtualPetWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static Pet OGPet = new Pet();
        public static int progress = 0; // for walking
        public static Store OGStore = new Store();


        public MainWindow()
        {
            InitializeComponent();

            _NavigationFrame.Navigate(new TitlePage());

            //OGPet = ()OGPet;
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OGPet.lastSignIn = DateTime.Now;
            OGPet.SavePet(OGPet);
            OGPet.SaveInventory(OGPet);
            OGStore.SaveStock(OGStore);
            OGPet.SaveSkills(OGPet);
        }
    }
}
