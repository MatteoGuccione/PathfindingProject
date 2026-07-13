using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsameFineAnnoVS
{
    abstract class Actor : GameObject
    {
        public int MaxHealth;
        public int Health;

        public Vector2 pointToReach; //End point of the path that the actor needs to take

        protected bool isPressed = false; //Used to make sure the right click is only clicked once during every click
        public Agent Agent; //Class used for pathfinding
        protected Animation anim; //Class used for the animation of the players
        protected Vector2 texOff; //Used 
        protected Vector2 lastdirection = Vector2.Zero; //Used to check if the class should or not update the animation to a new one
        public Dictionary<string, bool> Inv; //Class used to store the items that the player currently has
        public bool isMoving { get; set; }
        public bool isAlive { get { return Health > 0; } } //used to check if the player is still alive after taking dmg

        public Actor(string textureName) : base(textureName)
        {
            IsActive = true;
            Forward = new Vector2(1f, 0f); //Used now since the sprite are not aligned correctly at the first frame
            sprite.pivot = new Vector2(0.5f, 0.5f); //Same thing here, if the pivot is not 0.5f the sprite is not all contained in a single block of the map
            Inv = new Dictionary<string, bool>();
            Inv.Add("Pickaxe", false);
            Inv.Add("Sword", false);
            Inv.Add("Key", false);
            Agent = new Agent(this);
            anim = new Animation((int)sprite.Width, (int)sprite.Height, 20, 16);
            anim.Play();
            MaxHealth = 3;
            Health = MaxHealth;
            UpdateMngr.AddItem(this);
            DrawMngr.AddItem(this);
            
        }

        public virtual void SetSpriteDirection(Vector2 direction) //Method used by his subclasses
        {
        }
        public virtual void CheckMovement(Vector2 direction) //Method used by his subclasses
        {
        }

        public void ResetHealth()
        {
            Health = MaxHealth;
        }
        public virtual void HeadToPoint() //after a right mouse click this function makes so that the player proceeds to the point clicked
        {
            Vector2 dist = pointToReach - Position;
            Vector2 PTRV = pointToReach / 16;
            Vector2 PV = Position / 16;
            if (Agent.Target == null)
            {
                List<Node> path = (Game.CurrentScene).Map.GetPath((int)PV.X, (int)PV.Y, (int)PTRV.X, (int)PTRV.Y);
                Agent.SetPath(path);
            }

            Agent.Update();
        }

        public override void Update() //Every frame this method check if the point to reach is not a vector zero, if it is not he will head to the point
        {
            if (pointToReach != Vector2.Zero)
            {
                HeadToPoint();
            }

        }

        public virtual void Input() //method used to modify the value of pointToReach so that the player can command the actor
        {
            if (Game.Window.MouseRight && isPressed == false)
            {
                isPressed = true;
                Agent.Target = null;
                pointToReach = new Vector2(Game.Window.MouseX, Game.Window.MouseY);
            }
            else if (!Game.Window.MouseRight)
            {
                isPressed = false;
            }
        }

        public virtual void OnDie() //Method called when the player is dead, removes him from the scene
        {
            IsActive = false;
        }
        public override void Draw() //Method used to draw the sprite of the player every frame
        {
            if (sprite != null)
            {
                sprite.DrawTexture(texture, (int)texOff.X, (int)texOff.Y, (int)sprite.Width, (int)sprite.Height);
            }
        }

    }
}
