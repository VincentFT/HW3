using System.Diagnostics;

namespace MySpaceGame
{
    
    class Logging
    {
        
        internal static void Log(string Msg)
        {
            using (var sw = new System.IO.StreamWriter("data.log", true))
            {
                Debug.WriteLine(Msg);
                sw.WriteLine(Msg);
            }
        }
    }
}
