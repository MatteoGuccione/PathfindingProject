using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EsameFineAnnoVS
{
    internal class TileMap : I_Drawable
    {
        private string xmlFilePath;

        //Tileset
        TileSet tileSet;
        //TileLayer
        public TileLayer layer { get; }
        DrawLayer drawLayer;

        public DrawLayer Layer { get; }

        public TileMap(string filePath)
        {
            drawLayer = DrawLayer.Background; //set the  object to be drawn on the background
            DrawMngr.AddItem(this);

            xmlFilePath = filePath;

            XmlDocument xmlDoc = new XmlDocument();

            try //Check for some exception that might break the program
            {
                xmlDoc.Load(xmlFilePath);
            }
            catch (XmlException e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            //XML Parsing
            //Map Nodes and Attributes
            XmlNode mapNode = xmlDoc.SelectSingleNode("map");
            int mapCols = GetIntAttribute(mapNode,"width");
            int mapRows = GetIntAttribute(mapNode,"height");
            int mapTileW = GetIntAttribute(mapNode, "tilewidth");
            int mapTileH = GetIntAttribute(mapNode, "tileheight");

            //Tileset Node and Attributes
            XmlNode tilesetNode = mapNode.SelectSingleNode("tileset");
            int tileCount = GetIntAttribute(tilesetNode, "tilecount");
            int tilesetCols = GetIntAttribute(tilesetNode, "columns");
            int tilesetRows = tileCount / tilesetCols;
            tileSet = new TileSet("TestingMap", tilesetCols, tilesetRows,mapTileW, mapTileH);

            XmlNode layerNode = mapNode.SelectSingleNode("layer");

            layer = new TileLayer(layerNode, tileSet, mapCols, mapRows, mapTileW, mapTileH);
        }

        public static int GetIntAttribute(XmlNode node, string attrName)
        {
            return int.Parse(GetStringAttribute(node,attrName));
        }
        public static bool GetBoolAttribute(XmlNode node, string attrName)
        {
            return bool.Parse(GetStringAttribute(node, attrName));
        }
        public static string GetStringAttribute(XmlNode node, string attrName)
        {
            return node.Attributes.GetNamedItem(attrName).Value;
        }
        public void Draw()
        {
            layer.Draw();
        }
    }
}
