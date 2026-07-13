using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;
using Aiv.Fast2D;

namespace EsameFineAnnoVS
{
    internal class DungeonScene : Scene
    {
        public TileMap map;
        private Object sword;
        private Enemy blob;
        private Enemy blob2;
        private int switchBool = -1;
        private bool notMoving = false;
        private Vector2 lastPosition;
        private AudioSource pickupSource;
        private AudioClip pickup;
        private AudioSource battleSource;
        private AudioClip attackSound;
        private AudioClip hurtSound;
        private AudioClip deathSound;
        public override void Start()
        {

            IsPlaying = true;
            LoadAssets();
            map = new TileMap("Assets/DungeonMap.tmx");
            Game.Actor.Position = new Vector2(38f * 16, 38f * 16) + Game.Actor.Pivot;
            blob = new Enemy("BlobAnim");
            blob.Position = new Vector2(32f * 16, 5f * 16);
            sword = new Object("Sword");
            sword.Position = new Vector2(38 * 16, 16);
            blob2 = new Enemy("BlobAnim");
            blob2.Position = new Vector2(28f * 16, 5f * 16);
            blob2.Pivot = new Vector2(0.5f, 0.5f);
            UpdateMngr.AddItem(Game.Actor);
            DrawMngr.AddItem(Game.Actor);
            LoadMap();

            base.Start();


            bgMusic = GfxMngr.GetClip("DarkCave"); //Create the new BG Music
            pickupSource = new AudioSource();
            pickupSource.Position = new Vector3(Game.Window.Width * 0.5f, Game.Window.Height * 0.5f, 0.0f);
            pickupSource.ReferenceDistance = 100.0f;
            pickupSource.RolloffFactor = 5.0f;
            pickupSource.MaxDistance = 200.0f;
            pickup = GfxMngr.GetClip("Pickup"); //Create a pickupSound

            battleSource = new AudioSource(); //Create new BattleSounds
            battleSource.Position = new Vector3(Game.Window.Width * 0.5f, Game.Window.Height * 0.5f, 0.0f);
            battleSource.ReferenceDistance = 100.0f;
            battleSource.RolloffFactor = 5.0f;
            battleSource.MaxDistance = 200.0f;
            attackSound = GfxMngr.GetClip("Attack");//Create an Attack Sound
            hurtSound = GfxMngr.GetClip("Hurt");//Create a getting damaged sound
            deathSound = GfxMngr.GetClip("Death");//Create a death sound
        }

        protected override void LoadMap() //Every time a change is applied to the map this method is called to make sure every tile has the right cost
        {
            int[] cells = map.layer.AdjustLayer();
            Map = new Map(map.layer.rows, map.layer.cols, cells);
        }


        public override void Update()
        {
            UpdateMngr.Update();
            CheckPosition();

        }
        public void CheckSwitchCondition() //Check the condition of the fake wall and spikes based on if the switch present on the map is 
                                           //pressed or not
        {
            if (switchBool == 1)
            {
                for (int i = 0; i < map.layer.IDs.Length; i++)
                {
                    if (map.layer.IDs[i] == "295")
                    {

                        map.layer.IDs[i] = "252";
                    }
                    if (map.layer.IDs[i] == "289")
                    {

                        map.layer.IDs[i] = "290";
                    }
                    if (map.layer.IDs[i] == "292")
                    {
                        map.layer.IDs[i] = "291";
                    }
                }
                LoadMap();
            }
            else
            {
                for (int i = 0; i < map.layer.IDs.Length; i++)
                {
                    if (map.layer.IDs[i] == "252")
                    {

                        map.layer.IDs[i] = "295";
                    }
                    if (map.layer.IDs[i] == "290")
                    {

                        map.layer.IDs[i] = "289";
                    }
                    if (map.layer.IDs[i] == "291")
                    {
                        map.layer.IDs[i] = "292";
                    }
                }
                LoadMap();
            }
        }
        public void CheckPosition() //Method used to check if some condition based on the position of the actors are met
        {

            if (Game.Actor.Position == new Vector2(39f * 16, 38f * 16)) //Check if the player is in the position of the stair and if it is met
            {
                NextScene = new OverworldPlayScene(); //The scene changes into a new Scene
                OnExit();
            }
            if (sword != null)//Check if the sword still exist
            {
                if (Game.Actor.Position == sword.Position)//if it exist and the player is in the same position as the sword
                {
                    //The player gets a sword and the sword is destroyed from the scene
                    sword.Destroy();
                    sword = null;
                    Game.Actor.Inv["Sword"] = true;
                    pickupSource.Play(pickup);
                    CreateSwordDialogue();
                }
            }

            if (Game.Actor.Position == new Vector2(33 * 16, 28 * 16) || Game.Actor.Position == new Vector2(38 * 16, 31 * 16))
            {
                Game.Actor.Position = lastPosition;
                Game.Actor.pointToReach = Vector2.Zero;
                switchBool *= -1;
                CheckSwitchCondition();
            }
            //These two ifs check if the blobs are in the same position as that of the player and check if the player has the sword or not
            if (blob != null)
            {
                if (Game.Actor.Position == blob.Position)
                {
                    //if the players has the sword the blob is killed
                    if (Game.Actor.Inv["Sword"] == true)
                    {
                        battleSource.Play(attackSound);
                        blob.DestroyFsm();
                        blob.Destroy();
                        blob = null;
                    }
                    //else the player takes damage and if the damage bring him to 0 HP he dies
                    else
                    {
                        battleSource.Play(hurtSound);
                        Game.Actor.Position = new Vector2(29 * 16, 20 * 16);
                        Game.Actor.Health--;
                        Game.Actor.pointToReach = Vector2.Zero;
                        if (Game.Actor.Health <= 0)
                        {
                            battleSource.Play(deathSound);
                            Game.Actor.OnDie();
                        }
                    }
                }
            }
            if (blob2 != null)
            {
                if (Game.Actor.Position == blob2.Position)
                {
                    if (Game.Actor.Inv["Sword"] == true)
                    {
                        battleSource.Play(attackSound);
                        blob2.DestroyFsm();
                        blob2.Destroy();
                        blob2 = null;
                    }
                    else
                    {
                        battleSource.Play(hurtSound);
                        Game.Actor.Position = new Vector2(29 * 16, 20 * 16);
                        Game.Actor.Health--;
                        Game.Actor.pointToReach = Vector2.Zero;
                        if (Game.Actor.Health <= 0)
                        {
                            battleSource.Play(deathSound);
                            Game.Actor.OnDie();
                        }
                    }
                }
            }
            //If the player is no longer alive the game is over and we go to the end scene caused by an enemy attack
            if (!Game.Actor.isAlive)
            {
                NextScene = new EndScene("Enemy");
                OnExit();
            }
            lastPosition = Game.Actor.Position;
        }

        public void CreateSwordDialogue()
        {
            dialogueBox.isActive = true;
            Vector2 wordPosition = new Vector2(0, 510);
            dialogue1 = new TextObject(wordPosition, "With this sword maybe i can cut ", FontMngr.GetFont(), 0);
            Vector2 wordPosition2 = new Vector2(0, 550);
            dialogue2 = new TextObject(wordPosition2, "the tree near the lake", FontMngr.GetFont(), 0);
            dialogue1.IsActive = true;
            dialogue2.IsActive = true;
            dialIsPlaying = true;

        }
        public override Scene OnExit()
        {
            Game.Actor.Position = new Vector2(34f * 16, 15f * 16);
            pickupSource = null;
            pickup = null;
            battleSource = null;
            attackSound = null;
            hurtSound = null;
            deathSound = null;
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
