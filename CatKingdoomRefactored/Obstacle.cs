using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatKingdoomRefactored
{
    public class Obstacle:Object
    {
        public Obstacle(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int shape)
        {
            this.texture = texture;
            this.position = position;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.shape = shape;
        }
    }
}
