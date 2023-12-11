using UnityEngine;

namespace gedou
{
    public class Skill1_State : StateBase
    {
        public Skill1_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            if (Entity as Hero)
            {
                if ((Entity as Hero).heroType == HeroType.Gedou)
                {
                    timeScale = 2;
                }
            }
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
                case StateEventType.keyDown:
                    switch (param)
                    {
                        case KeyCode.L:
                            Entity.aniComponent.switchState(AniName.collision);
                            break;
                        case KeyCode.U:
                            // Entity.aniComponent.switchState(AniName.skill1);
                            break;
                        case KeyCode.I:
                            Entity.aniComponent.switchState(AniName.skill2);
                            break;
                        case KeyCode.O:
                            Entity.aniComponent.switchState(AniName.skill3);
                            break;
                    }
                    break;
            }
        }


        public float DmgTime = 0;
        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            this.DmgTime += dt;
            if (this.DmgTime > 0.1) {
                this.DmgTime = 0;
                Entity.BeAtk();
            }
        }
        public override void onAniEnd()
        {
            base.onAniEnd();
            Entity.aniComponent.switchState(AniName.idle);
        }
    }
}