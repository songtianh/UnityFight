using UnityEngine;

namespace gedou
{
    public class Defense_State : StateBase
    {
        public Defense_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            base.onEnter(aniName);
            Entity.resoveAtk();
            Entity.hitTimes = 0;
            Entity.Jitui_Vel = 0;
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            if (Entity.Jitui_Vel <= 0)
            {
                Entity.Jitui_Vel = 0;
            }
            Entity.transform.position = new Vector3(Entity.transform.position.x + Entity.AtkTurn*Entity.Jitui_Vel,
                Entity.transform.position.y , 0);
            Entity.Jitui_Vel -= Entity.JituiVeol;
            Entity.resetPos();
        }
        public override void onEvent(int type, object param)
        {
            base.onEvent(type, param);
            switch ((StateEventType) type)
            {
                case StateEventType.hit:
                case StateEventType.hitJump:
                    Entity.Jitui_Vel = 0.04f;
                    break;
                case StateEventType.keyDown:
                    switch (param) {
                        case KeyCode.W:
                            Entity.aniComponent.switchState(AniName.skill1);
                            break;
                        case KeyCode.S:
                            Entity.aniComponent.switchState(AniName.skill2);
                            break;
                        case KeyCode.D:
                            Entity.aniComponent.switchState(AniName.skill3);
                            break;
                        case KeyCode.A:
                            Entity.aniComponent.switchState(AniName.xuli1);
                            break;
                    }
                    break;
                case StateEventType.keyUp:
                    switch (param)
                    {
                        case KeyCode.J:
                            Entity.aniComponent.switchState(AniName.defenseback);
                            break;
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