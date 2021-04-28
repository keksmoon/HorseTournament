using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameClientSpace {

    [Serializable]
    public class Move {
        public int I, J, IJ;

        public Move() { }

        public Move(int I, int J) {
            this.I = I;
            this.J = J;
            IJ = int.Parse($"{I}{J}");
        }

        public override bool Equals(object obj) {
            var other = obj as Move;
            if (other == null) return false;

            return IJ == other.IJ;
        }

        public override int GetHashCode() => 1;
    }

    [Serializable]
    public class GameClient {

        //Запоминаем предыдущие позиции, чтобы их возвращать в исходное состояние
        public List<Move> previousPossibleMove; // Предыдущие возможные ходы

        public Move previousGreenMove;
        public Move previousRedMove;// Предыдущий ход

        public int[][] movex; // Матрица ходов

        public HashSet<Move> OldMove; // Завоеванные замки
        public int FirstOrSecondGamer; // Номер ходящего игрока
        public int MovesLeft; // Количество возможных ходов

        public int CntMoves; // Количество ходов в партии

        public int GameMode;

        public void MoveTo(int i, int j) {
            movex[i][j] = FirstOrSecondGamer == 0 ? 30 : 31;

            // 0 - зеленый
            // 1 - красный

            if (OldMove.Count > 0) {
                var previousMove = OldMove.Last();
                movex[previousMove.I][previousMove.J] = FirstOrSecondGamer == 0 ? 11 : 10;

                if (movex[previousMove.I][previousMove.J] % 10 == 0) previousGreenMove = previousMove;
                else previousRedMove = previousMove;
            }

            if (previousPossibleMove != null) {
                foreach (var m in previousPossibleMove) {
                    if (m.I == i && m.J == j) continue;
                    movex[m.I][m.J] = 0;
                }
            }

            OldMove.Add(new Move(i, j));
            CntMoves++;

            FirstOrSecondGamer = (FirstOrSecondGamer + 1) % 2;

            var result = GetVarMoves(i, j);
            foreach (var m in result) movex[m.I][m.J] = FirstOrSecondGamer == 0 ? 20 : 21;

            MovesLeft = result.Count();
            previousPossibleMove = result;
        }

        public List<Move> GetVarMoves(int i, int j) {
            var result = new List<Move>();
            Move move;

            if (i + 1 < 10) {
                if (j + 2 < 10) {
                    move = new Move(i + 1, j + 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (j - 2 >= 0) {
                    move = new Move(i + 1, j - 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }
            if (i - 1 >= 0) {
                if (j + 2 < 10) {
                    move = new Move(i - 1, j + 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (j - 2 >= 0) {
                    move = new Move(i - 1, j - 2);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }
            if (j + 1 < 10) {
                if (i + 2 < 10) {
                    move = new Move(i + 2, j + 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (i - 2 >= 0) {
                    move = new Move(i - 2, j + 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }
            if (j - 1 >= 0) {
                if (i + 2 < 10) {
                    move = new Move(i + 2, j - 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
                if (i - 2 >= 0) {
                    move = new Move(i - 2, j - 1);
                    if (!OldMove.Contains(move))
                        result.Add(move);
                }
            }

            return result;
        }

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

        public void LogDebug() {
            StreamWriter sw = new StreamWriter("debug.txt");
            for (int ii = 0; ii < 10; ii++) {
                for (int jj = 0; jj < 10; jj++) {
                    sw.Write(movex[ii][jj] + " ");
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        public Move GetRobotMove() {
            //int max = int.MaxValue; int minMove = 0;
            //for (int i = 0; i < previousPossibleMove.Count; i++) {
            //    var s = GetVarMoves(previousPossibleMove[i].I, previousPossibleMove[i].J);
            //    if (s.Count < max) {
            //        max = s.Count;
            //        minMove = i;
            //    }
            //}

            int Move = (new Random(DateTime.Now.Millisecond)).Next(0, MovesLeft);

            var newMove = previousPossibleMove[Move];
            MoveTo(newMove.I, newMove.J);

            return newMove;
        }

        public GameClient(int gameMode) {
            FirstOrSecondGamer = 0;
            CntMoves = 0;
            OldMove = new HashSet<Move>();

            movex = new int[10][];
            for (int i = 0; i < 10; i++)
                movex[i] = new int[10];

            GameMode = gameMode;
        }
    }
}