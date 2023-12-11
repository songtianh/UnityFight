using System;
using UnityEngine;

namespace gedou
{
    public class HitJump_State : StateBase
    {
        public HitJump_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
            base.onEnter(aniName);
            Entity.resoveAtk();
            Entity.hitTimes++;
        }

        public override void onUpdate(float dt)
        {
            base.onUpdate(dt);

            if (Entity.Jitui_Vel <= 0)
            {
                Entity.Jitui_Vel = 0;
            }

            Entity.transform.position = new Vector3(
                Entity.transform.position.x + Entity.Jitui_Vel * Entity.AtkTurn,
                Entity.transform.position.y + Entity.Jump_Vel, 0);
            Entity.Jitui_Vel -=   Entity.JituiVeol;
            Entity.Jump_Vel -= (Entity.DownVeol + Entity.tempDownVeol);
            if (Entity.transform.position.y <= Entity.startPosY)
            {
                if (Entity.CanTan &&Entity.HitNum <= 30&& Math.Abs(Entity.Jump_Vel)>0.04f)
                {
                    Entity.tempDownVeol = 0;
                    Entity.CanTan = false;
                    Entity.Jump_Vel = 0.12f;
                }
                else
                {
                    Entity.aniComponent.switchState(AniName.hit4);
                }
            }
            Entity.resetPos(0);
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