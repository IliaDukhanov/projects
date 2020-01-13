using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace CatKingdoomRefactored
{
    public class Building : Object
    {
        public bool placed = false;
        public int cost;
        public Keys key;
        public bool congra = false;
        public int playerWinnerCount = 0;
        public SoundEffect built;
        public Building(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int shape, int cost, Keys key, SoundEffect built)
        {
            this.texture = texture;
            this.position = position;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.shape = shape;
            this.cost = cost;
            this.key = key;
            this.built = built;
        }

        public void Function(Player player, List<Winners> winners)
        {
            if (key == Keys.K)
            {
                congra = true;
                if (File.Exists("content/winners.txt"))
                {
                    using (StreamReader reader = new StreamReader("content/winners.txt"))
                    {
                        string textline = reader.ReadLine();
                        while (textline != null)
                        {
                            textline = textline.Remove(0, 1);
                            if (textline.IndexOf(" ") != -1)
                            {
                                winners.Add(new Winners(textline.Split(new char[] { ' ' })[1], float.Parse(textline.Split(new char[] { ' ' })[2])));
                            }
                            textline = reader.ReadLine();
                        }
                    }

                }
                File.Delete("content/winners.txt");
                winners.Add(new Winners(player.name, player.timer2));
                winners.Sort(new CompareScores());
                for (int i = 0; i < winners.Count; i++)
                {
                    if ((winners[i].name == player.name) && (winners[i].score == player.timer2))
                        playerWinnerCount = i;
                }
                using (StreamWriter sw = new StreamWriter(new FileStream("content/winners.txt", FileMode.OpenOrCreate, FileAccess.Write)))
                {
                    for (int i = 1; i <= winners.Count; i++)
                    {
                        (sw.BaseStream).Seek(0, SeekOrigin.End);
                        sw.WriteLine(i + " " + winners[i - 1].name + " " + winners[i - 1].score);
                    }
                }
                winners.Clear();
            }
            if (key == Keys.W)
            {
                player.damage += 5;
            }
            if (key == Keys.M)
            {
                player.goldForHead += 10;
            }
            if (key == Keys.R)
            {
                player.speed += 5;
            }
            if (key == Keys.D)
            {
                player.defend += 5;
            }
        }

    }
}

