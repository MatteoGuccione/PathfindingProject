using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    internal class EndScene : Scene
    {
        protected TextObject endWord; //Words used to set the game over screen
        protected TextObject endWord2;
        protected TextObject endWord3;
        protected TextObject endWord4;
        protected string byWhat; //String used to determinate the cause of the game over
        public EndScene(string byWhat) : base()
        {
            this.byWhat = byWhat;
        }

        public override void Draw()
        {
            DrawMngr.Draw();
        }

        public override void Input()
        {
            
        }

        public override void Update()
        {
            //R is used as a retry key
            if (Game.Window.GetKey(KeyCode.Q))
            {
                NextScene = new TitleScene();
                OnExit();
            }
            if (Game.Window.GetKey(KeyCode.R))
            {
                Game.Actor.ResetHealth();
                NextScene = new DungeonScene();
                OnExit();
            }
        }

        public override Scene OnExit()
        {
            UpdateMngr.ClearAll();
            DrawMngr.ClearAll();
            FontMngr.ClearAll();
            IsPlaying = false;
            return NextScene;
        }

        public override void Start()
        {
            IsPlaying = true;
            LoadAssets();
            //If the cause of the end scene was winning the game, the game congratulates the player for winning the game
            if (byWhat == "Winning")
            {
                Vector2 wordPosition = new Vector2(Game.Window.Width * 0.5f - 200, Game.Window.Height * 0.5f - 200);
                endWord = new TextObject(wordPosition, "Congratulation you  ", FontMngr.GetFont(), 0);
                Vector2 wordPosition2 = wordPosition + new Vector2(0f, 100f);
                endWord2 = new TextObject(wordPosition2, "completed the game.", FontMngr.GetFont(), 0);
                Vector2 wordPosition3 = wordPosition2 + new Vector2(-100f, 100f);
                endWord3 = new TextObject(wordPosition3, "Press esc to close the game.", FontMngr.GetFont(), 0);
                endWord.IsActive = true;
                endWord2.IsActive = true;
                endWord3.IsActive = true;
            }
            //else the game tells the player of his unfortunate demise and tells him to press the R key to restart the game
            else if (byWhat == "Enemy")
            {
                Vector2 wordPosition = new Vector2(Game.Window.Width * 0.5f - 100, Game.Window.Height * 0.5f - 200);
                endWord = new TextObject(wordPosition, "YOU DIED!  ", FontMngr.GetFont(), 0);
                Vector2 wordPosition2 = wordPosition + new Vector2(-200, 100f);
                endWord2 = new TextObject(wordPosition2, "Press R to try the stage again", FontMngr.GetFont(), 0);
                Vector2 wordPosition3 = wordPosition2 + new Vector2(0f, 100f);
                endWord3 = new TextObject(wordPosition3, "Press Q to try again", FontMngr.GetFont(), 0);
                Vector2 wordPosition4 = wordPosition3 + new Vector2(0f, 100f);
                endWord4 = new TextObject(wordPosition4, "Press esc to close the game.", FontMngr.GetFont(), 0);
                endWord.IsActive = true;
                endWord2.IsActive = true;
                endWord3.IsActive = true;
                endWord4.IsActive = true;
            }
        }

        protected override void LoadMap()
        {
            
        }
    }
}
