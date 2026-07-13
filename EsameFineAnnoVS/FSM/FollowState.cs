using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace EsameFineAnnoVS
{
    class FollowState : State
    {
        private Enemy owner; //owner of the fms


        public FollowState(Enemy owner)
        {
            this.owner = owner;

            owner.Agent.Target = null;
        }



        public override void Update() //If the owner can no longer see the player he return to the walk state, else he tries to reach him
        {
          
            if(!owner.CanDetectPlayer(Game.Actor))
            {
                fsm.GoTo(StateEnum.WALK);
            }
            else
            {
                owner.HeadToPlayer();
            }
        }
    }
}
