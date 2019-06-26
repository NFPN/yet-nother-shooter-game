using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nave2D.Scripts
{
    class Background
    {
        public float speed;
        public Texture2D image;
        public Color color = Color.White;

        public Vector2 size = new Vector2();
        public Vector2 position = new Vector2();
        public Vector2 direction = new Vector2(0,1);

        public void Draw(SpriteBatch batch)
        {
           batch.Draw(image, position, color);
        }

        public void Move(float deltaTime, bool isRunning = false)
        {
            if (isRunning) position += speed * direction * deltaTime * 2;
            else position += speed * direction * deltaTime;
        }
    }
}
