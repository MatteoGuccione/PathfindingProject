using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;
using Aiv.Fast2D;
using OpenTK;

namespace EsameFineAnnoVS
{
    static class Game
    {
        // Variables
        public static Window Window;
        public static Actor Actor; //Player character
        public static AudioDevice output = new AudioDevice(); //Audio Output used for hearing the sounds
        // Properties
        public static Scene CurrentScene { get; private set; }
        public static float DeltaTime { get { return Window.DeltaTime; } }
        public static float ScreenCenterX { get { return Window.Width * 0.5f; } }
        public static float ScreenCenterY { get { return Window.Height * 0.5f; } }

        public static float OptimalScreenHeight { get; private set; }
        public static float UnitSize { get; private set; }
        public static float OptimalUnitSize { get; private set; }


        public static void Init()
        {
            Window = new Window(640, 640, "The Adventures of the Hero, Princess and a dog?");

            // SCENES
            TitleScene titleScene = new TitleScene();
            //OverworldPlayScene playScene = new OverworldPlayScene();
            //HousePlayScene house = new HousePlayScene();
            //RandomHouse Rhouse = new RandomHouse();
            //FutureMapScene futurearea = new FutureMapScene();
            //DungeonScene dungeon = new DungeonScene();
            //EndScene endscene = new EndScene("Enemy");
            CurrentScene = titleScene;
        }

        public static void Play()
        {
            CurrentScene.Start();

            while (Window.IsOpened)
            {
               

                Window.SetTitle($"FPS: {1f / Window.DeltaTime}");

                if (Window.GetKey(KeyCode.Esc))
                {
                    break;
                }

                if (!CurrentScene.IsPlaying)
                {
                    Scene nextScene = CurrentScene.NextScene;

                    if (nextScene != null)
                    {
                        CurrentScene = nextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                if (CurrentScene.bgMusic != null)
                {
                    CurrentScene.bgSource.Stream(CurrentScene.bgMusic, Window.DeltaTime);
                }


                // INPUT
                CurrentScene.Input();

                

                // UPDATE
                CurrentScene.Update();


                // DRAW
                CurrentScene.Draw();

                Window.Update();
            }
        }
    }
}
