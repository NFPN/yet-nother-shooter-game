using Microsoft.Xna.Framework.Input;

namespace Nave2D.Scripts
{
    public class Input
    {
        public Keys Up { get; set; } = Keys.Up;
        public Keys Down { get; set; } = Keys.Down;
        public Keys Left { get; set; } = Keys.Left;
        public Keys Right { get; set; } = Keys.Right;

        public Keys Focus { get; set; } = Keys.LeftShift;
        public Keys Shoot { get; set; } = Keys.Z;
    }
}
