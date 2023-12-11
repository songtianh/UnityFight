using UnityEngine;

namespace gedou
{
    public class Attack0_State : StateBase
    {
        public Attack0_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            base.onEnter(aniName);
            Entity.BeAtk();
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
            Entity.aniComponent.switchState(AniName.attackback0);
        }
    }
}