using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace FormExample
{
    public partial class Form1 : Form
    {
        Random rng = new Random();
        bool startup = true;
        int x;
        int y;
        int turn;
        int winner;
        int cellSize;
        int margin = 10;
        int row = -1;
        int col = -1;

        int[][] board = new int[3][] {new int[3] { 0, 0, 0 }, new int[3] { 0, 0, 0 }, new int[3] { 0, 0, 0 }};
        //The board is a 3x3 array. 0 means the cell is empty, 1 is X, and 2 is O.

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UpdateSize();
            NewGame();
            turn = 1;
        }

        private void UpdateSize()
        {
            cellSize = (Math.Min(ClientSize.Width, ClientSize.Height) - 2 * margin) / 3;
            if(ClientSize.Width > ClientSize.Height)
            {
                x = (ClientSize.Width - 3 * cellSize) / 2;
                y = margin;
            }
            else
            {
                x = margin;
                y = (ClientSize.Height - 3 * cellSize) / 2;
            }
        }

        protected override void OnResize(EventArgs e)
        {

            base.OnResize(e);
            UpdateSize();
            Refresh();
        }

        //Sets up a new game. Resets winner to 0, clears board, and randomizes the turn.
        private void NewGame()
        {
            winner = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[j][i] = 0;
                }
            }
            turn = rng.Next(1, 3);
            Refresh();
        }


        //Checks for a winner. Returns 0 if no winner, 1 if X, and 2 if 0.
        private int CheckWin()
        {
            /*
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[j][i]);
                    if(j != 2) Console.Write("-");
                }
                Console.WriteLine();
            }*/
            //Checks Top Row and Left Column, returning the link (0,0), if a win is found.
            if (board[0][0].Equals(board[0][1]) && board[0][1].Equals(board[0][2])
             || board[0][0].Equals(board[1][0]) && board[1][0].Equals(board[2][0]))
            {
                if(board[0][0] != 0) return board[0][0];
            }
            //Checks Middle Row and Middle Column, returning the link (1,1), if a win is found.
            if (board[1][0].Equals(board[1][1]) && board[1][1].Equals(board[1][2])
             || board[0][1].Equals(board[1][1]) && board[1][1].Equals(board[2][1]))
            {
                if (board[1][1] != 0) return board[1][1];
            }
            //Checks Bottom Row and Right Column, returning the link (2,2), if a win is found.
            if (board[2][0].Equals(board[2][1]) && board[2][1].Equals(board[2][2])
             || board[0][2].Equals(board[1][2]) && board[1][2].Equals(board[2][2]))
            {
                if (board[2][2] != 0) return board[2][2];
            }
            //Checks both of the Diagonals, returning the link (1,1), if a win is found.
            if (board[0][0].Equals(board[1][1]) && board[1][1].Equals(board[2][2])
             || board[0][2].Equals(board[1][1]) && board[1][1].Equals(board[2][0]))
            {
                if (board[1][1] != 0) return board[1][1];
            }
            return 0;
        }

        private bool BoardFull()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (board[j][i] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.N)
            {
                NewGame();
            }
            else if(e.KeyCode == Keys.D1)
            {
                NewGame();
                turn = 1;
            }
            else if (e.KeyCode == Keys.D2)
            {
                NewGame();
                turn = 2;
            }
            else if (e.KeyCode == Keys.H)
            {
                MessageBox.Show("\tHelp Menu\nControls:\nN: New Game\n1: New Game (X Starts)\n2: New Game (O Starts)\nT: Shows the current Turn.");
            }
            else if (e.KeyCode == Keys.T)
            {
                if (turn == 1)
                {
                    MessageBox.Show("It is X's Turn.");
                }
                if (turn == 2)
                {
                    MessageBox.Show("It is O's Turn.");
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            col = (int)Math.Floor((e.X - x) * 1.0 / cellSize);
            row = (int)Math.Floor((e.Y - y) * 1.0 / cellSize);
            if (col < 3 && col >= 0  && row < 3 && row >= 0)
            {
                if (turn == 1)
                {
                    if (board[col][row] == 0)
                    {
                        board[col][row] = 1;
                        turn = 2;
                    }
                }
                else
                {
                    if (board[col][row] == 0)
                    {
                        board[col][row] = 2;
                        turn = 1;
                    }
                }
                //  Console.WriteLine(winner = CheckWin());
                winner = CheckWin();
            }
            Refresh();
            if (winner != 0)
            {
                if (winner == 1)
                {
                    MessageBox.Show("The Winner is X!");
                }
                else
                {
                    MessageBox.Show("The Winner is O!");
                }
                NewGame();
            }
            else if(BoardFull())
            {
                MessageBox.Show("The Game is a Tie!");
                NewGame();
            }
            //base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);    
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Rectangle rect = new Rectangle(x + i * cellSize, y + j * cellSize, cellSize, cellSize);
                    e.Graphics.DrawRectangle(Pens.AntiqueWhite, rect);
                    System.Drawing.Font font = new System.Drawing.Font("Ubuntu", cellSize * 3 * 72 / 96 / 4);
                    if (board[j][i] == 1)
                    {
                        e.Graphics.DrawString("X", font, Brushes.CadetBlue, x + (cellSize / 8) + (j * cellSize), y + (cellSize / 8) + (i * cellSize) - (cellSize / 20));
                    }
                    else if (board[j][i] == 2)
                    {
                        e.Graphics.DrawString("O", font, Brushes.Gray, x + (cellSize / 8) + (j * cellSize) - (cellSize / 20), y + (cellSize / 8) + (i * cellSize) - (cellSize / 20));
                    }
                }
            }
            if(startup)
            {
                MessageBox.Show("Welcome to Tic-Tac-Toe!\nControls:\nN: New Game\n1: New Game(X Starts)\n2: New Game(O Starts)\nT: Shows the current Turn.\nH: Shows the Help Menu (Essentially this box.)");
                startup = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    
}
