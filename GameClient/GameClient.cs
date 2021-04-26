using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameClientSpace {
    [Serializable]
    public class Pair {
        public int I, J;
        public int IJ;

        public Pair() { }

        public Pair(int I, int J) {
            this.I = I;
            this.J = J;
            IJ = int.Parse($"{I}{J}");
        }
        public override bool Equals(object obj) {
            var other = obj as Pair;
            if (other == null) return false;

            return IJ == other.IJ;
        }

        public override int GetHashCode() => 1;
    }

    [Serializable]
    public class GameClient {
        //Запоминаем предыдущие позиции, чтобы их возвращать в исходное состояние
        public List<Pair> previousPossibleMove; // Предыдущие возможные ходы
        public Pair previousMove; // Предыдущий ход
        public Pair prePreviousMove;

        public int[][] movex;

        public HashSet<Pair> OldMove; // Завоеванные замки
        public int FirstOrSecondGamer; // Номер ходящего игрока
        public int MovesLeft; // Количество возможных ходов

        public int CntMoves;

        public string firstPlayer { get; set; } = string.Empty;
        public string secondPlayer { get; set; } = string.Empty;

        /// <summary>
        /// Определение возможных следующих ходов
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Коллекция ходов</returns>
        public IEnumerable<Pair> MoveTo(int i, int j) {
            movex[i][j] = FirstOrSecondGamer == 0 ? 30 : 31;

            // 0 - зеленый
            // 1 - красный

            if (previousMove != null)
                movex[previousMove.I][previousMove.J] = FirstOrSecondGamer == 0 ? 11 : 10;

            if (previousPossibleMove != null) {
                foreach (var m in previousPossibleMove) {
                    if (m.I == i && m.J == j) continue;
                    movex[m.I][m.J] = 0;
                }
            }

            if (previousMove != null)
            prePreviousMove = new Pair(previousMove.I, previousMove.J);
            previousMove = new Pair(i, j);
            OldMove.Add(previousMove);
            CntMoves++;

            FirstOrSecondGamer = (FirstOrSecondGamer + 1) % 2;

            var result = GetVarMoves(i, j);

            foreach (var m in result) {
                movex[m.I][m.J] = FirstOrSecondGamer == 0 ? 20 : 21;
            }

            MovesLeft = result.Count();
            previousPossibleMove = result;

            StreamWriter sw = new StreamWriter("debug.txt");
            for (int ii = 0; ii < 10; ii++) {
                for (int jj = 0; jj < 10; jj++) {
                    sw.Write(movex[ii][jj] + " ");
                }
                sw.WriteLine();
            }
            sw.Close();

            return result;
        }

        public List<Pair> GetVarMoves(int i, int j) {
            List<Pair> result = new List<Pair>();

            if (i + 1 < 10) {
                if (j + 2 < 10) {
                    Pair move = new Pair(i + 1, j + 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (j - 2 >= 0) {
                    Pair move = new Pair(i + 1, j - 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }
            if (i - 1 >= 0) {
                if (j + 2 < 10) {
                    Pair move = new Pair(i - 1, j + 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (j - 2 >= 0) {
                    Pair move = new Pair(i - 1, j - 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }
            if (j + 1 < 10) {
                if (i + 2 < 10) {
                    Pair move = new Pair(i + 2, j + 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (i - 2 >= 0) {
                    Pair move = new Pair(i - 2, j + 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }
            if (j - 1 >= 0) {
                if (i + 2 < 10) {
                    Pair move = new Pair(i + 2, j - 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (i - 2 >= 0) {
                    Pair move = new Pair(i - 2, j - 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }

            return result;
        }

        /// <summary>
        /// Проверка возможно ли сделать ход в выбранное поле
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool CanMakeMove(int i, int j) {
            bool flag = false;
            foreach (var previousNextPossibleMove in previousPossibleMove) {
                if (i == previousNextPossibleMove.I && j == previousNextPossibleMove.J) {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public GameClient() {
            FirstOrSecondGamer = 0;
            CntMoves = 0;
            OldMove = new HashSet<Pair>();

            movex = new int[10][];
            for (int i = 0; i < 10; i++)
                movex[i] = new int[10];
        }
    }
}
