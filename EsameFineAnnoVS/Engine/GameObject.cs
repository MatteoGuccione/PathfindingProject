using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace EsameFineAnnoVS
{
    class GameObject : I_Updatable, I_Drawable
    {
        protected Sprite sprite; //Sprite used by the GameObject
        protected Texture texture; //Texture used by the GameObject
        public Vector2 Scale { get { return sprite.scale; } set { sprite.scale *= value; } }

        protected int textOffsetX, textOffsetY; //Used only by TextObjects;
        protected int frameW; //Width of the GameObject
        protected int frameH; //Height of the GameObject
        public bool IsActive; //See if the GameObject is still alive/active


        public int X { get { return (int)sprite.position.X; } set { sprite.position.X = value; } }
        public int Y { get { return (int)sprite.position.Y; } set { sprite.position.Y = value; } }
        public Vector2 Pivot { get { return sprite.pivot; } set { sprite.pivot = value; } }
        public virtual Vector2 Position { get { return new Vector2(X, Y); } set { sprite.position = value; } }

        public float HalfWidth { get; protected set; }
        public float HalfHeight { get; protected set; }

        public DrawLayer Layer { get; protected set; } //used to get and set (only this and subclasses) to see the Layer where the sprite should been drawn
        

        public Vector2 Forward
        {
            get
            {
                return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            }
            set
            {
                sprite.Rotation = (float)Math.Atan2(value.Y, value.X);
            }
        }

        public GameObject(string texturePath, DrawLayer layer = DrawLayer.Playground, int textOffsetX = 0, int textOffsetY = 0, float spriteWidth = 0, float spriteHeight = 0)
        {
            texture = GfxMngr.GetTexture(texturePath);
            float spriteW = spriteWidth > 0 ? spriteWidth : texture.Width; //The first option is used only for characters and not a Cell
            float spriteH = spriteHeight > 0 ? spriteHeight : texture.Height; //The first option is used only for characters and not a Cell
            sprite = new Sprite(spriteW, spriteH);

            Layer = layer;

            frameW = texture.Width;
            frameH = texture.Height;

            this.textOffsetX = textOffsetX;
            this.textOffsetY = textOffsetY;

            HalfWidth = sprite.Width * 0.5f;
            HalfHeight = sprite.Height * 0.5f;

            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
        }

        public virtual void Update()
        {
           
        }


        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, textOffsetX, textOffsetY, (int)sprite.Width, (int)sprite.Height);
            }
        }

        public virtual void Destroy()
        {
            IsActive = false;
            sprite = null;
            texture = null;
            UpdateMngr.RemoveItem(this);
            DrawMngr.RemoveItem(this);
        }
    }
}
