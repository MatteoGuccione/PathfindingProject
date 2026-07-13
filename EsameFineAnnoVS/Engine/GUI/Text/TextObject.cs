using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    class TextObject
    {
        protected List<TextChar> sprites;
        protected string text;
        protected bool isActive;
        protected Font font;
        protected float hSpace;
        protected Vector2 position;

        public bool IsActive //Used to see if the TextObject should be drawn or not
        {
            get { return isActive; }
            set { isActive = value; UpdateCharStatus(); }
        }

        public TextObject(Vector2 position, string textString = "", Font font = null, int horizontalSpacing = 0)
        {
            this.position = position;
            hSpace = horizontalSpacing;
            if (font == null)
            {
                font = FontMngr.GetFont();
            }

            this.font = font;

            sprites = new List<TextChar>();

            if (textString != "")
            {
                SetText(textString);
            }

        }

        protected void SetText(string newText) //Used to set every char to the text
        {
            if (newText != text)
            {
                text = newText;
                int numChars = text.Length;
                float charX = position.X;
                float charY = position.Y;

                for (int i = 0; i < numChars; i++)
                {
                    char c = text[i];

                    if (i >= sprites.Count)
                    {
                        TextChar tc = new TextChar(new Vector2(charX, charY), c, font);
                        tc.IsActive = isActive;
                        sprites.Add(tc);
                    }
                    else if(c != sprites[i].Character)
                    {
                        sprites[i].Character = c;
                    }

                    charX += sprites[i].HalfWidth * 2 + hSpace;
                }

                if(sprites.Count > numChars)
                {
                    int count = sprites.Count - numChars;
                    int startCut = numChars;

                    for (int i = startCut; i < sprites.Count; i++)
                    {
                        sprites[i].Destroy();
                    }

                    sprites.RemoveRange(startCut, count);
                }
            }
        }

        protected virtual void UpdateCharStatus()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].IsActive = isActive;
            }
        }
    }
}
