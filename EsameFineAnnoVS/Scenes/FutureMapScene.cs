using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace EsameFineAnnoVS
{
    internal class FutureMapScene : Scene
    {
        public TileMap map;
        private Object key;
        private AudioSource pickupSource;
        private AudioClip pickup;

        public override void Start()
        {
            IsPlaying = true;

            LoadAssets();


            map = new TileMap("Assets/FutureMapArea.tmx");
            Game.Actor.Position = new Vector2(37f * 16, 3f * 16) + Game.Actor.Pivot;
            UpdateMngr.AddItem(Game.Actor);
            DrawMngr.AddItem(Game.Actor);
            if (!Game.Actor.Inv["Key"] == true)//If the player inventory already contains the key another one is not spawned
            {
                key = new Object("Key");
                key.Position = new Vector2(28f * 16, 15f * 16) + Game.Actor.Pivot;
            }

            LoadMap();
            base.Start();
            bgMusic = GfxMngr.GetClip("Future");
            pickupSource = new AudioSource();
            pickupSource.Position = new Vector3(Game.Window.Width * 0.5f, Game.Window.Height * 0.5f, 0.0f);
            pickupSource.ReferenceDistance = 100.0f;
            pickupSource.RolloffFactor = 5.0f;
            pickupSource.MaxDistance = 200.0f;
            pickup = GfxMngr.GetClip("Pickup");

        }

        protected override void LoadMap()
        {
            int[] cells = map.layer.AdjustLayer();
            Map = new Map(map.layer.rows, map.layer.cols, cells);
        }

        public void CreateKeyDialogues()
        {
            dialogueBox.isActive = true;
            Vector2 wordPosition = new Vector2(0, 510);
            dialogue1 = new TextObject(wordPosition, "Finally I can go back at home", FontMngr.GetFont(), 0);
            Vector2 wordPosition2 = new Vector2(0, 550);
            dialogue2 = new TextObject(wordPosition2, "Hopefully nothing goes wrong", FontMngr.GetFont(), 0);
            dialogue1.IsActive = true;
            dialogue2.IsActive = true;
            dialIsPlaying = true;

        }
        public override void Update()
        {
            UpdateMngr.Update();
            CheckPosition();
        }

        public void CheckPosition()
        {
            //if the position is the same as the stair's one than he is sent back to the OverworldMap
            if (Game.Actor.Position == new Vector2(37f * 16, 2f * 16))
            {
                NextScene = new OverworldPlayScene();
                OnExit();
            }
            //if the key still exist in the world
            if (key != null)
            {
                //And the player is in its same position
                if(Game.Actor.Position == key.Position)
                {
                    //the player gets a key and the key is removed from the game
                    key.Destroy();
                    key = null;
                    Game.Actor.Inv["Key"] = true;
                    pickupSource.Play(pickup);
                    CreateKeyDialogues();
                }
            }
        }

        public override Scene OnExit()
        {
            UpdateMngr.ClearAll();
            DrawMngr.ClearAll();
            FontMngr.ClearAll();
            Game.Actor.Position = new Vector2(5f * 16, 17f * 16);
            return base.OnExit();
        }

        public override void Input()
        {
            if (Game.Window.GetKey(KeyCode.Space) && !keyisPressed)
            {
                keyisPressed = true;
                dialogue1.IsActive = false;
                dialogue2.IsActive = false;
                if (dialogue3 != null)
                {
                    dialogue3.IsActive = false;
                }
                dialogueBox.isActive = false;
                dialIsPlaying = false;
            }
            else if (!Game.Window.GetKey(KeyCode.Space))
            {
                keyisPressed = false;
            }
            base.Input();
        }
        public override void Draw()
        {
            DrawMngr.Draw();
        }

    }
}
