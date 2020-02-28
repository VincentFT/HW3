using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;

namespace MySpaceGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
        private static List<Bullet> _bullet = new List<Bullet>();
        private static Asteroid[] _asteroids;
        private static Ship _ship;
        private static PowerUp[] _powerup;
        private static int score = 0;
                
        //Число объектов
        const int numOfGalaxies = 5;
        const int numOfStars = 30;
        const int numOfAsteroids = 20;
        const int numOfSmallStars = 100;
        const int numOfPowerUp = 3;
        //Скорости объектов
        const int smallStarSpeed = 5;
        const int starSpeed = 1;
        const int galaxiesSpeed = 6;
        const int asteroidSpeed = 10;
        const int powerUpSpeed = 8;
        const int laserSpeed = 15;
        //Размеры объектов
        const int maxSize = 30;
        const int minSize = 10;
        const int starMaxSize = maxSize / 2;
        const int starMinSize = minSize / 2;
        const int galaxiesMaxSize = maxSize * 8;
        const int galaxiesMinSize = minSize * 5;
        const int powerUpMinSize = 15;
        const int powerUpMaxSize = 30;
        const int shipWidth = 60;
        const int shipHeight = 30;
        //Ограничения объектов
        const int formSizeLimit = 1000;
        const int speedLimit = 30;

                       
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Game()
        {
        }
                
        public static void Load()
        {
            try
            {
                _objs = new BaseObject[numOfStars + numOfGalaxies + numOfSmallStars];
                                
                _ship = new Ship(new Point(5, 400), new Point(5, 5), new Size(shipWidth, shipHeight));

                _asteroids = new Asteroid[numOfAsteroids];

                _powerup = new PowerUp[numOfPowerUp];

                for (int i = 0; i < _objs.Length - numOfStars - numOfGalaxies; i++)
                {
                    _objs[i] = new SmallStar(new Point(Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Width),
                        Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Height)),
                        new Point(myRandom.RandomIntNumber(-smallStarSpeed, -1), 0), new Size(1, 1));
                }

                for (int i = _objs.Length - numOfStars - numOfGalaxies; i < _objs.Length - numOfGalaxies; i++)
                {
                    int size = myRandom.RandomIntNumber(starMinSize, starMaxSize);
                    int widthPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Width);
                    int heightPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Height);
                    int speed = myRandom.RandomIntNumber(-starSpeed * 2, -1);

                    _objs[i] = new Star(new Point(widthPosition, heightPosition),
                                new Point(speed, 0), new Size(size, size));

                    if (size < 0)
                        throw new GameObjectException($"Размер объекта {typeof(Star)} меньше нуля", -1);
                    if (speed > 0)
                        throw new GameObjectException($"Объект {typeof(Star)} двигается не в ту сторону", 1);
                    if (speed == 0)
                        throw new GameObjectException($"Объект {typeof(Star)} стоит на месте", 0);

                }

                for (int i = _objs.Length - numOfGalaxies; i < _objs.Length; i++)
                {
                    int size = myRandom.RandomIntNumber(galaxiesMinSize, galaxiesMaxSize);
                    int widthPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Width);
                    int heightPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Height);
                    int speed = myRandom.RandomIntNumber(-galaxiesSpeed, -1);

                    _objs[i] = new Galaxies(new Point(widthPosition, heightPosition),
                                new Point(speed, 0), new Size(size, size));

                    if (size < 0)
                        throw new GameObjectException($"Размер объекта {typeof(Galaxies)} меньше нуля", -1);
                    if (speed > 0)
                        throw new GameObjectException($"Объект {typeof(Galaxies)} двигается не в ту сторону", 1);
                    if (speed == 0)
                        throw new GameObjectException($"Объект {typeof(Galaxies)} стоит на месте", 0);

                }

                for (int i = 0; i < numOfPowerUp; i++)
                {
                    int size = myRandom.RandomIntNumber(powerUpMinSize, powerUpMaxSize);
                    int widthPosition = Width;
                    int heightPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Height);
                    int speed = myRandom.RandomIntNumber(-powerUpSpeed, -1);

                    _powerup[i] = new PowerUp(new Point(widthPosition, heightPosition),
                                new Point(speed, 0), new Size(size, size));

                    if (size < 0)
                        throw new GameObjectException($"Размер объекта {typeof(Galaxies)} меньше нуля", -1);
                    if (speed > 0)
                        throw new GameObjectException($"Объект {typeof(Galaxies)} двигается не в ту сторону", 1);
                    if (speed == 0)
                        throw new GameObjectException($"Объект {typeof(Galaxies)} стоит на месте", 0);

                }

                for (int i = 0; i < _asteroids.Length; i++)
                {
                    int size = myRandom.RandomIntNumber(minSize, maxSize);
                    int widthPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)(Game.Width - size));
                    int heightPosition = Convert.ToInt32(myRandom.RandomDoubleNumber() * (double)Game.Height - size);
                    int speed1 = myRandom.RandomIntNumber(-asteroidSpeed, asteroidSpeed);
                    int speed2 = myRandom.RandomIntNumber(-asteroidSpeed, asteroidSpeed);

                    _asteroids[i] = new Asteroid(new Point(widthPosition, heightPosition),
                                    new Point(speed1, speed2), new Size(size, size));

                    if (size < 0)
                        throw new GameObjectException($"Размер объекта {typeof(Asteroid)} меньше нуля", -1);
                    if (widthPosition < 0 || widthPosition > Game.Width || heightPosition < 0 || heightPosition > Game.Height)
                        throw new GameObjectException($"Объект {typeof(Asteroid)} появился за пределами экрана", 2);
                    if (speed1 == 0 && speed2 == 0)
                        throw new GameObjectException($"Объект {typeof(Asteroid)} стоит на месте", 0);
                    if (Math.Abs(speed1) > speedLimit || Math.Abs(speed2) > speedLimit)
                        throw new GameObjectException($"Объект {typeof(Asteroid)} двигается со слишком большой скоростью", 1);

                }
            }
            catch (GameObjectException ex)
            {
                Debug.WriteLine($"{DateTime.Now.ToString()}: {ex.ToString()}");
            }
        }
                
        static public Timer _timer = new Timer { Interval = 75 };
                
        public static void Init(Form form)
        {
                     
            Graphics g;
            
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width > formSizeLimit || Height > formSizeLimit)
                {
                    throw new ArgumentOutOfRangeException("Высота или ширина формы принимают значение более чем 1000");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                
            }
            
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            _timer.Start();
            _timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;
        }
                
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
                
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                _bullet.Add( 
                            new Bullet(
                                        new Point(_ship.Rect.X + _ship.Width, _ship.Rect.Y + _ship.Height / 2), 
                                        new Point(laserSpeed, 0), 
                                        new Size(5, 2)
                                        )
                );
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }
                       
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            foreach (Bullet b in _bullet)
            {
                b?.Draw();
            }
            _ship?.Draw();

            foreach (PowerUp m in _powerup)
            {
                m?.Draw();
            }

            if (_ship != null)
            {
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                Buffer.Graphics.DrawString("Score:" + score, SystemFonts.DefaultFont, Brushes.White, Game.Width - 100, 0);
            }
            else
                Buffer.Graphics.DrawString("Energy:" + 0, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Render();

        }
                
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet bul in _bullet) bul?.Update();
            foreach (PowerUp pow in _powerup) pow?.Update();

            for (var i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                for (var j = 0; j < _bullet.Count; j++)
                {
                    if (_bullet[j]?.Collision(_asteroids[i]) ?? false)
                    {
                        System.Media.SystemSounds.Beep.Play();
                        _asteroids[i].Recreate();
                        _bullet[j].Destroed();
                        _bullet.RemoveAt(j);
                        score += _asteroids[i].Power;
                        continue;
                    }

                    if (_bullet[j].OutOfScreen())
                        _bullet.RemoveAt(j);
                }

                if (_ship.Collision(_asteroids[i])) {
                    _asteroids[i].Recreate();
                    _ship?.EnergyLow(_asteroids[i].Power);
                    System.Media.SystemSounds.Asterisk.Play();
                    if (_ship.Energy <= 0) _ship?.Die();
                };
            }

            for (int i = 0; i < _powerup.Length; i++)
            {
                if (_ship.Collision(_powerup[i]))
                {
                    _powerup[i].Recreate();
                    _ship?.EnergyHigh(_powerup[i].Power);
                    System.Media.SystemSounds.Exclamation.Play();
                };
            }
        }
                
        public static void Finish()
        {
            Closed();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, Width/3, Height/2);
            Buffer.Render();
        }
                
        public static void Closed()
        {
            _timer.Stop();
            _timer.Tick -= Timer_Tick;

        }

    }
}

