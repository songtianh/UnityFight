using UnityEngine;

namespace gedou
{
    public class Idle_State : StateBase
    {
        float powerTime = 0;

        public Idle_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            isLoop = true;
            base.onEnter(aniName);
            Entity.sufDmg.Clear();
            Entity.resoveAtk();
            Entity.hitTimes = 0;
            Entity.HitNum = 0;
            Entity.tempDownVeol = 0;
            powerTime = 0;
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
                    }
                    break;
                case StateEventType.keyUp:
                    switch (param)
                    {
                        case KeyCode.J:
                            Entity.aniComponent.switchState(AniName.attack0);
                            break;
                        case KeyCode.K:
                            Entity.aniComponent.switchState(AniName.jump);
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

        public override void onExit()
        {
            base.onExit();
            Entity.startPosY = Entity.transform.position.y;
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            float a = (Entity.moveDict[0] - Entity.moveDict[1]);
            float b = (Entity.moveDict[2] - Entity.moveDict[3]);
            if (a != 0 || b != 0)
            {
                Entity.aniComponent.switchState(AniName.move);
            }

            if (Entity.keySet.Contains(KeyCode.J))
            {
                this.powerTime += dt;
                if (this.powerTime > 0.2f)
                {
                    Entity.aniComponent.switchState(AniName.defense);
                }
            }
            else
            {
                this.powerTime = 0;
            }
        }
    }
}