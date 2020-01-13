using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatKingdoomRefactored
{
    public class Player : Creature
    {
        public Texture2D heart, sword, shield, coin, boot, time;
        public int gold = 0;
        public int goldForHead = 5;
        public int defend = 5;
        public bool gameOver = false;
        public string name = "UNNAMED";
        public Color color = new Color(Color.White, 1.0f);
        public float timer2 = 0;
        public int timer3 = 0;
        public Player(Texture2D texture,
            Vector2 position, int frameWidth,
            int frameHeight, Point moveFrame,
            Point moveSize, Point idleFrame,
            Point idleSize, Point atackFrame,
            Point atackSize, Point dieFrame,
            Point dieSize, int health,
            int damage, int speed, Texture2D heart,
            Texture2D sword, Texture2D shield,
            Texture2D boot, Texture2D coin, Texture2D time,
            SoundEffect atack, SoundEffect death,
            SoundEffect payment)
            : base(texture,
            position, frameWidth,
            frameHeight, moveFrame,
            moveSize, idleFrame,
            idleSize, atackFrame,
            atackSize, dieFrame,
            dieSize, health,
            damage, speed)
        {
            this.heart = heart;
            this.sword = sword;
            this.shield = shield;
            this.coin = coin;
            this.boot = boot;
            this.time = time;
            this.atack = atack;
            this.death = death;
            this.payment = payment;
        }

        public void Move(List<Obstacle> rock, List<Building> building, GameSet gameSet)
        {
            moveFrame = Animate(moveFrame, new Point(0, 1), moveSize);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y += speed;
            for (int i = 0; i < rock.Count; i++)
                for (int j = 0; j < building.Count; j++)
                    if (((IsCollide(this, rock[i])) && (gameSet == GameSet.Game)) || ((IsCollide(this, building[j])) && (gameSet == GameSet.GameBase) && (building[j].placed)) || (gameSet == GameSet.GameBase && position.Y >= 700))
                        position.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y -= speed;
            for (int i = 0; i < rock.Count; i++)
                for (int j = 0; j < building.Count; j++)
                    if (((IsCollide(this, rock[i])) && (gameSet == GameSet.Game)) || ((IsCollide(this, building[j])) && (gameSet == GameSet.GameBase) && (building[j].placed)) || (gameSet == GameSet.GameBase && position.Y <= -5))
                        position.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += speed;
            for (int i = 0; i < rock.Count; i++)
                for (int j = 0; j < building.Count; j++)
                    if (((IsCollide(this, rock[i])) && (gameSet == GameSet.Game)) || ((IsCollide(this, building[j])) && (gameSet == GameSet.GameBase) && (building[j].placed)))
                        position.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= speed;
            for (int i = 0; i < rock.Count; i++)
                for (int j = 0; j < building.Count; j++)
                    if (((IsCollide(this, rock[i])) && (gameSet == GameSet.Game)) || ((IsCollide(this, building[j])) && (gameSet == GameSet.GameBase) && (building[j].placed)) || (gameSet == GameSet.GameBase && position.X <= -5))
                        position.X += speed;
        }

        public void Atack(Player player, Enemy enemy)
        {
            if (IsCollide(player, enemy) && Keyboard.GetState().IsKeyDown(Keys.Space) && (player.health > 0) && (enemy.health > 0))
            {
                atackFrame = Animate(atackFrame, new Point(0, 9), atackSize);
                timer++;
                if (timer >= 10)
                {
                    enemy.health -= player.damage;
                    atack.Play();
                    timer = 0;
                }
                if (enemy.health <= 0)
                {
                    player.gold += new System.Random().Next(1, goldForHead);
                    payment.Play();
                }
            }

        }

        public void Die()
        {
            dieFrame = Animate(dieFrame, new Point(0, 4), dieSize);
            if (health <= 0)
            {
                timer3++;
                if (timer3 >= 30)
                {
                    death.Play();
                    timer3 = 0;
                }
            }
        }

        public void Idle()
        {
            idleFrame = Animate(idleFrame, new Point(0, 0), idleSize);
        }

        public void Spawn(List<Obstacle> rock, List<Building> building, GameSet gameSet)
        {
            Move(rock, building, gameSet);
            Idle();
            Die();
        }

        public void Draw(SpriteBatch spriteBatch, Player player, SpriteFont congra)
        {
            spriteBatch.Draw(heart, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(congra, player.health.ToString(), new Vector2(64, 16), Color.Red);
            spriteBatch.Draw(sword, new Vector2(0, 64), Color.White);
            spriteBatch.DrawString(congra, player.damage.ToString(), new Vector2(64, 80), Color.Gray);
            spriteBatch.Draw(shield, new Vector2(144, 0), Color.White);
            spriteBatch.DrawString(congra, player.defend.ToString(), new Vector2(208, 16), Color.LightGray);
            spriteBatch.Draw(coin, new Vector2(144, 64), Color.White);
            spriteBatch.DrawString(congra, player.gold.ToString(), new Vector2(208, 80), Color.Yellow);
            spriteBatch.Draw(boot, new Vector2(292, 0), Color.White);
            spriteBatch.DrawString(congra, player.speed.ToString(), new Vector2(356, 16), Color.Orange);
            spriteBatch.Draw(time, new Vector2(292, 64), Color.White);
            spriteBatch.DrawString(congra, timer2.ToString("0.00"), new Vector2(356, 80), Color.Orange);
            if (player.health <= 0)
            {
                spriteBatch.Draw(player.texture, player.position, new Rectangle(player.dieFrame.X * player.frameWidth, player.dieFrame.Y * player.frameHeight, player.frameWidth, player.frameHeight), color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                if (player.dieFrame == new Point(0, 4))
                {
                    gameOver = true;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
                spriteBatch.Draw(player.texture, player.position, new Rectangle(player.atackFrame.X * player.frameWidth, player.atackFrame.Y * player.frameHeight, player.frameWidth, player.frameHeight), color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left))
                spriteBatch.Draw(player.texture, player.position, new Rectangle(player.moveFrame.X * player.frameWidth, player.moveFrame.Y * player.frameHeight, player.frameWidth, player.frameHeight), color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(player.texture, player.position, new Rectangle(player.idleFrame.X * player.frameWidth, player.idleFrame.Y * player.frameHeight, player.frameWidth, player.frameHeight), color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

        }
    }
}
