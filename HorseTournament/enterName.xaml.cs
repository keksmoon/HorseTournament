using System.Windows;
using System.Windows.Input;

namespace HorseTournament {
    /// <summary>
    /// Логика взаимодействия для enterName.xaml
    /// </summary>
    public partial class enterName : Window {
        public enterName() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(namePlayer1.Text) || string.IsNullOrEmpty(namePlayer2.Text)) {
                MessageBox.Show("The fields are filled in incorrectly!");
                this.DialogResult = false;
                return;
            }

            this.DialogResult = true;
        }

        private void dragStartGameForm(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) 
                DragMove();
        }
    }
}
