using UnityEngine;

namespace gedou
{
    public class Attack0_Back_State : StateBase
    {
        public Attack0_Back_State(Entity entity) : base(entity)
        {
            this.Entity = entity;
        }

        public override void onEnter(string aniName)
        {
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
                    if (Entity as Hero)
                    {
                        Debug.Log("触发1");
                        // switch (param)
                        // {
                        //     case KeyCode.J:
                        //         if (Entity.keySet.Contains(KeyCode.W))
                        //         {
                        //             Entity.aniComponent.switchState(AniName.skill1);
                        //         }
                        //         else if (Entity.keySet.Contains(KeyCode.S))
                        //         {
                        //             Entity.aniComponent.switchState(AniName.skill2);
                        //         }
                        //         else if (Entity.keySet.Contains(KeyCode.D))
                        //         {
                        //             Entity.aniComponent.switchState(AniName.skill3);
                        //         }
                        //         else if (Entity.keySet.Contains(KeyCode.A))
                        //         {
                        //             Entity.aniComponent.switchState(AniName.xuli1);
                        //         }
                        //
                        //         break;
                        // }
                    }

                    break;
                case StateEventType.keyUp:
                    if (Entity as Hero)
                    {
                        switch (param)
                        {
                            case KeyCode.J:
                                Entity.aniComponent.switchState(AniName.attack1);
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
                    }

                    break;
            }
        }

        public override void onAniEnd()
        {
            base.onAniEnd();
            Entity.aniComponent.switchState(AniName.idle);
        }
    }
}