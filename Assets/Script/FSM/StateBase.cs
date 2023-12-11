using UnityEngine.TextCore;

namespace gedou
{
    public class StateBase
    {
        public Entity Entity;
        public bool isLoop = false;
        public float timeScale = 1;
        public StateBase(Entity entity)
        {
            this.Entity = entity;
        }

        public virtual void onEnter(string aniName)
        {
            // Entity.ani.SetTrigger(AniName.jump);
            Entity.SetAni(aniName,isLoop,timeScale);
        }

        public virtual void onUpdate(float dt)
        {
        }

        public virtual void onEvent(int type, object param)
        {
            
        }

        public virtual void onExit()
        {
        }

        public virtual void onAniEnd()
        {
        }
    }
}