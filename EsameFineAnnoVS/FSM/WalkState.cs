using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace EsameFineAnnoVS
{
    class WalkState : State
    {
        private Enemy owner; //Owner of the fms

        public WalkState(Enemy owner)
        {
            this.owner = owner;
        }

        public override void Update() //If the owner can see the player he goes into the follow state else he heads to a random point
        {
            if (owner.CanDetectPlayer(Game.Actor))
            {
                owner.Rival = Game.Actor;
                fsm.GoTo(StateEnum.FOLLOW);
                owner.Agent.Target = null;
                return;
            }
            owner.HeadToPoint();
        }
    }
}
