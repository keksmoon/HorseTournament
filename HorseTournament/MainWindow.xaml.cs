﻿using GameClientSpace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HorseTournament {
    public partial class MainWindow : Window {
        private GameClient gameClient = null;
        private bool help = false;
        private MediaPlayer mediaBG = new MediaPlayer();
        // Иконки: красный игрок, зеленый игрок
        //         красная крепость, зеленая крепость
        public static ImageBrush redHome = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/redHome.png")));
        public static ImageBrush greenHome = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/greenHome.png")));
        public static ImageBrush green = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/greenPlr.png")));
        public static ImageBrush red = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/redPlr.png")));

        public MainWindow() {
            InitializeComponent();
            GameFieldInit();

            var path = Path.Combine(Environment.CurrentDirectory, "Media/bg.mp3");
            mediaBG.Open(new Uri($"file://{path}", UriKind.Absolute));
            mediaBG.MediaEnded += MediaBG_MediaEnded;
            mediaBG.Play();

            nextMoveGo.Text = "Green";
            nextMoveGo.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }

        private void MediaBG_MediaEnded(object sender, EventArgs e) {
            mediaBG.Position = new TimeSpan(0);
            mediaBG.Play();
        }

        /// <summary>
        /// Алгоритм игры робота.
        /// </summary>
        private void RobotGo() {
            if (gameClient.previousPossibleMove.Count == 0) return;

            //Чистим метки на кнопках возможных ходов сделанных на предыдущем ходе
            if (help)
            foreach (var previousNextPossibleMove in gameClient.previousPossibleMove) {
                var mychild = (Button)GameField.Children[previousNextPossibleMove.IJ];
               mychild.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
            }

            if (gameClient.previousRedMove != null)
                ((Button)GameField.Children[gameClient.previousRedMove.IJ]).Background = redHome;

            var newMove = gameClient.GetRobotMove();

            var robotChild = (Button)GameField.Children[newMove.IJ];
            robotChild.IsEnabled = false;
            robotChild.Background = red;

            nextMoveGo.Text = gameClient.FirstOrSecondGamer == 0 ? "Green" : "Red";
            nextMoveGo.Foreground = gameClient.FirstOrSecondGamer == 0 ? new SolidColorBrush(Color.FromRgb(0, 255, 0)) : new SolidColorBrush(Color.FromRgb(255, 0, 0));

            if (help)
            foreach (var nextPossibleMove in gameClient.GetVarMoves(newMove.I, newMove.J)) {
                robotChild = (Button)GameField.Children[nextPossibleMove.IJ];
                robotChild.Background = new SolidColorBrush(Color.FromRgb(100, 200, 100));
            }
        }

        /// <summary>
        /// Алгоритм сделанного хода
        /// </summary>
        private void HorseGo_ButtonClick(object sender, RoutedEventArgs e) {
            var but = (Button)sender;
            int position = int.Parse(but.Name.Substring(1));

            int i = position / 10; int j = position % 10;

            if (gameClient.CntMoves != 0) {
                // Начиная со второго хода (хода красных) проверяем куда предполагается ставить новый ход
                if (!gameClient.CanMakeMove(i, j)) {
                    MessageBox.Show("You can't go to here!", "Attention!");
                    return;
                }

                // Чистим метки на кнопках возможных ходов сделанных на предыдущем ходе
                if (help)
                foreach (var previousNextPossibleMove in gameClient.previousPossibleMove)
                    ((Button)GameField.Children[previousNextPossibleMove.IJ]).Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));

                // Помечаем поле предыдущего хода пройденным.
                if (gameClient.FirstOrSecondGamer == 0 && gameClient.previousGreenMove != null)
                    ((Button)GameField.Children[gameClient.previousGreenMove.IJ]).Background = greenHome;
                else if ((gameClient.FirstOrSecondGamer == 1 && gameClient.previousRedMove != null))
                    ((Button)GameField.Children[gameClient.previousRedMove.IJ]).Background = redHome;
            }

            // Делаем новый ход
            var mychild = (Button)GameField.Children[position];
            mychild.IsEnabled = false;
            mychild.Background = gameClient.FirstOrSecondGamer == 1 ? red : green;

            nextMoveGo.Text = gameClient.FirstOrSecondGamer == 1 ? "Green" : "Red";
            nextMoveGo.Foreground = gameClient.FirstOrSecondGamer == 1 ? new SolidColorBrush(Color.FromRgb(0, 255, 0)) : new SolidColorBrush(Color.FromRgb(255, 0, 0));

            gameClient.MoveTo(i, j);

            if (help)
            foreach (var nextPossibleMove in gameClient.GetVarMoves(i, j))
                ((Button)GameField.Children[nextPossibleMove.IJ]).Background = gameClient.FirstOrSecondGamer == 1 ?
                   new SolidColorBrush(Color.FromRgb(255, 83, 73)) : new SolidColorBrush(Color.FromRgb(100, 200, 100));

            if (gameClient.GameMode == 1 && gameClient.FirstOrSecondGamer == 1) 
                RobotGo();

            if (gameClient.MovesLeft == 0) {
                if (gameClient.OldMove.Count == 100) {
                    MessageBox.Show("Draw!");
                    return;
                }
                string win = gameClient.FirstOrSecondGamer == 0 ? "Red" : "Green";
                MessageBox.Show($"{win} WIN!");
            }
        }

        private void TwoPlayerModeButtonClick(object sender, RoutedEventArgs e) {
            string nameFirst, nameSecond;
            enterName enterName = new enterName();
            if (enterName.ShowDialog() == true) {
                nameFirst = enterName.namePlayer1.Text;
                nameSecond = enterName.namePlayer2.Text;
            } 
            else return;

            gameModeText.Text = "Играют двое";
            SettingFieldWelcome.Visibility = Visibility.Hidden;
            SettingField.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Visible;
            GameField.IsEnabled = true;
            HelpButton.IsEnabled = false;

            greenPlayerName.Text = nameFirst;
            redPlayerName.Text = nameSecond;

            gameClient = new GameClient(2);
        }

        private void OnePlayerModeButtonClick(object sender, RoutedEventArgs e) {
            string nameFirst, nameSecond;
            enterName enterName = new enterName();
            enterName.namePlayer2.Text = "Computer";
            enterName.namePlayer2.IsEnabled = false;
            if (enterName.ShowDialog() == true) {
                nameFirst = enterName.namePlayer1.Text;
                nameSecond = enterName.namePlayer2.Text;
            }
            else return;

            gameModeText.Text = "Играет один";
            SettingFieldWelcome.Visibility = Visibility.Hidden;
            SettingField.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Visible;
            GameField.IsEnabled = true;
            HelpButton.IsEnabled = false;

            greenPlayerName.Text = nameFirst;
            redPlayerName.Text = nameSecond;

            gameClient = new GameClient(1);
        }

        private void RulesOpenButtonClick(object sender, RoutedEventArgs e) => new rules().ShowDialog();

        /// <summary>
        /// Лист Рекордов
        /// </summary>
        private void RecordsButtonClick(object sender, RoutedEventArgs e) {
            recordsList.Items.Clear();

            TourWCF.ServiceClient service = new TourWCF.ServiceClient();
            var data = service.GetData();

            foreach (var rec in data)
                recordsList.Items.Add($"{rec.FirstPlayer} && {rec.SecondPlayer} :: Total Score: {rec.TotalScore}, WIN: {rec.Win}, " +
                    $"{rec.dateTime.Day}.{rec.dateTime.Month}.{rec.dateTime.Year}");

            service.Close();

            SettingFieldWelcome.Visibility = Visibility.Hidden;
            RecordsField.Visibility = Visibility.Visible;
        }

        private void BackFromRecordsButtonClick(object sender, RoutedEventArgs e) {
            SettingFieldWelcome.Visibility = Visibility.Visible;
            RecordsField.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Выход из режима игры в Меню
        /// </summary>
        private void StopButtonClick(object sender, RoutedEventArgs e) {
            if (gameClient == null)
                return;

            if (gameClient.MovesLeft == 0 && gameClient.OldMove.Count != 0) {
                List<TourWCF.Record> data;

                TourWCF.ServiceClient service = new TourWCF.ServiceClient();
                data = service.GetData().ToList();

                string win = gameClient.OldMove.Count == 100 ? "Draw" : gameClient.FirstOrSecondGamer == 0 ? "Red" : "Green";
                TourWCF.Record record = new TourWCF.Record();
                record.FirstPlayer = greenPlayerName.Text;
                record.SecondPlayer = redPlayerName.Text;
                record.TotalScore = gameClient.OldMove.Count;
                record.Win = win;
                record.dateTime = DateTime.Now;
                record.RoomCode = data.Count() + 1;

                data.Add(record);

                service.SetData(data.ToArray());

                service.Close();
            }

            greenPlayerName.Text = string.Empty;
            redPlayerName.Text = string.Empty;
            gameClient = null;
            GameFieldInit();
            SettingFieldWelcome.Visibility = Visibility.Visible;
            SettingField.Visibility = Visibility.Hidden;
            GameField.IsEnabled = false;
            HelpButton.IsEnabled = true;
            StopButton.Visibility = Visibility.Hidden;
        }

        #region Кнопки управления окном программы

        // Закрытия программы (красная кнопка)
        private void CloseAppClick(object sender, RoutedEventArgs e) => Close();

        // Открытие программы во весь экран (зеленая кнопка)
        private void MaximizeFrame(object sender, RoutedEventArgs e) {
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowState = WindowState.Maximized;
        }

        // Возвращение программы в нормальное положение (желтая кнопка)
        private void MinimizeFrame(object sender, RoutedEventArgs e) {
            this.ResizeMode = ResizeMode.CanResizeWithGrip;
            this.WindowState = WindowState.Normal;
        }

        // Изменение громкости звука
        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            => mediaBG.Volume = sliderPosition.Value;

        private void SoundOffOnButton(object sender, RoutedEventArgs e) {
            var but = (Button)sender;

            if (but.Style == (Style)Application.Current.FindResource("SoundButtonOn")) {
                but.Style = (Style)Application.Current.FindResource("SoundButtonOff");
                mediaBG.Pause();
            }
            else {
                but.Style = (Style)Application.Current.FindResource("SoundButtonOn");
                mediaBG.Play();
            }
        }

        private void HelpOnAppClick(object sender, RoutedEventArgs e) {
            var but = (Button)sender;

            if (but.Style == (Style)Application.Current.FindResource("HelpButtonOff")) {
                but.Style = (Style)Application.Current.FindResource("HelpButtonOn");
                help = true;
            }
            else {
                but.Style = (Style)Application.Current.FindResource("HelpButtonOff");
                help = false;
            }
        }

        /// <summary>
        /// Метод инициализации игрового поля.
        /// </summary>
        private void GameFieldInit() {
            int cnt = 0;
            GameField.Children.Clear();
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    Button button = new Button();
                    button.Name = $"_{cnt}";
                    button.Click += HorseGo_ButtonClick;
                    button.Style = (Style)Application.Current.FindResource("MyButtonStyle");
                    button.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));

                    GameField.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    cnt++;
                }
            }
        }

        /// <summary>
        /// Изменение размеров окна
        /// </summary>
        private void SizeChangingGameField(object sender, SizeChangedEventArgs e) {
            var gridHeight = GameGrid.RowDefinitions[0].ActualHeight - 20;
            var gridWidth = GameGrid.ColumnDefinitions[1].ActualWidth - 20;
            var minHW = Math.Min(gridHeight, gridWidth);

            if ((gridHeight > GameField.ActualHeight || gridWidth > GameField.ActualWidth) ||
                (Math.Abs(gridHeight - GameField.ActualHeight) > 20 || Math.Abs(gridWidth - GameField.ActualWidth) > 20)) {
                if (minHW < 0) return;
                GameField.Height = minHW;
                GameField.Width = minHW;
            }
        }

        /// <summary>
        /// Возможность перетягивать форму за меню
        /// </summary>
        private void Menu_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        #endregion Кнопки управления окном программы
    }
}