using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    internal class Princess : Actor
    {


        public Princess(string textureName = "PrincessIdleD") : base(textureName)
        {

        }
        public override void SetSpriteDirection(Vector2 direction)//Set the direction the movement of the character and set the sprite to match the direction
        {
            Vector2 currentPos = Position;
            CheckMovement(direction);
            sprite.pivot = new Vector2(0.5f, 0.5f);
            sprite.position = currentPos + sprite.pivot;
        }
        public override void CheckMovement(Vector2 direction)//Update the animation of the character using the direction of the movement
                                                             //(if the direction was not the same)
        {
            bool same = false;
            if (lastdirection != Vector2.Zero)
            {
                if (lastdirection == direction)
                {
                    same = true;
                }
            }
            if (direction != Vector2.Zero && same == false)
            {
                texOff = Vector2.Zero;
                if (direction.Y == 1)
                {
                    texture = GfxMngr.GetTexture("PrincessWalkD-NBG");
                    sprite = new Sprite(texture.Width, texture.Height / 4);
                }
                else if (direction.Y == -1)
                {
                    texture = GfxMngr.GetTexture("PrincessWalkU-NBG");
                    sprite = new Sprite(texture.Width, texture.Height / 4);
                }
                else if (direction.X == 1)
                {
                    texture = GfxMngr.GetTexture("PrincessWalkR-NBG");
                    sprite = new Sprite(texture.Width, texture.Height / 4);
                }
                else
                {
                    texture = GfxMngr.GetTexture("PrincessWalkR-NBG");
                    sprite = new Sprite(texture.Width, texture.Height / 4);
                    sprite.FlipX = true;
                }
                sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
                anim = new Animation((int)sprite.Width, (int)sprite.Height, 20, 4, true);
                lastdirection = direction;
            }
            else if (same == false)
            {

            }
            if (anim != null)
            {
                anim.Update(Game.DeltaTime, ref texOff);
            }
        }
    }
}
