﻿using MahApps.Metro.Controls;

namespace RetailPlanningAndForecasting.UI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            ViewModelLocator.Initalize(this);
            InitializeComponent();
        }
    }
}
