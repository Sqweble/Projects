using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SquareGenerator
{
    public class Generator
    {
        
        Random random = new Random();
        public struct Square
        {
            public float width;
            public float height;
            public Vector2 position;
        }
        Square[] squares = new Square[160];




        public void InitializeSquares(GraphicsDeviceManager graphics)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                int randomSizeX = random.Next(1, 4);
                int randomSizeY = random.Next(1, 4);



                squares[i].height = randomSizeY * 16;
                squares[i].width = randomSizeX * 16;

                if (i == 0)
                {
                    squares[i].position = new Vector2(0, 0);
                }
                else
                {
                    Square lastSquare = squares[i - 1];
                    squares[i].position = lastSquare.position + new Vector2(lastSquare.width, 0);
                }
            }
        }
        public void DrawLine(SpriteBatch spriteBatch,Texture2D texture,Vector2 P, Vector2 Q)
        {
            Vector2 line;

            if (P.X == Q.X)
            {
                line.X = P.X;
                line.Y = 0;

                if (P.Y > Q.Y)
                {
                    line.Y = P.Y - Q.Y;
                    for (int i =0; i < line.Y; i++)
                    {
                        spriteBatch.Draw(texture, new Vector2(line.X, Q.Y + line.Y - i), Color.Black);
                    }
                }

                if (P.Y < Q.Y)
                {
                    line.Y = Q.Y - P.Y;
                    for (int i = 0; i < line.Y; i++)
                    {
                        spriteBatch.Draw(texture, new Vector2(line.X, P.Y + line.Y - i), Color.Black);
                    }
                }
            }

            else if (P.Y == Q.Y)
            {
                line.Y = P.Y;
                line.X = 0;

                if (P.X > Q.X)
                {
                    line.X = P.X - Q.X;
                    for (int i = 0; i < line.X+1; i++)
                    {
                        spriteBatch.Draw(texture, new Vector2(Q.X + line.X - i, line.Y), Color.Black);
                    }
                }

                if (P.X < Q.X)
                {
                    line.X = Q.X - P.X;
                    for (int i = 0; i < line.X - 1; i++)
                    {
                        spriteBatch.Draw(texture, new Vector2(P.X + line.X - i, line.Y), Color.Black);
                    }
                }


            }
        }

        public void DrawSquare(SpriteBatch spriteBatch,Texture2D texture,Vector2 position, float height, float width)
        {
            DrawLine(spriteBatch, texture,position, position + new Vector2(width, 0));
            DrawLine(spriteBatch, texture,position + new Vector2(width, 0), position + new Vector2(width, -height));
            DrawLine(spriteBatch, texture,position + new Vector2(width, -height), position + new Vector2(0, -height));
            DrawLine(spriteBatch, texture,position + new Vector2(0, -height), position);

        }

        public void DrawSquares(SpriteBatch spriteBatch,Texture2D texture)
        {
            for (int i = 0; i < squares.Length; i++)
            {
                DrawSquare(spriteBatch, texture, squares[i].position, squares[i].height, squares[i].width);
            }
        }
    }
}
