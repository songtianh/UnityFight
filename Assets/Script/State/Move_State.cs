using UnityEngine;

namespace gedou
{
    public class Move_State : StateBase
    {
        float powerTime = 0;

        public Move_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            isLoop = true;
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
                case StateEventType.keyDown:
                    switch (param)
                    {
                        case KeyCode.K:
                        case KeyCode.Space:
                            Entity.aniComponent.switchState(AniName.jump);
                            break;
                    }
                    break;
                case StateEventType.keyUp:
                    switch (param)
                    {
                        case KeyCode.J:
                            Entity.aniComponent.switchState(AniName.attack0);
                            break;
                        case KeyCode.L:
                            Entity.aniComponent.switchState(AniName.collision);
                            break;
                        case KeyCode.U:
                            Entity.aniComponent.switchState(AniName.skill1);
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

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            float a = (Entity.moveDict[0] - Entity.moveDict[1]);
            float b = (Entity.moveDict[2] - Entity.moveDict[3]);
            if (a == 0 && b == 0) {
                Entity.aniComponent.switchState(AniName.idle);
                return;
            }
            
            Entity.transform.position = new Vector3(
                Entity.transform.position.x + Entity.MoveSpeedX * Entity.moveDict[2] -
                Entity.MoveSpeedX * Entity.moveDict[3],
                Entity.transform.position.y + Entity.MoveSpeedY * Entity.moveDict[0] -
                Entity.MoveSpeedY * Entity.moveDict[1], 0);
            if (Entity.moveDict[2] > 0) {
                Entity.transform.localScale =new Vector3(1, 1, 1);
            }
            if (Entity.moveDict[3] > 0) {
                Entity.transform.localScale = new Vector3(-1, 1, 1);
            }
            Entity.startPosY = Entity.transform.position.y;
            Entity.resetPos();
        }
        public override void onExit()
        {
            base.onExit();
            Entity.startPosY = Entity.transform.position.y;
        }
    }
}