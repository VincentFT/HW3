using System;
using System.Windows.Forms;
using System.Drawing;

namespace MySpaceGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static BaseObject[] _objs;
                        
        public static int Width { get; set; }
        
        public static int Height { get; set; }

        static SplashScreen()
        {
        }
                
        public static void Load()
        {
            
        }
                
        public static void Init(Form form)
        {
                       
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
   
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
           
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;

        }
                
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
        }
                
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Render();
        }
                
        public static void Update()
        {
           
        }


    }
}
