using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatKingdoomRefactored
{
    class Level
    {
        public Texture2D button, button2, button3, cursor, cursor_tap, background, grass, gameOver, createground, winScreen;
        public Vector2 cursor_coords, textboxPosition = new Vector2(560, 30);
        public int[,] arena, kingdoom;
        public Level()
        {
            this.arena = new int[8, 10] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 4, 4, 4, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 4 },
            };
            this.kingdoom = new int[8, 10] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 2, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            };
        }
        public void LoadTextures(Texture2D cursor, Texture2D button, Texture2D button2, Texture2D button3, Texture2D cursor_tap, Texture2D background, Texture2D createground, Texture2D grass, Texture2D gameOver, Texture2D winScreen)
        {
            this.cursor = cursor;
            this.button = button;
            this.button2 = button2;
            this.button3 = button3;
            this.cursor_tap = cursor_tap;
            this.background = background;
            this.createground = createground;
            this.grass = grass;
            this.gameOver = gameOver;
            this.winScreen = winScreen;
        }
    }
}
