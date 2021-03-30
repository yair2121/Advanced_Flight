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

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Advanced_Flight_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender1, RoutedEventArgs e)
        {
            Model_Server server = new Model_Server();
            server.connect("127.0.0.1", 5400);
            Thread sending = new Thread(server.send);
            sending.Start();
        }

        private void slidey_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }
    }
}
