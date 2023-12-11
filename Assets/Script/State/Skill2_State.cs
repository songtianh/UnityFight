using UnityEngine;

namespace gedou
{
    public class Skill2_State : StateBase
    {
        public Skill2_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            timeScale = 2;
            base.onEnter(aniName);
            Entity.resoveAtk();
            Entity.hitTimes = 0;
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
            Entity.aniComponent.switchState(AniName.idle);
            ClientEvent.dispatchEvent(CusEventType.Shoot,new object[]{Entity,Entity.DmgId++});
        }
    }
}