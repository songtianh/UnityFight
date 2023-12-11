using UnityEngine;

namespace gedou
{
    public class HitJump_Back_State : StateBase
    {
        public HitJump_Back_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            base.onEnter(aniName);
            Entity.resoveAtk();
            float tempTime = Entity.HitNum * 0.0002f;
            Entity.standTime = Entity.stand_Max_Time - tempTime;
            Entity.standTime = Entity.standTime > 0.1f ? Entity.standTime : 0.1f;
        }

        public override void onAniEnd()
        {
            base.onAniEnd();
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);

            Entity.resetPos(0);
            Entity.standTime -= dt;
            if (Entity.standTime <= 0)
            {
                Entity.standTime = Entity.stand_Max_Time;
                Entity.aniComponent.switchState(AniName.hit2back);
            }
        }
    }
}