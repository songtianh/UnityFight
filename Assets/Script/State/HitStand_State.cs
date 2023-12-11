using UnityEngine;

namespace gedou
{
    public class HitStand_State : StateBase
    {
        public HitStand_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            Entity.resoveAtk();
            Entity.hitTimes++;
            Entity.recoverTime =Entity.recoverMax;
            if (Entity.hitTimes % 2 == 0) {
                base.onEnter(AniName.hit);
            } else {
                base.onEnter(AniName.hit5);
            }
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            
            if (Entity.Jitui_Vel <= 0) {
                Entity.Jitui_Vel = 0;
            }
            // Entity.transform.position =new Vector3(
            //     Entity.transform.position.x + Entity.Jitui_Vel * Entity.AtkTurn,
            //     Entity.transform.position.y, 0);
            
            Entity.transform.position =new Vector3(
                Entity.transform.position.x ,Entity.transform.position.y, 0);
            Entity.Jitui_Vel -=  Entity.JituiVeol;
            Entity.recoverTime -= dt;
            if (Entity.recoverTime <= 0) {
                Entity.recoverTime = 0;
                Entity.aniComponent.switchState(AniName.idle);
            }
        }

        public override void onEvent(int type, object param)
        {
            base.onEvent(type, param);
            switch ((StateEventType) type)
            {
                case StateEventType.hit:
                    Entity.aniComponent.switchState(AniName.hit);
                    break;
                case StateEventType.hitJump:
                   Entity.aniComponent.switchState(AniName.hit3);
                    break;
            }
        }

        public override void onAniEnd()
        {
            base.onAniEnd();
        }
    }
}