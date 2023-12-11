using UnityEngine;

namespace gedou
{
    public class Collosoon_State : StateBase
    {
        public Collosoon_State(Entity entity) : base(entity)
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
            }
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);
            
            Entity.transform.position = new Vector3(
                Entity.transform.position.x + Entity.MoveSpeedX*3 * Entity.transform.localScale.x,
                Entity.transform.position.y , 0);
            Entity.resetPos();
        }

        public override void onAniEnd()
        {
            base.onAniEnd();
            Entity.aniComponent.switchState(AniName.idle);
        }
    }
}