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
using System.Windows.Shapes;

namespace HorseTournament {
    /// <summary>
    /// Логика взаимодействия для rules.xaml
    /// </summary>
    public partial class rules : Window {
        public rules() {
            InitializeComponent();
        }

        private void dragStartGameForm(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void StartTravelButtonClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
