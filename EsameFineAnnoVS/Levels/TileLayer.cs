using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EsameFineAnnoVS
{
    internal class TileLayer
    {
        private Texture tilesetImage; //Image used as the source of the Tileset
        private Sprite[] layerSprites; //
        private TileSet tileSet; //Tileset used by the TileLayer
        public int  rows { get; private set; } //Number of rows of the TileLayer
        public int  cols { get; private set; } //Number of height of the TileLayer
        private int tileW, tileH; //Width and Height of a single Tile
        public string[] IDs { get; } //Id every single Tile in the TileLayer

        public TileLayer(XmlNode layerNode, TileSet tileset, int cols, int rows, int tileW, int tileH)
        {
            XmlNode dataNode = layerNode.SelectSingleNode("data"); //Select the corresponding node from the XmlNode
            string data = dataNode.InnerText; //Trasnform the data receive into a single string
            data = data.Replace("\n", "").Replace(" ", "").Replace("\r", ""); //Replaces the leftmost string with the rightmost

            string[] Ids = data.Split(','); //Every time that the program encounters a , he create a blank space
            IDs = Ids;

            this.cols = cols;
            this.rows = rows;
            this.tileW = tileW;
            this.tileH = tileH;
            this.tileSet = tileset;

            tilesetImage = GfxMngr.GetTexture(tileset.TextureName);
            layerSprites = new Sprite[cols * rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    layerSprites[i * cols + j] = new Sprite(tileW, tileH);
                    layerSprites[i * cols + j].position = new Vector2(j * tileW, i * tileH); //Set the position of everysingle sprite
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int tileID = int.Parse(IDs[i * cols + j]);
                    int xOff = tileSet.GetAtIndex(tileID).X;
                    int yOff = tileSet.GetAtIndex(tileID).Y;
                    layerSprites[i * cols + j].DrawTexture(tilesetImage, xOff, yOff, tileW, tileH);
                }
            }
        }

        public int[] AdjustLayer() //Used every time a map is loaded to set the right amount of cost for each tile
        {
            int[] cells = new int[rows * cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int tileID = int.Parse(IDs[i * cols + j]);
                    switch (tileID - 1)
                    {
                        case 30:
                            cells[i * cols + j] = 2; break;
                        case 36:
                            cells[i * cols + j] = 1; break;
                        case 55:
                            cells[i * cols + j] = 1; break;
                        case 56:
                            cells[i * cols + j] = 1; break;
                        case 61:
                            cells[i * cols + j] = 1; break;
                        case 78:
                            cells[i * cols + j] = 10; break;
                        case 181:
                            cells[i * cols + j] = 1; break;
                        case 215:
                            cells[i * cols + j] = 1; break;
                        case 216:
                            cells[i * cols + j] = 1; break;
                        case 246:
                            cells[i * cols + j] = 1; break;
                        case 247:
                            cells[i * cols + j] = 1; break;
                        case 248:
                            cells[i * cols + j] = 1; break;
                        case 249:
                            cells[i * cols + j] = 1; break;
                        case 250:
                            cells[i * cols + j] = 1; break;
                        case 251:
                            cells[i * cols + j] = 1; break;
                        case 252:
                            cells[i * cols + j] = 1; break;
                        case 262:
                            cells[i * cols + j] = 1; break;
                        case 263:
                            cells[i * cols + j] = 1; break;
                        case 264:
                            cells[i * cols + j] = 1; break;
                        case 265:
                            cells[i * cols + j] = 1; break;
                        case 266:
                            cells[i * cols + j] = 1; break;
                        case 288:
                            cells[i * cols + j] = 1; break;
                        case 290:
                            cells[i * cols + j] = 3; break;
                        case 291:
                            cells[i * cols + j] = 4; break;
                        case 293:
                            cells[i * cols + j] = 1; break;
                        case 328:
                            cells[i * cols + j] = 1; break;
                        case 346:
                            cells[i * cols + j] = 1; break;
                        case 396:
                            cells[i * cols + j] = 1; break;
                        case 443:
                            cells[i * cols + j] = 1; break;
                        case 456:
                            cells[i * cols + j] = 1; break;
                        default:
                            cells[i * cols + j] = 9000; break;

                    }
                }
            }
            return cells;
        }

    }
}
