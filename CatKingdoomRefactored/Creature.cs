using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CatKingdoomRefactored
{
    public class Creature : Object
    {
        public Point moveFrame, moveSize, idleFrame, idleSize, atackFrame, atackSize, dieFrame, dieSize;
        public int health;
        public int damage;
        public int speed;
        public int timer = 0;
        public SoundEffect death, atack, payment;
        public Creature(Texture2D texture,
            Vector2 position, int frameWidth,
            int frameHeight, Point moveFrame,
            Point moveSize, Point idleFrame,
            Point idleSize, Point atackFrame,
            Point atackSize, Point dieFrame,
            Point dieSize, int health,
            int damage, int speed)
        {
            this.texture = texture;
            this.position = position;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.moveFrame = moveFrame;
            this.moveSize = moveSize;
            this.idleFrame = idleFrame;
            this.idleSize = idleSize;
            this.atackFrame = atackFrame;
            this.atackSize = atackSize;
            this.dieFrame = dieFrame;
            this.dieSize = dieSize;
            this.health = health;
            this.damage = damage;
            this.speed = speed;
        }

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

        public bool IsCollide(Object sp1, Object sp2)
        {
            if (sp1.position.X < sp2.position.X + sp2.frameWidth &&
                sp1.position.X + sp1.frameWidth > sp2.position.X + sp2.shape &&
                sp1.position.Y < sp2.position.Y + sp2.frameHeight &&
                sp1.position.Y + sp1.frameHeight > sp2.position.Y + sp2.shape)
            {
                return true;
            }
            else return false;
        }
    }
}
