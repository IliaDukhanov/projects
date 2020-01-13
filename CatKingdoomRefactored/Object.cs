using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatKingdoomRefactored
{
    public class Object
    {
        public Texture2D texture;
        public Vector2 position;
        public int frameWidth;
        public int frameHeight;
        public int shape = 0;
        public Point Animate(Point currentFrame, Point beginFrame, Point size)
        {
            ++currentFrame.X;
            if (currentFrame.X >= size.X)
            {
                currentFrame.X = beginFrame.X;
                ++currentFrame.Y;
                if (currentFrame.Y >= size.Y)
                    currentFrame.Y = beginFrame.Y;
            }
            return currentFrame;
        }
    }
}
