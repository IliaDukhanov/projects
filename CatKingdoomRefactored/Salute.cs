using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatKingdoomRefactored
{
    class Salute:Object
    {
        public Texture2D background;
        public Point frame = new Point(0, 0);
        public Point size = new Point(8, 3);
        int timer = 0;
        public Salute(Texture2D texture, Texture2D background, int frameWidth, int frameHeight)
        {
            this.background = background;
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            position = new Vector2(400, 520);
        }
        public void Start()
        {
            frame = Animate(frame, new Point(0, 0), size);
            timer++;
            if (timer >= 24)
            {
                position = new Vector2(new Random().Next(1366), new Random().Next(768));
                timer = 0;
            }
        }
    }
}
