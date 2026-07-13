using OpenTK;
using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    internal class DialogueBox : I_Drawable
    {
        public DrawLayer Layer { get; protected set; }
        Vector4 blackColor;
        Sprite box;
        public bool isActive;
        public DialogueBox(Vector2 startPosition, Vector2 endPosition)
        {
            blackColor = new Vector4(0f, 0f, 0f, 1f);
            int width = (int)(endPosition.X - startPosition.X);
            int height = (int)(endPosition.Y - startPosition.Y);
            Layer = DrawLayer.Middleground;
            box = new Sprite(width, height);
            box.position = startPosition;
            DrawMngr.AddItem(this);
        }

        public virtual void DestroyBox()
        {
            box = null;
            DrawMngr.RemoveItem(this);
        }

        public void Draw()
        {
            if (isActive)
            {
                box.Draw();
            }
        }
    }
}
