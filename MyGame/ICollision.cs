using System.Drawing;

namespace MySpaceGame
{
    
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }

    }
}
