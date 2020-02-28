using System;
using System.Collections.Generic;
using System.Drawing;

namespace MySpaceGame
{
    class Galaxies : BaseObject
    {
        
        List<Bitmap> bitMapList = new List<Bitmap>() {
            new Bitmap("..\\..\\img/galaxies/galaxy1.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy2.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy3.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy4.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy5.png"),
            //new Bitmap("..\\..\\img/galaxies/galaxy6.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy7.png"),
            //new Bitmap("..\\..\\img/galaxies/galaxy9.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy10.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy12.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy13.png"),
            new Bitmap("..\\..\\img/galaxies/galaxy14.png"),  };


        Bitmap image;
            
        public Galaxies(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            image = bitMapList[myRandom.RandomIntNumber(0,bitMapList.Count)];
        }
                
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
                
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0 - Size.Width)
            {
                Pos.X = Game.Width + Size.Width;
                image = bitMapList[myRandom.RandomIntNumber(0, bitMapList.Count)];
                Pos.Y = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Height);
                Size.Width = Convert.ToInt32((myRandom.RandomDoubleNumber() + 0.5) * Size.Width);
                Size.Height = Size.Width;
                Dir.X = Convert.ToInt32((myRandom.RandomDoubleNumber() + 0.5) * Dir.X);
                switch (myRandom.RandomIntNumber(0, 3))
                {
                    case 0:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 1:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case 2:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                }
            }
        }
    }
}
