using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CatKingdoomRefactored
{
    public class Enemy : Creature
    {
        public Vector2 radiusOfAgressive = Vector2.Zero, spawnPosition;
        public Enemy(Texture2D texture,
            Vector2 position, int frameWidth,
            int frameHeight, Point moveFrame,
            Point moveSize, Point idleFrame,
            Point idleSize, Point atackFrame,
            Point atackSize, Point dieFrame,
            Point dieSize, int health,
            int damage, int speed,
            SoundEffect atack,
            SoundEffect death) : base(texture,
            position, frameWidth,
            frameHeight, moveFrame,
            moveSize, idleFrame,
            idleSize, atackFrame,
            atackSize, dieFrame,
            dieSize, health,
            damage, speed)
        {
            this.atack = atack;
            this.death = death;
        }

        public void Move(Player player)
        {
            radiusOfAgressive = player.position - position;
            if (radiusOfAgressive.Length() < 100 && radiusOfAgressive.Length() > 25 && player.health > 0)
            {
                radiusOfAgressive.Normalize();
                radiusOfAgressive *= 5;
                position += radiusOfAgressive;
                moveFrame = Animate(moveFrame, new Point(0, 1), moveSize);
            }

        }

        void Atack(Player player, Enemy enemy)
        {
            atackFrame = Animate(atackFrame, new Point(0, 2), atackSize);
            if ((IsCollide(player, enemy)) && (player.health > 0))
            {
                timer++;
                if (timer >= 15)
                {
                    player.health = player.health + ((player.defend * enemy.damage) / 100) - enemy.damage;
                    atack.Play();
                    timer = 0;
                }
            }
        }

        public void Die()
        {
            dieFrame = Animate(dieFrame, new Point(0, 3), dieSize);
            if (health < 0)
                death.Play();
        }

        public void Idle()
        {
            idleFrame = Animate(idleFrame, new Point(0, 0), idleSize);
        }

        public void Spawn(Player player, Enemy enemy)
        {
            Move(player);
            Idle();
            Die();
            Atack(player, enemy);
        }

        public bool Draw(SpriteBatch spriteBatch, Player player, Enemy enemy)
        {
            if (enemy.health <= 0)
            {
                spriteBatch.Draw(enemy.texture, enemy.position, new Rectangle(enemy.dieFrame.X * enemy.frameWidth, enemy.dieFrame.Y * enemy.frameHeight, enemy.frameWidth, enemy.frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                return true;
            }
            else if (IsCollide(player, enemy) && player.health > 0)
                spriteBatch.Draw(enemy.texture, enemy.position, new Rectangle(enemy.atackFrame.X * enemy.frameWidth, enemy.atackFrame.Y * enemy.frameHeight, enemy.frameWidth, enemy.frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            else if (enemy.radiusOfAgressive.Length() < 100 && enemy.radiusOfAgressive.Length() > 25 && player.health > 0)
                spriteBatch.Draw(enemy.texture, enemy.position, new Rectangle(enemy.moveFrame.X * enemy.frameWidth, enemy.moveFrame.Y * enemy.frameHeight, enemy.frameWidth, enemy.frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            else
                spriteBatch.Draw(enemy.texture, enemy.position, new Rectangle(idleFrame.X * enemy.frameWidth, enemy.idleFrame.Y * enemy.frameHeight, enemy.frameWidth, enemy.frameHeight), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            return false;
        }
    }
}
