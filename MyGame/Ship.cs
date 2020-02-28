using System;
using System.Collections.Generic;
using System.Drawing;


namespace MySpaceGame
{
    class Ship : BaseObject
    {
        private static int maxEnergy = 100;

        private int _energy = maxEnergy;        

        public int Energy => _energy;

        public int Width => Size.Width;

        public int Height => Size.Height;

        Bitmap image = new Bitmap("..\\..\\img/ship.png");

        public static event Action<string> shipDie;
        public static event Action<string> shipEnergyLow;
        public static event Action<string> shipEnergyHigh;

        public static event Message MessageDie;
                       
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
                
        public void EnergyLow(int n)
        {
            _energy -= n;
            shipEnergyLow?.Invoke($"{DateTime.Now}: Корабль получил повреждения");
        }
                
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
                
        public override void Update()
        {
        }
                
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
                
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
                
        public void Die()
        {
            shipDie?.Invoke($"{DateTime.Now}: Корабль был уничтожен");
        }
                
        internal void EnergyHigh(int power)
        {
            if (_energy < maxEnergy)
            {
                if (_energy + power > maxEnergy)
                {
                    _energy = maxEnergy;
                }
                else
                {
                    _energy += power;
                }
                shipEnergyHigh?.Invoke($"{DateTime.Now}: Корабль восстановил энергию");
            }
        }
    }

}
