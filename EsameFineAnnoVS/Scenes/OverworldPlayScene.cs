using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using EsameFineAnnoVS;
using OpenTK;
using Aiv.Audio;

namespace EsameFineAnnoVS
{
    class OverworldPlayScene : Scene
    {
        private Object pickaxe;
        private Object chest;
        private TileMap map;
        public bool isPressed = true;
        private bool checkSapling = true;
        private bool checkRocks = true;
        private AudioSource pickupSource;
        private AudioClip pickup;

        public OverworldPlayScene() : base()
        {
        }

        public override void Start()
        {
            UpdateMngr.AddItem(Game.Actor);
            DrawMngr.AddItem(Game.Actor);
            IsPlaying = true;
            base.Start();
            LoadAssets();
            if (Game.Actor.Inv["Pickaxe"] == false)
            {
                CreateStartDialogue();
            }
            map = new TileMap("Assets/OverworldMap.tmx");
            if (Game.Actor.Inv["Key"] == true) //if the player inventory contains a key the closed door becomes an open one
            {
                for (int i = 0; i < map.layer.IDs.Length; i++)
                {
                    if (map.layer.IDs[i] == "164")
                    {

                        map.layer.IDs[i] = "182";
                    }
                }
            }

            if (Game.Actor.Inv["Pickaxe"] == true) //if the player inventory contains a pickaxe the minerals blocking the dungeon get destroyed
            {
                for (int i = 0; i < map.layer.IDs.Length; i++)
                {
                    if (map.layer.IDs[i] == "102")
                    {

                        map.layer.IDs[i] = "79";
                    }
                }
            }
            if (Game.Actor.Position == Vector2.Zero) //if the players does not already have a position a new Position is assigned to him
            {
                Game.Actor.Position = new Vector2(16 * 16, 4 * 16);
            }

            if (Game.Actor.Inv["Pickaxe"] == false) //if the player inventory does not contain a pickaxe a chest containing one is spawned
            {
                chest = new Object("Chest");
                chest.Position = new Vector2(12 * 16f, 19 * 16f) + chest.Pivot;
            }


            LoadMap();
            bgMusic = GfxMngr.GetClip("Overworld");
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

        public void CreateStartDialogue()
        {

            dialogueBox.isActive = true;
            Vector2 wordPosition = new Vector2(0, 510);
            dialogue1 = new TextObject(wordPosition, "Where could I have left my Keys?", FontMngr.GetFont(), 0);
            Vector2 wordPosition2 = new Vector2(0, 550);
            dialogue2 = new TextObject(wordPosition2, "Maybe I should check the maze", FontMngr.GetFont(), 0);
            dialogue1.IsActive = true;
            dialogue2.IsActive = true;
            dialIsPlaying = true;

        }
        public void CreatePickaxeDialogues()
        {
            dialogueBox.isActive = true;
            Vector2 wordPosition = new Vector2(0, 500);
            dialogue1 = new TextObject(wordPosition, "A pickaxe?", FontMngr.GetFont(), 0);
            Vector2 wordPosition2 = new Vector2(0, 550);
            dialogue2 = new TextObject(wordPosition2, "Maybe I can go check", FontMngr.GetFont(), 0);
            Vector2 wordPosition3 = new Vector2(0, 600);
            dialogue3 = new TextObject(wordPosition3, "the dungeon now", FontMngr.GetFont(), 0);
            dialogue1.IsActive = true;
            dialogue2.IsActive = true;
            dialogue3.IsActive = true;
            dialIsPlaying = true;

        }
        public void CreateDungeonDialogue()
        {
            dialogueBox.isActive = true;
            Vector2 wordPosition = new Vector2(0, 500);
            dialogue1 = new TextObject(wordPosition, "Nice. Time to see", FontMngr.GetFont(), 0);
            Vector2 wordPosition2 = new Vector2(0, 550);
            dialogue2 = new TextObject(wordPosition2, "if my keys are down there", FontMngr.GetFont(), 0);
            dialogue1.IsActive = true;
            dialogue2.IsActive = true;
            dialIsPlaying = true;
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
            else if(!Game.Window.GetKey(KeyCode.Space))
            {
                keyisPressed = false;
            }
            base.Input();
        }

        public override void Update()
        {
            UpdateMngr.Update();
            CheckPosition();

        }

        public void CheckPosition()
        {
            //if the player does not already have a pickaxe
            if (Game.Actor.Inv["Pickaxe"] == false)
            {
                //and his position is the same as the chest's position
                if (Game.Actor.Position == new Vector2(12f * 16f, 19f * 16f))
                {
                    if (chest != null)
                    {
                        //A pickaxe is spawned nearby and the chest is removed
                        pickaxe = new Object("Pickaxe");
                        pickaxe.Position = chest.Position + new Vector2(1f * 16f, 0f);
                        chest.Destroy();
                        chest = null;
                    }
                }
                //and his position is the same as the pickaxe
                if (Game.Actor.Position == new Vector2(13f * 16f, 19f * 16f))
                {
                    if (pickaxe != null)
                    {
                        //the pickaxe is added to his inventory and the pickaxe is removed
                        Game.Actor.Inv["Pickaxe"] = true;
                        pickaxe.Destroy();
                        pickaxe = null;
                        pickupSource.Play(pickup);
                        CreatePickaxeDialogues();

                    }
                }
            }
            //if the player position is the same as the newly open door
            if (Game.Actor.Position == new Vector2(16f * 16f, 3f * 16f) && Game.Actor.Inv["Key"] == true)
            {
                //The scene changes to the interiors of the house
                NextScene = new HousePlayScene();
                OnExit();
            }
            //if the players position is the same as an open door
            if (playerDoorPosition())
            {
                //he enters the house
                NextScene = new RandomHouse();
                OnExit();
            }
            //If the player position is one tile away from the sapling and he posses a sword the sapling gets cut down
            if (Game.Actor.Position == new Vector2(5f * 16f, 17f * 16f) && Game.Actor.Inv["Sword"] == true)
            {

                for (int i = 0; i < map.layer.IDs.Length; i++)
                {
                    if (map.layer.IDs[i] == "59")
                    {
                        map.layer.IDs[i] = "56";
                    }
                }

                LoadMap();
            }
            if (Game.Actor.Position == new Vector2(5f * 16f, 19f * 16f) && Game.Actor.Inv["Sword"] == true)
            {
                for (int i = 0; i < map.layer.IDs.Length; i++)
                {
                    if (map.layer.IDs[i] == "59")
                    {

                        map.layer.IDs[i] = "56";
                    }
                }
                LoadMap();
            }
            //if the player position is the same as the ladder
            if (Game.Actor.Position == new Vector2(5f * 16f, 16f * 16f))
            {
                //the scene changes to the future map scene
                NextScene = new FutureMapScene();
                OnExit();
            }
            //If the player position is near the rocks blobking the access to the dungeon, the rocks get cleared and the dungeon is now available
            if ((Game.Actor.Position == new Vector2(34f * 16f, 16f * 16f) || (Game.Actor.Position == new Vector2(35f * 16f, 16f * 16f))) && Game.Actor.Inv["Pickaxe"] == true)
            {
                //The rocks are only destroyed if they are not already destroyed
                if (checkRocks == true)
                {
                    for (int i = 0; i < map.layer.IDs.Length; i++)
                    {
                        if (map.layer.IDs[i] == "102")
                        {

                            map.layer.IDs[i] = "79";
                        }
                    }
                    checkRocks = false;
                    if (!Game.Actor.Inv["Sword"] == true)
                    {
                        CreateDungeonDialogue();
                    }
                    LoadMap();
                }
            }
            //If the player position is the same as one of the two stairs leading to the dungeon he is able to descend into the dungeon
            if ((Game.Actor.Position == new Vector2(34f * 16, 14 * 16f)) || (Game.Actor.Position == new Vector2(35f * 16, 14 * 16f)))
            {
                NextScene = new DungeonScene();
                OnExit();
            }
        }

        public bool playerDoorPosition()
        {
            if (Game.Actor.Position == new Vector2(19 * 16, 3 * 16))
            {
                return true;
            }
            if (Game.Actor.Position == new Vector2(13 * 16, 3 * 16))
            {
                return true;
            }
            if (Game.Actor.Position == new Vector2(10 * 16, 3 * 16))
            {
                return true;
            }
            if (Game.Actor.Position == new Vector2(7 * 16, 3 * 16))
            {
                return true;
            }
            return false;
        }
        public override Scene OnExit()
        {
            map = null;
            pickupSource = null;
            pickup = null;
            return base.OnExit();
        }

        public override void Draw()
        {
            DrawMngr.Draw();
        }
    }
}
