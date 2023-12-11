using UnityEngine;

namespace gedou
{
    public class Defense_Back_State : StateBase
    {
        public Defense_Back_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
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
                case StateEventType.keyUp:
                    switch (param)
                    {
                    }
                    break;
            }
        }

        public override void onAniEnd()
        {
            base.onAniEnd();
            Entity.aniComponent.switchState(AniName.idle);
        }
    }
}