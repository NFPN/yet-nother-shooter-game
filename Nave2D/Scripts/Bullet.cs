using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Nave2D.Scripts
{
    class Bullet : BaseObject
    {
        public Vector2 inactivePosition;
        public Bullet() { Status = Status.Active;  inactivePosition = new Vector2(); }

        public override void Update(GameTime time)
        {
            if (Status == Status.Active)
            {
                Move(posY: Direction.Y);
                
                if (Position.Y < 0)
                    Status = Status.Inactive;
            }
            else
                Position = inactivePosition;

            UpdateTransform();
            //base.Update(time);
        }
    }
}
