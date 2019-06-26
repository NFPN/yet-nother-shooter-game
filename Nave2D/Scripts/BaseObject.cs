using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace Nave2D.Scripts
{
    /// <summary>
    /// Base class for all Game Objects
    /// </summary>
    public class BaseObject
    {
        public ObjType Type;
        public Status Status;

        public float Speed;
        public float Life;

        public Input Input;
        public Texture2D Image;
        public Color Color = Color.White;
        public Vector2 Size = new Vector2();
        public Vector2 Position = new Vector2();
        public Vector2 Direction = new Vector2();
        public Rectangle Transform = new Rectangle();
        public GraphicsDeviceManager Graphics;

        int run = 0;

        public virtual void Update(GameTime time)
        {
            //Makes sure collision only occurs at actual sprite size
            UpdateTransform();

            #region Movement

            if (Input == null)
                return;
            //focus
            run = Keyboard.GetState().IsKeyDown(Input.Focus) ? 2 : 5;

            if (Keyboard.GetState().IsKeyDown(Input.Left) && Transform.X > 0)
                Move(-run);
            if (Keyboard.GetState().IsKeyDown(Input.Right) && Transform.X < (Graphics.PreferredBackBufferWidth - Transform.Width))
                Move(run);
            if (Keyboard.GetState().IsKeyDown(Input.Up) && Transform.Y > 0)
                Move(posY: -run);
            if (Keyboard.GetState().IsKeyDown(Input.Down) && Transform.Y < (Graphics.PreferredBackBufferHeight - Transform.Height))
                Move(posY: run);
            #endregion
        }

        public void Move(float posX = 0, float posY = 0)
        {
            Position.X += posX * Speed;
            Position.Y += posY * Speed;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Image, Transform, Color);
        }

        public void UpdateTransform()
        {
            Transform = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }
    }
}
