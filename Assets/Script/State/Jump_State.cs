using UnityEngine;

namespace gedou
{
    public class Jump_State : StateBase
    {
        public Jump_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            base.onEnter(aniName);
            Entity.resoveAtk();
            Entity.Jump_Vel = Entity.RoleJump;
            Entity.hitTimes = 0;
        }

        public override void onEvent(int type, object param)
        {
            base.onEvent(type, param);
            switch ((StateEventType) type)
            {
                case StateEventType.hit:
                    Entity.aniComponent.switchState(AniName.hit3);
                    break;
                case StateEventType.hitJump:
                    Entity.aniComponent.switchState(AniName.hit3);
                    
                    break;
                case StateEventType.keyUp:
                    switch (param)
                    {
                        case KeyCode.J:
                            Entity.aniComponent.switchState(AniName.jumpattack);
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
            Entity.Jump_Vel -= Entity.JumpVeol;
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