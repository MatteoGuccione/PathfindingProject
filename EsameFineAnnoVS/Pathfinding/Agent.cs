using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace EsameFineAnnoVS
{
    class Agent
    {
        public List<Node> path; //Path that the owner would traverse
        Node current; //Current Node
        Node target; //Next Node
        public Vector2 direction { get; private set; }
        Actor owner; //Owner of this class

        private float timeToAct; //Time left before the owner is able to move again
        public Node Target { get { return target; } set { target = value; } }

        public Agent(Actor owner)
        {
            this.owner = owner;
            target = null;

            ResetTimer();
        }


        public virtual void SetPath(List<Node> newPath) //Set the path that the owner will follow
        {
            path = newPath;

            if (target == null && path.Count > 0)
            {
                target = path[0];
                path.RemoveAt(0);
            }
            else if (path.Count > 0)
            {
                int dist = Math.Abs(path[0].X * 16 - target.X * 16) + Math.Abs(path[0].Y * 16 - target.Y * 16);

                if (dist == 1)
                {
                    path.Insert(0, current);
                }
            }
        }


        public void Update() //Every frame the owner would go to the next target position and repeat the process until
                            //the path is not empty
        {
            if (target != null)
            {
                WaitTimer();
                if (TimerIsOver())
                {
                    Vector2 destination = new Vector2(target.X, target.Y);
                    Vector2 pos16 = owner.Position;
                    pos16.X = (int)(pos16.X / 16);
                    pos16.Y = (int)(pos16.Y / 16);
                    direction = destination - pos16;
                    current = target;
                    owner.Position += direction.Normalized() * 16;
                    SetDirection(direction);
                    

                    if (path.Count == 0)
                    {
                        target = null;
                        direction = Vector2.Zero;
                    }
                    else
                    {
                        target = path[0];
                        path.RemoveAt(0);
                    }
                    ResetTimer();
                }
            }
        }


        public void SetDirection(Vector2 direction) //Calls the appropriate method of the owner (Check the direction and change the sprite)
        {
            owner.SetSpriteDirection(direction);
        }

        private void WaitTimer() //Every frame timeToAct ticks down
        {
            timeToAct -= Game.DeltaTime;
        }
        private void ResetTimer() //After the timer reaches less than 0 and the owner act the timer is reset based on the type of the owner
        {
            if (owner is Enemy)
            {
                timeToAct = 1f;
            }
            else
            {
                timeToAct = 0.25f;
            }
        }
        private bool TimerIsOver() //Check every frame if the timer is over
        {
            if(timeToAct <= 0)
                return true;
            return false;
        }

    }
}
