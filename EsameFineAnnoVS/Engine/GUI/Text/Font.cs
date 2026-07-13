using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    class Font
    {
        protected int numCol; //Num of colums the font has
        protected int firstValue; //First value of the Font

        public string TextureName { get; protected set; } //Name of the texture of the Font
        public Texture Texture { get; protected set; } //Texture of the Font
        public int CharacterWidth { get; protected set; } //Width of the characters in the Font
        public int CharacterHeight { get; protected set; }//height of the characters in the Font

        public Font(string textureName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            TextureName = textureName;
            Texture = GfxMngr.AddTexture(TextureName, texturePath);
            firstValue = firstCharacterASCIIvalue;
            CharacterWidth = charWidth;
            CharacterHeight = charHeight;
            numCol = numColumns;
        }

        public virtual Vector2 GetOffset(char c)
        {
            int cVal = c;
            int delta = cVal - firstValue;

            int x = delta % numCol;
            int y = delta / numCol;

            return new Vector2(x * CharacterWidth, y * CharacterHeight);
        }

    }
}
