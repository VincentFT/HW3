using System;

namespace MySpaceGame
{
    class GameObjectException : Exception
    {
        public int Error { get; set; }
                
        public GameObjectException(string Msg, int Error) : base(Msg)
        {
            this.Error = Error;
        }
               
        public override string ToString()
        {
            return $"{base.Message}. Код ошибки: {this.Error}";
        }

    }
}
