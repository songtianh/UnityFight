using UnityEngine;

namespace gedou
{
    public class Jump_Attack_State : StateBase
    {
        public Jump_Attack_State(Entity entity) : base(entity)
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
                case StateEventType.keyDown:
                    switch (param)
                    {
                        case KeyCode.W:
                            Entity.moveDict[0] = 1;
                            break;
                        case KeyCode.S:
                            Entity.moveDict[1] = 1;
                            break;
                        case KeyCode.D:
                            Entity.moveDict[2] = 1;
                            break;
                        case KeyCode.A:
                            Entity.moveDict[3] = 1;
                            break;
                    }

                    break;
                case StateEventType.keyUp:
                    switch (param)
                    {
                        case KeyCode.W:
                            Entity.moveDict[0] = 0;
                            break;
                        case KeyCode.S:
                            Entity.moveDict[1] = 0;
                            break;
                        case KeyCode.D:
                            Entity.moveDict[2] = 0;
                            break;
                        case KeyCode.A:
                            Entity.moveDict[3] = 0;
                            break;
                    }

                    break;
            }
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            // Entity.node.position = v3(Entity.node.position.x,Entity.node.position.y+Entity.Jump_Vel,0);
            Entity.transform.position = new Vector3(
                Entity.transform.position.x + Entity.MoveSpeedX * Entity.moveDict[2] -
                Entity.MoveSpeedX * Entity.moveDict[3],
                Entity.transform.position.y + Entity.Jump_Vel, 0);
            Entity.Jump_Vel -= Entity.JumpVeol*2f;
            if (Entity.transform.position.y <= Entity.startPosY)
            {
                Entity.transform.position = new Vector3(
                    Entity.transform.position.x ,
                    Entity.startPosY, 0);
                Entity.aniComponent.switchState(AniName.idle);
            }
            Entity.resetPos(0);
        }
    }
}