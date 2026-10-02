using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;


        enum enPlayer
        {
            Player1, Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            InProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }
       
        private void ResetImage(PictureBox image)
        {
            image.Tag = "?";
            image.Image = Resources.NOT_Image;
        }

        private void RestartGame()
        {
            
            ResetImage(pictureBox1);
            ResetImage(pictureBox2);
            ResetImage(pictureBox3);
            ResetImage(pictureBox4);
            ResetImage(pictureBox5);
            ResetImage(pictureBox6);
            ResetImage(pictureBox7);
            ResetImage(pictureBox8);
            ResetImage(pictureBox9);

            PlayerTurn = enPlayer.Player1;
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.InProgress;

            //reset labels
            lblTurn.ForeColor = Color.FromArgb(255, 128, 255);
            lblTurn.Text = "X Player1";

            LblWinner.Image = Resources.In_Process1;
            LblWinner.ForeColor = Color.White;
            LblWinner.Text = "   In Progress";


        }
        private void EndGame()
        {
            lblTurn.ForeColor = Color.FromArgb(155, 213, 254);
            lblTurn.Text = "Game Over";

            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                     LblWinner.ForeColor = Color.FromArgb(255, 128, 255);
                    LblWinner.Image = null;
                    LblWinner.Text = "   X Player1";
                    break;

                case enWinner.Player2:
                    LblWinner.ForeColor = Color.FromArgb(212, 255, 114);
                    LblWinner.Image = null;
                    LblWinner.Text = "   O Player2";
                    break;

                case enWinner.Draw:
                    LblWinner.ForeColor = Color.FromArgb(155, 213, 254);
                    LblWinner.Image = null;
                    LblWinner.Text = "   Draw";
                    break;


            }
            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private bool CheckValues(PictureBox Picture1, PictureBox Picture2, PictureBox Picture3)
        {
            if (Picture1.Tag.ToString() != "?" && Picture1.Tag.ToString() == Picture2.Tag.ToString() && Picture1.Tag.ToString() == Picture3.Tag.ToString())
            {

                if (Picture1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
            }
            GameStatus.GameOver = false;
            return false;
        }

        private void CheckWinner(PictureBox pictureBox)
        {
            //Check Rows
            if (CheckValues(pictureBox1, pictureBox2, pictureBox3))
                return;

            //Check Rows
            if (CheckValues(pictureBox4, pictureBox5, pictureBox6))
                return;

            //Check Rows
            if (CheckValues(pictureBox7, pictureBox8, pictureBox9))
                return;

            //Check Cols
            if (CheckValues(pictureBox1, pictureBox4, pictureBox7))
                return;

            //Check Cols
            if (CheckValues(pictureBox2, pictureBox5, pictureBox8))
                return;

            //Check Cols
            if (CheckValues(pictureBox3, pictureBox6, pictureBox9))
                return;

            //Check Diagonal1
            if (CheckValues(pictureBox1, pictureBox5, pictureBox9))
                return;

            //Check Diagonal2
            if (CheckValues(pictureBox3, pictureBox5, pictureBox7))
                return;


        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Color SkyBlue = Color.FromArgb(155, 213, 254);

            //Pen pen = new Pen(SkyBlue);
            //pen.Width = 5;


            //e.Graphics.DrawCurve(pen,)




        }


        private void DrawBoard(PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255);
            Pen WhitePen = new Pen(white, 7);

            WhitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            WhitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //Draw columns
            e.Graphics.DrawLine(WhitePen, 400, 100, 400, 580);
            e.Graphics.DrawLine(WhitePen, 200, 100, 200, 580);

            //Draw Rows
            e.Graphics.DrawLine(WhitePen, 50, 250, 550, 250);
            e.Graphics.DrawLine(WhitePen, 50, 430, 550, 430);


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int w = 600; // Width of the board
            int h = 595; // Total height of the board
            int x = (panel1.Width - w) / 2;
            int y = (panel1.Height - h) / 2;

            Color SkyBLue = Color.FromArgb(155, 213, 254);

            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            using (SolidBrush fillBrush = new SolidBrush(SkyBLue)) // Brush to fill the inside
            using (Pen borderPen = new Pen(SkyBLue, 4f))          // Pen to draw the outer border
            {
                // 1. Draw the top curved arch (semi-circle)
                path.AddArc(x, y, w, w, 180, 180);

                // 2. Draw the vertical side lines down to the base
                path.AddLine(x + w, y + (w / 2), x + w, y + h); // Right side
                path.AddLine(x + w, y + h, x, y + h);           // Bottom flat line
                path.AddLine(x, y + h, x, y + (w / 2));         // Left side

                path.CloseFigure(); // Ensures the shape is properly sealed for filling

                // 3. Fill the interior background first
                g.FillPath(fillBrush, path);

                // 4. Render the outline path on top
                g.DrawPath(borderPen, path);

                // 5. Draw the tic-tac-toe grid lines and elements
                DrawBoard(e);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChangeImage(PictureBox Picture)
        {
            if (Picture.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        Picture.Image = Resources.X_Image_Tic_Tac_Toe;
                        lblTurn.ForeColor = Color.FromArgb(212, 255, 114);
                        PlayerTurn = enPlayer.Player2;
                        lblTurn.Text = "O Player2";
                        GameStatus.PlayCount++;
                        Picture.Tag = "X";
                        CheckWinner(Picture);
                        break;
                    case enPlayer.Player2:
                        Picture.Image = Resources.O_Image_Tic_Tac_Toe;
                        lblTurn.ForeColor = Color.FromArgb(255, 128, 255);
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "X Player1";
                        GameStatus.PlayCount++;
                        Picture.Tag = "O";
                        CheckWinner(Picture);
                        break;
                }

            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!GameStatus.GameOver)
            {
                if (GameStatus.PlayCount == 9)
                {
                    GameStatus.Winner = enWinner.Draw;
                    EndGame();
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ChangeImage((PictureBox) sender);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}

    


