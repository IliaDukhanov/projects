using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace CatKingdoomRefactored
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D button, button2, button3, cursor, cursor_tap, background, grass, salute, gameOver, createground, saluteBackground;
        Vector2 cursor_coords, salutePosition = new Vector2(400, 520), textboxPosition = new Vector2(560, 30);
        int saluteFrameWidth = 128;
        int saluteFrameHeight = 128;
        Point saluteFrame = new Point(0, 0);
        Point saluteSize = new Point(8, 3);
        Player player;
        List<Enemy> enemies = new List<Enemy>();
        List<Obstacle> rocks = new List<Obstacle>();
        List<Building> buildings = new List<Building>();
        List<Winners> winners = new List<Winners>();
        GameSet gameSet = GameSet.Menu, pgSet;
        public int[,] Layer, LayerBase; SpriteFont congra; KeyboardState oldKeyboardState, currentKeyboardState; int colorPreset = 0; bool presed = false; Song backMusic, endMusic;




        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50);
        }

        protected override void Initialize()
        {
            Layer = new int[8, 10] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 4, 4, 4, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 4 },
            };
            LayerBase = new int[8, 10] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 2, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            };
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            saluteBackground = Content.Load<Texture2D>("saluteBackground");
            congra = Content.Load<SpriteFont>("congra");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cursor = Content.Load<Texture2D>("menu_cursor");
            button = Content.Load<Texture2D>("menu_button");
            button2 = Content.Load<Texture2D>("menu_button2");
            button3 = Content.Load<Texture2D>("menu_button3");
            cursor_tap = Content.Load<Texture2D>("menu_cursor_tap");
            background = Content.Load<Texture2D>("background");
            createground = Content.Load<Texture2D>("createground");
            grass = Content.Load<Texture2D>("grass");
            salute = Content.Load<Texture2D>("salute");
            gameOver = Content.Load<Texture2D>("gameOver");
            backMusic = Content.Load<Song>("backMusic");
            endMusic = Content.Load<Song>("menuMusic");
            MediaPlayer.Play(backMusic);
            int a = 0, b = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Layer[i, j] == 1)
                    {
                        enemies.Add(new Enemy(Content.Load<Texture2D>("enemySprite"), new Vector2(j * 136, i * 96), 64, 64, new Point(0, 1), new Point(5, 2), new Point(0, 0), new Point(4, 1), new Point(0, 2), new Point(6, 3), new Point(0, 3), new Point(6, 4), 50, 6, 5, Content.Load<SoundEffect>("enemyAtack"), Content.Load<SoundEffect>("enemyDeath")));
                    }

                    if (Layer[i, j] == 2)
                    {
                        a = j;
                        b = i;
                    }

                    if (Layer[i, j] == 3)
                        rocks.Add(new Obstacle(Content.Load<Texture2D>("tree"), new Vector2(j * 136, i * 96), 35, 75, 75));

                    if (Layer[i, j] == 4)
                        rocks.Add(new Obstacle(Content.Load<Texture2D>("rock"), new Vector2(j * 136, i * 96), 34, 14, 30));
                    //if (LayerBase[i, j] == 1)
                    //{
                    //    buildings.Add(new Building(Content.Load<Texture2D>("Kingdoom"), new Vector2(j * 136, i * 96 - 20), 150, 150, 20, 10, Keys.K));
                    //}
                    if (LayerBase[i, j] == 2)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("warhouse"), new Vector2(j * 136 + 40, i * 96 - 20), 200, 150, 50, 10, Keys.W, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 3)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("market"), new Vector2(j * 136, i * 96), 200, 150, 50, 10, Keys.M, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 4)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("rally"), new Vector2(j * 136, i * 96), 150, 170, 15, 10, Keys.R, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 5)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("wall"), new Vector2(j * 136, i * 96), 400, 70, 20, 10, Keys.D, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                }
            }
            player = new Player(Content.Load<Texture2D>("sprite"), new Vector2(a * 136, b * 96), 64, 64, new Point(0, 1), new Point(8, 2), new Point(0, 0), new Point(4, 1), new Point(0, 9), new Point(10, 10), new Point(0, 4), new Point(9, 5), 100, 5, 5, Content.Load<Texture2D>("heart"), Content.Load<Texture2D>("sword"), Content.Load<Texture2D>("shield"), Content.Load<Texture2D>("boot"), Content.Load<Texture2D>("coin"), Content.Load<Texture2D>("time"), Content.Load<SoundEffect>("heroAtack"), Content.Load<SoundEffect>("heroDeath"), Content.Load<SoundEffect>("payment"));
            //if (File.Exists("content/winners.txt"))
            //{
            //    if(System.IO.File.ReadAllLines("content/winners.txt").Length > 0)
            //        indexOfPlayer = File.ReadAllLines("content/winners.txt").Length;
            //}
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            gameSet = Menu(gameSet, pgSet);
            if ((player.position.X < 0) && (gameSet == GameSet.Game))
            {
                gameSet = GameSet.GameBase;
                player.position.X = 1350;
            }
            if ((player.position.X > 1366) && (gameSet == GameSet.GameBase))
            {
                gameSet = GameSet.Game;
                player.position.X = 0;
            }
            switch (gameSet)
            {
                case GameSet.End:
                    ++saluteFrame.X;
                    if (saluteFrame.X >= saluteSize.X)
                    {
                        saluteFrame.X = 0;
                        ++saluteFrame.Y;
                        if (saluteFrame.Y >= saluteSize.Y)
                        {
                            saluteFrame.Y = 0;
                            salutePosition = new Vector2(new Random().Next(1366), new Random().Next(768));
                        }
                    }
                    break;

                case GameSet.Game:
                    player.timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    player.Spawn(rocks, buildings, gameSet);

                    for (int i = 0; i < enemies.Count; i++)
                    {
                        player.Atack(player, enemies[i]);
                        enemies[i].Spawn(player, enemies[i]);
                    }
                    break;
                case GameSet.Menu:
                    MouseState state = Mouse.GetState();
                    cursor_coords.X = state.X;
                    cursor_coords.Y = state.Y;
                    break;
                case GameSet.CreateHero:
                    player.Idle();
                    Textbox();
                    break;
                case GameSet.GameBase:
                    player.timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    player.Spawn(rocks, buildings, gameSet);
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (gameSet)
            {
                case GameSet.Game:
                    spriteBatch.Draw(grass, new Vector2(0, 0), Color.Wheat);
                    if (Keyboard.GetState().IsKeyDown(Keys.F1))
                    {
                        spriteBatch.DrawString(congra, "Вы можете использовать стрелки для передви-", new Vector2(0, 660), Color.White);
                        spriteBatch.DrawString(congra, "жения и пробел, чтобы атаковать", new Vector2(0, 700), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(congra, "F1 - помощь", new Vector2(0, 700), Color.White);
                    }
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].Draw(spriteBatch, player, enemies[i]))
                            enemies.RemoveAt(i);
                    }
                    player.Draw(spriteBatch, player, congra);
                    for (int i = 0; i < rocks.Count; i++)
                        spriteBatch.Draw(rocks[i].texture, rocks[i].position, Color.White);
                    if (player.gameOver)
                    {
                        spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            gameSet = GameSet.Menu;
                            player.gameOver = false;
                        }
                    }

                    //spriteBatch.DrawString(congra, timer.ToString("0.00"), new Vector2(456, 16), Color.RoyalBlue);
                    pgSet = GameSet.Game;
                    break;
                case GameSet.Menu:
                    spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(button, new Vector2(325, 10), Color.White);
                    spriteBatch.Draw(button2, new Vector2(325, 170), Color.White);
                    spriteBatch.Draw(button3, new Vector2(325, 650), Color.White);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        spriteBatch.Draw(cursor_tap, cursor_coords, Color.White);
                    else
                        spriteBatch.Draw(cursor, cursor_coords, Color.White);
                    if ((cursor_coords.X > 325) & (cursor_coords.X < 1025) & (cursor_coords.Y > 10) & (cursor_coords.Y < 80) & (Mouse.GetState().LeftButton == ButtonState.Pressed))
                    {
                        gameSet = GameSet.CreateHero;
                        //for (int i = 0; i < enemies.Count; i++)
                        //    enemies[i].health = 0;
                        //for (int j = 0; j < buildings.Count; j++)
                        //    buildings[j].placed = false;
                        NewGame();
                        spriteBatch.Draw(cursor_tap, cursor_coords, Color.White);

                    }
                    if ((cursor_coords.X > 325) & (cursor_coords.X < 1025) & (cursor_coords.Y > 170) & (cursor_coords.Y < 240) & (Mouse.GetState().LeftButton == ButtonState.Pressed))
                    {
                        if (pgSet == GameSet.Game)
                            gameSet = GameSet.Game;
                        if (pgSet == GameSet.GameBase)
                            gameSet = GameSet.GameBase;
                        spriteBatch.Draw(cursor_tap, cursor_coords, Color.White);
                    }
                    if ((cursor_coords.X > 325) & (cursor_coords.X < 1025) & (cursor_coords.Y > 650) & (cursor_coords.Y < 729) & (Mouse.GetState().LeftButton == ButtonState.Pressed))
                    {
                        Exit();
                        spriteBatch.Draw(cursor_tap, cursor_coords, Color.White);
                    }
                    break;
                case GameSet.CreateHero:
                    spriteBatch.Draw(createground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(player.texture, new Vector2(375, 200), new Rectangle(player.idleFrame.X * player.frameWidth, player.idleFrame.Y * player.frameHeight, player.frameWidth, player.frameHeight), player.color, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
                    spriteBatch.DrawString(congra, player.name, textboxPosition, Color.White);
                    break;
                case GameSet.GameBase:
                    spriteBatch.Draw(grass, new Vector2(0, 0), Color.GreenYellow);
                    player.health = 100;
                    //for (int i = 0; i < enemies.Count; i++)
                    //    enemies[i].health = 0;
                    enemies.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (Layer[i, j] == 1)
                            {
                                enemies.Add(new Enemy(Content.Load<Texture2D>("enemySprite"), new Vector2(j * 136, i * 96), 64, 64, new Point(0, 1), new Point(5, 2), new Point(0, 0), new Point(4, 1), new Point(0, 2), new Point(6, 3), new Point(0, 3), new Point(6, 4), 50, 6, 5, Content.Load<SoundEffect>("enemyAtack"), Content.Load<SoundEffect>("enemyDeath")));
                            }
                        }
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.F1))
                    {
                        spriteBatch.DrawString(congra, " Нажмите К, чтобы построить Замок", new Vector2(0, 260), Color.White);

                        spriteBatch.DrawString(congra, " (завершает обучающий уровень)", new Vector2(0, 300), Color.White);
                        spriteBatch.DrawString(congra, " Нажмите W, чтобы построить Казарму", new Vector2(0, 340), Color.White);

                        spriteBatch.DrawString(congra, " (прибавляет 10 к атаке)", new Vector2(0, 380), Color.White);
                        spriteBatch.DrawString(congra, " Нажмите М, чтобы построить Банк", new Vector2(0, 420), Color.White);

                        spriteBatch.DrawString(congra, "(увеличивает награду за убийство врага на величину до 10 монет)", new Vector2(0, 460), Color.White);
                        spriteBatch.DrawString(congra, " Нажмите R, чтобы построить Стадион", new Vector2(0, 500), Color.White);

                        spriteBatch.DrawString(congra, " (увеличивает скорость персонажа на 10)", new Vector2(0, 540), Color.White);
                        spriteBatch.DrawString(congra, " Нажмите D, чтобы построить стену", new Vector2(0, 580), Color.White);

                        spriteBatch.DrawString(congra, " (прибавляет 5 к защите)", new Vector2(0, 620), Color.White);
                        spriteBatch.DrawString(congra, " Стоимость каждого здания - 10 золотых монет", new Vector2(0, 660), Color.White);

                        spriteBatch.DrawString(congra, " (убивайте монстров, чтобы остроить свое королевство)", new Vector2(0, 700), Color.White);

                    }
                    else
                    {
                        spriteBatch.DrawString(congra, "Press F1 to view help", new Vector2(0, 700), Color.White);
                    }
                    for (int i = 0; i < buildings.Count; i++)
                    {
                        if (Keyboard.GetState().IsKeyDown(buildings[i].key) && (!presed) && (player.gold >= buildings[i].cost) && (!buildings[i].placed))
                        {
                            player.gold -= buildings[i].cost;
                            buildings[i].built.Play();
                            buildings[i].Function(player, winners);
                            buildings[i].placed = true;
                            presed = true;
                        }
                        if (buildings[i].placed)
                            spriteBatch.Draw(buildings[i].texture, buildings[i].position, Color.White);
                        if (buildings[i].congra)
                        {
                            MediaPlayer.Play(endMusic);
                            gameSet = GameSet.End;
                            buildings[i].congra = false;
                        }
                        if (Keyboard.GetState().IsKeyUp(buildings[i].key))
                            presed = false;
                    }
                    player.Draw(spriteBatch, player, congra);

                    //spriteBatch.DrawString(congra, timer.ToString("0.00"), new Vector2(456, 16), Color.RoyalBlue);
                    pgSet = GameSet.GameBase;
                    break;
                case GameSet.End:
                    spriteBatch.Draw(saluteBackground, new Vector2(0, 0), Color.Black);
                    spriteBatch.DrawString(congra, "Обучающий уровень завершен!", new Vector2(400, 50), Color.HotPink);
                    if (File.ReadAllLines("content/winners.txt").Count() <= 7)
                    {
                        for (int i = 0; i < File.ReadAllLines("content/winners.txt").Count(); i++)
                            spriteBatch.DrawString(congra, File.ReadAllLines("content/winners.txt")[i], new Vector2(400, 100 * (i + 1)), Color.HotPink);
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                            spriteBatch.DrawString(congra, File.ReadAllLines("content/winners.txt")[i], new Vector2(400, 100 * (i + 1)), Color.HotPink);
                        spriteBatch.DrawString(congra, ".......................................................................", new Vector2(400, 600), Color.HotPink);
                        for (int i = 0; i < buildings.Count; i++)
                            if (buildings[i].playerWinnerCount >= 7)
                                spriteBatch.DrawString(congra, File.ReadAllLines("content/winners.txt")[buildings[i].playerWinnerCount], new Vector2(400, 700), Color.HotPink);
                            else if (buildings[i].playerWinnerCount != 0)
                                spriteBatch.DrawString(congra, File.ReadAllLines("content/winners.txt")[File.ReadAllLines("content/winners.txt").Count() - 1], new Vector2(400, 700), Color.HotPink);
                    }
                    spriteBatch.Draw(salute, salutePosition, new Rectangle(saluteFrame.X * saluteFrameWidth, saluteFrame.Y * saluteFrameHeight, saluteFrameWidth, saluteFrameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        MediaPlayer.Play(backMusic);
                        gameSet = GameSet.GameBase;
                    }

                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        GameSet Menu(GameSet gameSet, GameSet pgSet)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && (!presed))
            {
                presed = true;
                if (gameSet == GameSet.Game)
                {
                    gameSet = GameSet.Menu;
                }
                else if (gameSet == GameSet.GameBase)
                {
                    gameSet = GameSet.Menu;
                }
                else if (gameSet == GameSet.Menu)
                {
                    if (pgSet == GameSet.Game)
                        gameSet = GameSet.Game;
                    if (pgSet == GameSet.GameBase)
                        gameSet = GameSet.GameBase;
                }


            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
                presed = false;
            return gameSet;
        }

        public void NewGame()
        {
            buildings.Clear();
            enemies.Clear();
            rocks.Clear();
            player.timer2 = 0;
            int a = 0, b = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Layer[i, j] == 1)
                    {
                        enemies.Add(new Enemy(Content.Load<Texture2D>("enemySprite"), new Vector2(j * 136, i * 96), 64, 64, new Point(0, 1), new Point(5, 2), new Point(0, 0), new Point(4, 1), new Point(0, 2), new Point(6, 3), new Point(0, 3), new Point(6, 4), 50, 6, 5, Content.Load<SoundEffect>("enemyAtack"), Content.Load<SoundEffect>("enemyDeath")));
                    }

                    if (Layer[i, j] == 2)
                    {
                        a = j;
                        b = i;
                    }

                    if (Layer[i, j] == 3)
                        rocks.Add(new Obstacle(Content.Load<Texture2D>("tree"), new Vector2(j * 136, i * 96), 35, 75, 75));

                    if (Layer[i, j] == 4)
                        rocks.Add(new Obstacle(Content.Load<Texture2D>("rock"), new Vector2(j * 136, i * 96), 34, 14, 30));
                    if (LayerBase[i, j] == 1)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("Kingdoom"), new Vector2(j * 136, i * 96 - 20), 150, 150, 20, 10, Keys.K, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 2)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("warhouse"), new Vector2(j * 136 + 40, i * 96 - 20), 200, 150, 50, 10, Keys.W, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 3)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("market"), new Vector2(j * 136, i * 96), 200, 150, 50, 10, Keys.M, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 4)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("rally"), new Vector2(j * 136, i * 96), 150, 170, 15, 10, Keys.R, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                    if (LayerBase[i, j] == 5)
                    {
                        buildings.Add(new Building(Content.Load<Texture2D>("wall"), new Vector2(j * 136, i * 96), 400, 70, 20, 10, Keys.D, Content.Load<SoundEffect>("buildingPlaced")));
                    }
                }
            }
            player = new Player(Content.Load<Texture2D>("sprite"), new Vector2(a * 136, b * 96), 64, 64, new Point(0, 1), new Point(8, 2), new Point(0, 0), new Point(4, 1), new Point(0, 9), new Point(10, 10), new Point(0, 4), new Point(9, 5), 100, 5, 5, Content.Load<Texture2D>("heart"), Content.Load<Texture2D>("sword"), Content.Load<Texture2D>("shield"), Content.Load<Texture2D>("boot"), Content.Load<Texture2D>("coin"), Content.Load<Texture2D>("time"), Content.Load<SoundEffect>("heroAtack"), Content.Load<SoundEffect>("heroDeath"), Content.Load<SoundEffect>("payment"));
            //indexOfPlayer++;
        }

        void Textbox()
        {
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            Keys[] pressedKeys;
            pressedKeys = currentKeyboardState.GetPressedKeys();
            string toStringKeys;

            foreach (Keys key in pressedKeys)
            {
                if (oldKeyboardState.IsKeyUp(key))
                {
                    if (key == Keys.Back && player.name != "") // overflows 
                    {
                        player.name = player.name.Remove(player.name.Length - 1, 1);
                        textboxPosition.X += 15;
                    }
                    else
                    if (key == Keys.Space)
                        player.name = player.name.Insert(player.name.Length, " ");
                    else if (key.GetHashCode() >= 65 && key.GetHashCode() <= 90 || key == Keys.OemOpenBrackets || key == Keys.OemCloseBrackets || key == Keys.OemQuotes || key == Keys.OemSemicolon || key == Keys.OemComma || key == Keys.OemPeriod)
                    {
                        switch (key.ToString())
                        {
                            case "Q": toStringKeys = "Й"; break;
                            case "W": toStringKeys = "Ц"; break;
                            case "E": toStringKeys = "У"; break;
                            case "R": toStringKeys = "К"; break;
                            case "T": toStringKeys = "Е"; break;
                            case "Y": toStringKeys = "Н"; break;
                            case "U": toStringKeys = "Г"; break;
                            case "I": toStringKeys = "Ш"; break;
                            case "O": toStringKeys = "Щ"; break;
                            case "P": toStringKeys = "З"; break;
                            case "OemOpenBrackets": toStringKeys = "Х"; break;
                            case "OemCloseBrackets": toStringKeys = "Ъ"; break;
                            case "A": toStringKeys = "Ф"; break;
                            case "S": toStringKeys = "Ы"; break;
                            case "D": toStringKeys = "В"; break;
                            case "F": toStringKeys = "А"; break;
                            case "G": toStringKeys = "П"; break;
                            case "H": toStringKeys = "Р"; break;
                            case "J": toStringKeys = "О"; break;
                            case "K": toStringKeys = "Л"; break;
                            case "L": toStringKeys = "Д"; break;
                            case "OemSemicolon": toStringKeys = "Ж"; break;
                            case "OemQuotes": toStringKeys = "Э"; break;
                            case "Z": toStringKeys = "Я"; break;
                            case "X": toStringKeys = "Ч"; break;
                            case "C": toStringKeys = "С"; break;
                            case "V": toStringKeys = "М"; break;
                            case "B": toStringKeys = "И"; break;
                            case "N": toStringKeys = "Т"; break;
                            case "M": toStringKeys = "Ь"; break;
                            case "OemComma": toStringKeys = "Б"; break;
                            case "OemPeriod": toStringKeys = "Ю"; break;
                            default: toStringKeys = Convert.ToString((Keys)key); break;
                        }
                        player.name += toStringKeys;
                        textboxPosition.X -= 15;
                    }



                    else if (key == Keys.Right)
                    {
                        if (colorPreset <= 7)
                        {
                            colorPreset++;
                            if (colorPreset == 1)
                                player.color = new Color(Color.Red, 1.0f);
                            if (colorPreset == 2)
                                player.color = new Color(Color.Green, 1.0f);
                            if (colorPreset == 3)
                                player.color = new Color(Color.Blue, 1.0f);
                            if (colorPreset == 4)
                                player.color = new Color(Color.Gold, 1.0f);
                            if (colorPreset == 5)
                                player.color = new Color(Color.Silver, 1.0f);
                            if (colorPreset == 6)
                                player.color = new Color(Color.OrangeRed, 1.0f);
                            if (colorPreset == 7)
                                player.color = new Color(Color.White, 1.0f);
                        }
                    }
                    else if (key == Keys.Left)
                    {
                        if (colorPreset > 0)
                        {
                            colorPreset--;
                            if (colorPreset == 1)
                                player.color = new Color(Color.Red, 1.0f);
                            if (colorPreset == 2)
                                player.color = new Color(Color.Green, 1.0f);
                            if (colorPreset == 3)
                                player.color = new Color(Color.Blue, 1.0f);
                            if (colorPreset == 4)
                                player.color = new Color(Color.Gold, 1.0f);
                            if (colorPreset == 5)
                                player.color = new Color(Color.Silver, 1.0f);
                            if (colorPreset == 6)
                                player.color = new Color(Color.OrangeRed, 1.0f);
                            if (colorPreset == 7)
                                player.color = new Color(Color.White, 1.0f);
                        }
                    }
                    else if (key == Keys.Enter)
                        gameSet = GameSet.Game;
                    //MediaPlayer.Play(backMusic);
                }
            }
        }

    }
}
