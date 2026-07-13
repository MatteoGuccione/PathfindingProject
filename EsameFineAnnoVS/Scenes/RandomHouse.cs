using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    class RandomHouse : Scene
    {
        public TileMap map;

        public override void Start()
        {
            IsPlaying = true;

            LoadAssets();

            map = new TileMap("Assets/HouseMap.tmx");
            LoadMap();
            Game.Actor.Position = new Vector2(20f * 16, 26f * 16) + Game.Actor.Pivot;
            Game.Actor.pointToReach = Vector2.Zero;
            UpdateMngr.AddItem(Game.Actor);
            DrawMngr.AddItem(Game.Actor);


        }

        protected override void LoadMap()
        {
            int[] cells = map.layer.AdjustLayer();
            Map = new Map(map.layer.rows, map.layer.cols, cells);
        }


        public override void Update()
        {
            UpdateMngr.Update();
            CheckPosition();
        }

        private void CheckPosition()
        {
            //if the player position is at the exit of the house he is sent back outside
            if (Game.Actor.Position == new Vector2(20f * 16, 27f * 16))
            {
                NextScene = new OverworldPlayScene();
                OnExit();
            }
            //if the player position is the same as the bed he wins the game
            if (Game.Actor.Position == new Vector2(25f * 16, 12f * 16))
            {
                NextScene = new EndScene("Winning");
                OnExit();
            }
        }

        public override Scene OnExit()
        {
            UpdateMngr.ClearAll();
            DrawMngr.ClearAll();
            FontMngr.ClearAll();
            Game.Actor.Position = new Vector2(16f * 16, 4f * 16);
            return base.OnExit();
        }

        public override void Draw()
        {
            DrawMngr.Draw();
        }

    }
}
