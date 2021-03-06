﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ViewCommunityHelper.View.WindowXaml
{
    /// <summary>
    /// Interaction logic for Faction.xaml
    /// </summary>
    public partial class FactionWindow : Window
    {
        public FactionWindow()
        {
            InitializeComponent();
        }


        private void Factions_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            if (grid.SelectedItem != null)
                grid.ScrollIntoView(grid.SelectedItem);
        }
    }
}
