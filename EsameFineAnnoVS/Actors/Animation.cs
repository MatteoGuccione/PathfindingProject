using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace EsameFineAnnoVS
{
    class Animation
    {
        protected int numFrames; //Numbers of frame the animation has
        protected float frameDuration; //How much does a single frame last
        protected bool isPlaying; //Check if the animation is playing or not
        public int currentFrame; //Current frame of the animation that is playing

        protected int frameWidth; //Width of the image used for the animation
        protected int frameHeight; //Height of the image used for the animation

        public bool Loop; //if the animation is supposed to loop or not

        public Animation(int frameW, int frameH, float fps, int frames, bool isPlay = false, int currentFrame = 0, bool loop = true)
        {
            this.frameWidth = frameW;
            this.frameHeight = frameH;

            this.currentFrame = currentFrame;
            isPlaying = isPlay;
            this.frameDuration = 1 / fps;

            this.numFrames = frames;

            this.Loop = loop;
        }

        public virtual void Update(float deltaTime, ref Vector2 offset) //Method used to update the animation every single frame
        {
            if(isPlaying)
            {
                currentFrame++;
                if(currentFrame >= numFrames)
                {
                    if(Loop) //Check to see if the animation should start once again or end
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        OnAnimationEnd();
                        return;
                    }
                }

                offset.Y = frameHeight * currentFrame;
            }
        }

        protected virtual void OnAnimationEnd()
        {
            isPlaying = false;
        }

        public virtual void Play()
        {
            isPlaying = true;
        }

        protected virtual void Pause()
        {
            isPlaying = false;
        }

        public virtual void Stop()
        {
            isPlaying = false;
            currentFrame = 0;
        }
    }
}
