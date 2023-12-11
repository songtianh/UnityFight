using UnityEngine;

namespace gedou
{
    public class Xuli_State : StateBase
    {
        public Xuli_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            isLoop = true;
            base.onEnter(aniName);
            Entity.resoveAtk();
            Entity.hitTimes = 0;
            xuliTime = 0;
        }
        private float xuliTime = 0;
        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            xuliTime += dt;
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
                    if (xuliTime > 0.5)
                    {
                        Entity.aniComponent.switchState(AniName.xuli2);
                    }
                    else
                    {
                        Entity.aniComponent.switchState(AniName.idle);
                    }
                    break;
            }
        }


        public override void onAniEnd()
        {
            base.onAniEnd();
        }
    }
}