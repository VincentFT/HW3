using System;
using System.Drawing;

namespace MySpaceGame
{
    class Bullet : BaseObject
    {

        public static event Action<string> bulletOutOfScreen;
        public static event Action<string> bulletDestroed;
                
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
                
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
                
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }
                
        public void Recreate()
        {
            Pos.X = 0;
            Pos.Y = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)(Game.Height - Size.Height));
        }
                
        public bool OutOfScreen()
        {
            if (Pos.X > Game.Width)
            {
                bulletOutOfScreen?.Invoke($"{DateTime.Now}: Лазер вылетел за пределы экрана");
                return true;
            }
            else
                return false;
        }
                
        internal void Destroed()
        {
            bulletDestroed?.Invoke($"{DateTime.Now}: Лазерный луч деградировал!");
        }
    }

}
