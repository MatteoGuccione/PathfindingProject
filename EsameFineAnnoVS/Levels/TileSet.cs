using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    struct TileOffset
    {
        public int Y;
        public int X;

        public TileOffset(int x, int y)
        {
            Y = y;
            X = x;
        }
    }
    class TileSet
    {
        private TileOffset[] tiles; //Array of Tiles

        public string TextureName; //Name of the texture of the tilesex
        public int TileWidth { get; set; } //Width of the tileset
        public int TileHeight { get; set; } //Height of the tileset

        private int cols; //Columns of the tileset
        private int rows; //Rows of the tileset

        public TileSet(string textName, int cols, int rows, int tileW, int tileH)
        {
            tiles = new TileOffset[cols * rows];
            TextureName = textName;
            TileWidth = tileW;
            TileHeight = tileH;
            this.cols = cols;
            this.rows = rows;

            int xOff = 0;
            int yOff = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tiles[i * cols + j] = new TileOffset(xOff, yOff);

                    xOff += TileWidth;
                }
                xOff = 0;
                yOff += TileHeight;
            }
        }

        public TileOffset GetAtIndex(int index)
        {
                return tiles[index - 1];
        }
    }
}
