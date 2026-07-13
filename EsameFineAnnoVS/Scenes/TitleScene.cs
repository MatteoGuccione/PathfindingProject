using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;

namespace EsameFineAnnoVS
{
    class TitleScene : Scene
    {
        protected Adventurer ad;
        protected Doggo dog;
        protected Princess princess;
        protected TextObject selectChar;
        protected Animation adAnim;
        protected Animation princAnim;
        protected Animation dogAnim;
        protected bool isPressed = false;

        public TitleScene()
        {

        }

        public override void Start()
        {
            //Create a scaled off sprite of the three protagonists
            LoadAssets();
            Vector2 direction = new Vector2(0f, 1f);
            Vector2 selectPos = new Vector2(Game.Window.Width * 0.5f - 200f, 100f);
            selectChar = new TextObject(selectPos, "Select your character", FontMngr.GetFont(), 0);
            selectChar.IsActive = true;
            ad = new Adventurer();
            ad.Position = new Vector2(50, Game.Window.Height * 0.5f);
            ad.Scale *= 7f;
            princess = new Princess();
            princess.Position = new Vector2(Game.Window.Width * 0.5f - 60f, Game.Window.Height * 0.5f);
            princess.Scale *= 7f;
            dog = new Doggo();
            dog.Position = new Vector2(Game.Window.Width -150f, Game.Window.Height * 0.5f);
            dog.Scale *= 7f;
            base.Start();
        }

        protected override void LoadAssets()
        {
            FontMngr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontMngr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
            GfxMngr.AddTexture("AdventurerIdleD", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer Idle D.gif");
            GfxMngr.AddTexture("PrincessIdleD", "Assets\\SPRITES\\HEROS\\PRINCESS\\HEROS_PixelPackTOPDOWN8BIT_Princess Idle D.gif");
            GfxMngr.AddTexture("DogIdleD", "Assets\\SPRITES\\HEROS\\DOGGO\\HEROS_PixelPackTOPDOWN8BIT_Dog Idle D.gif");
            GfxMngr.AddTexture("AdventurerWalkD-0-NBG", "Assets\\SPRITES\\HEROS\\ADVENTURER\\HEROS_PixelPackTOPDOWN8BIT_Adventurer_Walk_D-0-NBG.png");
            GfxMngr.AddTexture("PrincessWalkD-NBG", "Assets\\SPRITES\\HEROS\\PRINCESS\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Princess Walk D-NBG.png");
            GfxMngr.AddTexture("DoggoWalkD-NBG", "Assets\\SPRITES\\HEROS\\DOGGO\\NBG\\HEROS_PixelPackTOPDOWN8BIT_Dog Walk D_NBG.png");

        }

        public override void Input()
        {
            //if a protagonist's sprite is clicked he is selected as the player's character
            Vector2 MousePos = Vector2.Zero;
            if (Game.Window.MouseLeft && isPressed == false)
            {
                MousePos = Game.Window.MousePosition;
                CheckPosition(MousePos);
                isPressed = true;
            }
            else if (!Game.Window.MouseRight)
            {
                isPressed = false;
            }
            
        }

        public void CheckPosition(Vector2 mousePos)
        {
            if ((mousePos.X > 70 && mousePos.X < 130) && (mousePos.Y > 320 && mousePos.Y < 420))
            {
                Game.Actor = new Adventurer();
                OnExit();
            }
            if ((mousePos.X > 270 && mousePos.X < 340) && (mousePos.Y > 320 && mousePos.Y < 420))
            {
                Game.Actor = new Princess();
                OnExit();
            }
            if ((mousePos.X > 510 && mousePos.X < 570) && (mousePos.Y > 320 && mousePos.Y < 420))
            {
                Game.Actor = new Doggo();
                OnExit();
            }
        }

        public override Scene OnExit()
        {
            ad = null;
            princess = null;
            dog = null;
            UpdateMngr.ClearAll();
            DrawMngr.ClearAll();
            GfxMngr.ClearAll();
            FontMngr.ClearAll();
            NextScene = new OverworldPlayScene();
            return base.OnExit();
        }

        public override void Draw()
        {
            DrawMngr.Draw();
        }

        protected override void LoadMap()
        {
            
        }
    }
}
