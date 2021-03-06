﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rafy;
using Rafy.WPF;

namespace JXC.WPF
{
    public partial class BillQueryLayout : UserControl, ILayoutControl
    {
        public BillQueryLayout()
        {
            InitializeComponent();
        }

        public void Arrange(UIComponents components)
        {
            commands.Content = components.CommandsContainer.Control;
            main.Content = components.Main.Control;
            var result = components.FindControls(QueryLogicalView.ResultSurrounderType);
            foreach (var ui in result)
            {
                queryResult.Items.Add(new TabItem
                {
                    Header = ui.Blocks.MainBlock.KeyLabel.Translate(),
                    Content = ui.Control
                });
            }
        }
    }
}