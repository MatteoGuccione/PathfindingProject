using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace EsameFineAnnoVS
{

    class Enemy : Actor
    {
        private StateMachine fsm; //State machine used to manage the behaviour of the enemy

        protected float visionRadius; //How far the enemy is able to see
        public Actor Rival; //The actor that he is currently focused on
        public float animationTime;

        public Enemy(string textureName) : base(textureName)
        {
            IsActive = true;
            visionRadius = 5.0f * 16; //Set his vision radius to 5 blocks
            Agent = new Agent(this);
            fsm = new StateMachine();
            fsm.AddState(StateEnum.WALK, new WalkState(this));
            fsm.AddState(StateEnum.FOLLOW, new FollowState(this));
            fsm.GoTo(StateEnum.WALK);
            sprite = new Sprite(texture.Width, texture.Height / 5);
            anim = new Animation((int)sprite.Width, (int)sprite.Height, 60, 5, true);
            //Reset();
            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
        }


        public bool CanDetectPlayer(Actor player) //Method used to see if the player is currently in range for the enemy to see him
        {
            Vector2 dist = player.Position - Position;
            if(Game.Actor.Position.Y > 18f * 16f)
                return false;

            if (dist.LengthSquared < visionRadius * visionRadius)
            {
                return true;
            }

            return false;
        }


        public void DestroyFsm() //Used when the owner of the fsm is killed
        {
            fsm = null;
        }

        public override void SetSpriteDirection(Vector2 direction)
        {
            if (anim.currentFrame == 0)
            {
                texOff = Vector2.Zero;
            }
            anim.Update(Game.DeltaTime, ref texOff);
             
        }

        public override void HeadToPoint() //Same method as the one of the Actors only difference is that the point to reach is 
                                           //decided randomly on a smaller map, since the movement of the enemy is limited
        {
            Vector2 PV = Position / 16;
            if (Agent.Target == null)
            {
                Node randomNode = (Game.CurrentScene).Map.GetRandomNode(27, 39, 2, 17);
                int adjustedX = randomNode.X / 16;
                int adjustedY = randomNode.Y / 16;
                List<Node> path = (Game.CurrentScene).Map.GetPath((int)PV.X, (int)PV.Y, adjustedX, adjustedY);
                
                Agent.SetPath(path);
            }

            Agent.Update();
        }

        public void HeadToPlayer() // if the enemy can see the player he will attempt to go near him
        {
            if (Agent.Target == null)
            {
                Vector2 adjPosition = Position / 16;
                Vector2 adjRivalPosition = Rival.Position / 16;
                List<Node> path = (Game.CurrentScene).Map.GetPath((int)adjPosition.X, (int)adjPosition.Y, (int)adjRivalPosition.X, (int)adjRivalPosition.Y);
                Agent.SetPath(path);
            }

            Agent.Update();
        }

        public override void Update() //Every frame the fsm will update
        {
            if (IsActive)
            {
                fsm.Update();
            }
        }

    }
}
