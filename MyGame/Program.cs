using System;
using System.Drawing;
using System.Windows.Forms;

namespace MySpaceGame

//Алексей Петриленков

//1. Добавить космический корабль, как описано в уроке.
//2. Добработать игру «Астероиды».
//а) Добавить ведение журнала в консоль с помощью делегатов;
//б) * Добавить это и в файл.
// 3. Разработать аптечки, которые добавляют энергию.
// 4. Добавить подсчет очков за сбитые астероиды.
{
    class Program
    {
        static void Main(string[] args)
        {
            Form menu = new Form();
            menu.Width = 800;
            menu.Height = 600;


            SplashScreen.Init(menu);
            menu.Show();
            SplashScreen.Draw();

            Button startBtn = new Button
            {
                Text = "Старт",
                Location = new System.Drawing.Point(menu.Width / 2 - 30, menu.Height / 3)

            };
            startBtn.Click += startBtnClick;
            menu.Controls.Add(startBtn);

            Button recordsBtn = new Button
            {
                Text = "Рекорды",
                Location = new System.Drawing.Point(menu.Width / 2 - 30, menu.Height - menu.Height / 2)

            };
            recordsBtn.Click += recordsBtnClick;
            menu.Controls.Add(recordsBtn);

            Button exitBtn = new Button
            {
                Text = "Выход",
                Location = new System.Drawing.Point(menu.Width / 2 - 30, menu.Height - menu.Height / 3)

            };
            exitBtn.Click += exitBtnClick;
            menu.Controls.Add(exitBtn);

            Label autor = new Label
            {
                Text = "Алексей Петриленков".ToString(),
                ForeColor = System.Drawing.Color.Gray,
                BackColor = System.Drawing.Color.Black,
                AutoSize = true,
                Font = new Font("Arial Narrow", 11, FontStyle.Bold),
                Location = new System.Drawing.Point(menu.Width / 50, menu.Height - menu.Height / 8)

            };
            menu.Controls.Add(autor);

            Application.Run(menu);


            void exitBtnClick(object sender, EventArgs e)
            {
                menu.Close();
            }

            void recordsBtnClick(object sender, EventArgs e) { }

            void startBtnClick(object sender, EventArgs e)
            {
                Form game = new Form()
                {
                    Width = (Screen.PrimaryScreen.Bounds.Width/10)*9,
                    Height = (Screen.PrimaryScreen.Bounds.Height/10)*9
                };
                //game.Width = 800;
                //game.Height = 600;
                Game.Init(game);
                game.FormClosed += Game_FormClosed;
                game.Show();
                Game.Draw();
            }
        }
            
        private static void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Game.Closed();
        }
    }

}
