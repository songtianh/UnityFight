using UnityEngine;

namespace gedou
{
    public class HitJump_BackIdle_State : StateBase
    {
        public HitJump_BackIdle_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            base.onEnter(aniName);
            Entity.resoveAtk();
        }

        public override void onAniEnd()
        {
            base.onAniEnd();
            Entity.aniComponent.switchState(AniName.idle);
        }
        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            
            Entity.resetPos(0);
        }
    }
}