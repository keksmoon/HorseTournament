using System.Windows;
using System.Windows.Input;

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