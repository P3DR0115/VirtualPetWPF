﻿using System;
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
    /// Interaction logic for Credits.xaml
    /// </summary>
    public partial class Credits : Page
    {
        

        public Credits()
        {
            InitializeComponent();
        }

        private void TitleButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("TitlePage.xaml", UriKind.Relative));
        }
    }
}
