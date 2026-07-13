using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace EsameFineAnnoVS
{
    internal class Object : GameObject //Class used only to make object which are supposed to just stay in place appear on the map
    {
        public Object(string textureName) : base(textureName)
        {
            IsActive = true;
            Forward = new Vector2(1f, 0f);
            sprite.pivot = new Vector2(0.5f, 0.5f);
            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
        }
    }
}
