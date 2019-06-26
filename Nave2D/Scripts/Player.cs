using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Nave2D.Scripts
{
    public class Player : BaseObject
    {
        public Player() { Status = Status.Active; }

        public Player
        (
            Color color,
            Vector2 size,
            Texture2D image,
            Input playerInput,

            GraphicsDeviceManager graphics,

            float speed = 1,
            float life = 100,
            Status status = Status.Active,
            ObjType type = ObjType.Player,
            Vector2 position = new Vector2(), 
            Vector2 direction = new Vector2(),
            Rectangle transform = new Rectangle()
        )
        {
            
            //SetDirection(direction.X,Direction.Y);

            Type = type;
            Life = life;
            Size = size;
            Speed = speed;
            Color = color;
            Image = image;
            Input = playerInput;
            Graphics = graphics;
            Position = position;
            Transform = transform;
            Status = Status.Active;
        }
    }
}
