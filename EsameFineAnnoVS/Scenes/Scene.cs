using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; } //Check if the scene is still playing or not

        public Map Map { get; set; } //Map of the current scene
        public Scene NextScene; //The next scene used after this one is done playing
        public AudioSource bgSource { get; protected set; } //Source of the Music
        public AudioClip bgMusic { get; protected set; } //Music using during the game

        public bool dialIsPlaying = false;
        public bool keyisPressed = false;
        protected TextObject dialogue1;
        protected TextObject dialogue2;
        protected TextObject dialogue3;
        protected DialogueBox dialogueBox;
        public Scene()
        {
        }

        public virtual void Start()
        {
            dialogueBox = new DialogueBox(new Vector2(0, 500), new Vector2(Game.Window.Width, Game.Window.Height));
            bgSource = new AudioSource();//Create a new Audio source that the other classes can modify
            bgSource.Position = new Vector3(Game.Window.Width * 0.5f, Game.Window.Height * 0.5f, 0.0f);
            bgSource.ReferenceDistance = 100.0f;
            bgSource.RolloffFactor = 5.0f;
            bgSource.MaxDistance = 200.0f;
            IsPlaying = true;
        }

        public virtual Scene OnExit()
        {
            //Clears basically everything that was used during the scene (Expect the gfxMngr)
            UpdateMngr.ClearAll();
            DrawMngr.ClearAll();
            FontMngr.ClearAll();
            bgSource = null;
            bgMusic = null;
            IsPlaying = false;
            return NextScene;
        }

        protected virtual void LoadAssets() //All assets used are registered in the GfxMngr so they can be used anytime and anywhere you want
        {
            GfxMngr.AddTexture("TestingMap", "Assets/PixelPackTOPDOWN8BIT.png");

            GfxMngr.AddTexture("AdventurerIdleD", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Idle D.gif");
            GfxMngr.AddTexture("AdventurerIdleU", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Idle U.gif");
            GfxMngr.AddTexture("AdventurerIdleR", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Idle R.gif");
            GfxMngr.AddTexture("AdventurerWalkD", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Walk D.gif");
            GfxMngr.AddTexture("AdventurerWalkU", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Walk U.gif");
            GfxMngr.AddTexture("AdventurerWalkR", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Walk R.gif");

            GfxMngr.AddTexture("PrincessIdleD", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Idle D.gif");
            GfxMngr.AddTexture("PrincessIdleU", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Idle U.gif");
            GfxMngr.AddTexture("PrincessIdleR", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Idle R.gif");
            GfxMngr.AddTexture("PrincessWalkD", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk D.gif");
            GfxMngr.AddTexture("PrincessWalkU", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk U.gif");
            GfxMngr.AddTexture("PrincessWalkR", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk R.gif");

            GfxMngr.AddTexture("DoggoIdleD", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Idle D.gif");
            GfxMngr.AddTexture("DoggoIdleU", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Idle U.gif");
            GfxMngr.AddTexture("DoggoIdleR", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Idle R.gif");
            GfxMngr.AddTexture("DoggoWalkD", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk D.gif");
            GfxMngr.AddTexture("DoggoWalkU", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk U.gif");
            GfxMngr.AddTexture("DoggoWalkR", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk R.gif");

            GfxMngr.AddTexture("AdventurerWalkD-0-NBG", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer_Walk_D-0-NBG.png");
            GfxMngr.AddTexture("AdventurerWalkU-NBG", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Walk U-NBG.png");
            GfxMngr.AddTexture("AdventurerWalkR-NBG", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Walk R-NBG.png");

            GfxMngr.AddTexture("PrincessWalkD-NBG", "Assets\\SPRITES\\HEROS\\PRINCESS\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk D-NBG.png");
            GfxMngr.AddTexture("PrincessWalkU-NBG", "Assets\\SPRITES\\HEROS\\PRINCESS\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk U-NBG.png");
            GfxMngr.AddTexture("PrincessWalkR-NBG", "Assets\\SPRITES\\HEROS\\PRINCESS\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk R-NBG.png");

            GfxMngr.AddTexture("DoggoWalkD-NBG", "Assets\\SPRITES\\HEROS\\DOGGO\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk D_NBG.png");
            GfxMngr.AddTexture("DoggoWalkU-NBG", "Assets\\SPRITES\\HEROS\\DOGGO\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk U_NBG.png");
            GfxMngr.AddTexture("DoggoWalkR-NBG", "Assets\\SPRITES\\HEROS\\DOGGO\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk R_NBG.png");


            GfxMngr.AddTexture("BlobIdleD", "Assets\\SPRITES\\ENEMIES\\BLOB\\ENEMIES_PixelPackTOPDOWN8BIT_Blob Idle.gif");
            GfxMngr.AddTexture("BlobAnim", "Assets\\SPRITES\\ENEMIES\\BLOB\\ENEMIES_PixelPackTOPDOWN8BIT_Blob Walk.png");


            GfxMngr.AddTexture("Sword", "Assets\\SPRITES\\ITEMS\\item8BIT_sword.png");
            GfxMngr.AddTexture("Chest", "Assets\\SPRITES\\ITEMS\\item8BIT_chest.png");
            GfxMngr.AddTexture("Pickaxe", "Assets\\SPRITES\\ITEMS\\item8BIT_pickaxe.png");
            GfxMngr.AddTexture("Key", "Assets\\SPRITES\\ITEMS\\item8BIT_skullkey.png");



            GfxMngr.AddClip("Overworld", "Assets\\MUSIC\\1BITTopDownMusics - Track 01 (1BIT Adventure).wav");
            GfxMngr.AddClip("DarkCave", "Assets\\MUSIC\\1BITTopDownMusics - Track 02 (1BIT Dark Cave).wav");
            GfxMngr.AddClip("Future", "Assets\\MUSIC\\1BITTopDownMusics - Track 03 (1BIT Eerie).wav");

            GfxMngr.AddClip("Pickup", "Assets\\SFX\\Pickup01.wav");
            GfxMngr.AddClip("Attack", "Assets\\SFX\\Attack01.wav");
            GfxMngr.AddClip("Hurt", "Assets\\SFX\\Hurt01.wav");
            GfxMngr.AddClip("Death", "Assets\\SFX\\Death01.wav");

            FontMngr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontMngr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
        }
        public virtual void Input() //Check the input of the player
        {
            if (dialIsPlaying == false)
            {
                Game.Actor.Input();
            }

        }
        public virtual void Update()
        {

        }

        public abstract void Draw();
        public virtual void CreateDialogues()
        {
            
        }
        public virtual void WaitDialogues()
        {
            
        }
        protected abstract void LoadMap();
    }
}
