using System;
using System.Timers;
using Spine.Unity;

namespace gedou
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public enum EntityType
    {
        none,
        hero,
        monster
    }
    public class Entity : MonoBehaviour
    {
        
        public Entity Target = null;
        public static int DmgId = 0;
        public int curDmgId = 0;
        public List<int> sufDmg = new List<int>();

        public bool isPlayer = false;

        public ControlType controlType = ControlType.keyboard;

        // public Animator ani = null;
        public AnimatorComponent aniComponent = null;
        public SkeletonAnimation sa = null;
        public Animator sa1 = null;
        public HashSet<KeyCode> keySet = new HashSet<KeyCode>();
        public int hitTimes = 0; //受到攻击的次数
        private bool canDmg = false;
        public float[] moveDict = new[] {0f, 0f, 0f, 0f};
        public const float MoveSpeedX = 0.1f;
        public const float MoveSpeedY = 0.06f;
        public float recoverTime = 0;
        public const float recoverMax = 0.5f;
        public float Jump_Vel = 0;
        public float Jitui_Vel = 0;
        public bool isRight = true;
        public int AtkTurn = 1;

        //跳跃高度
        public const float RoleJump = 0.3f;
        //水平摩擦力
        public const float JituiVeol = 0.002f;
        //重力
        public const float JumpVeol = 0.015f;
        //击飞时重力
        public const float DownVeol = 0.01f;
        //临时击飞增加重力   连击数越多增加的越多
        public float tempDownVeol = 0f;
        //连击数
        public int HitNum = 0;
        //当前是否能反弹
        public bool CanTan = true;
        //倒地后得起立时间  被连击数越多起立越快
        public float standTime = 0.6f;
        //倒地后得最大起立时间
        public float stand_Max_Time = 0.6f;
        public float startPosY = 0;
        Vector3 minPos = new Vector3(-8, -4, 0);
        Vector3 maxPos = new Vector3(25, -1.5f, 0);
        public EntityType entityType = EntityType.none;
        // public Entity(EntityType i)
        // {
        //     EntityType = i;
        // }
        // Start is called before the first frame update
        private void Start()
        {
            Init();
        }

        public virtual  void Init()
        {
            aniComponent = new AnimatorComponent();
            aniComponent.regState(AniName.idle, new Idle_State(this));
            aniComponent.regState(AniName.move, new Move_State(this));
            aniComponent.regState(AniName.attack0, new Attack0_State(this));
            aniComponent.regState(AniName.attackback0, new Attack0_Back_State(this));
            aniComponent.regState(AniName.hit, new HitStand_State(this));
            aniComponent.regState(AniName.hit3, new HitJump_State(this));
            aniComponent.regState(AniName.hit4, new HitJump_Back_State(this));
            // AnimatorStateInfo a = ani.GetCurrentAnimatorStateInfo(0);
            aniComponent.switchState(AniName.idle);
            if (sa != null)
            {
                sa.state.Complete += aniComponent.onAniEnd;
            }
            else
            {
            }
        }

        public void SetAni(string aniName, bool isLoop, float timeScale)
        {
            if (sa != null)
            {
                sa.state.SetAnimation(0, aniName, isLoop);
                sa.timeScale = timeScale;
                
            }
            else
            {
                sa1.SetTrigger(aniName);
            }
        }

        private bool IsReadyExit = false;
        // Update is called once per frame
        void Update()
        {
            if (sa1 != null)
            {
                var animatorInfo = sa1.GetCurrentAnimatorStateInfo (0);
                if (!IsReadyExit && animatorInfo.normalizedTime <= 0.5f)
                {
                    IsReadyExit = true;
                }
                if (animatorInfo.normalizedTime >= 0.95f )//normalizedTime: 范围0 -- 1, 0是动作开始，1是动作结束
                {
                    aniComponent.onAniEnd(null);
                }
            }
            onMoveTurn();
            onKeyBoard();
            if (aniComponent != null)
            {
                aniComponent.onUpdate(Time.deltaTime);
            }
        }

        public void BeAtk()
        {
            canDmg = true;
            curDmgId = DmgId++;
        }

        public void resoveAtk()
        {
            canDmg = false;
            curDmgId = -1;
        }

        public bool canAtkSelf(Entity other)
        {
            bool CanAtk = other.canDmg;
            float selfY = 0;
            float otherY = 0;
            if (aniComponent == null || other.aniComponent == null) return false;
            if (aniComponent.stateString == AniName.jump || aniComponent.stateString == AniName.jumpattack
                                                         || aniComponent.stateString == AniName.hit3)
            {
                selfY = startPosY;
            }
            else
            {
                selfY = transform.position.y;
            }

            if (other.aniComponent.stateString == AniName.jump || other.aniComponent.stateString == AniName.jumpattack
                                                               || aniComponent.stateString == AniName.hit3)
            {
                otherY = other.startPosY;
            }
            else
            {
                otherY = other.transform.position.y;
            }

            if (Math.Abs(selfY - otherY) > 0.5f)
            {
                CanAtk = false;
            }

            if (other.curDmgId == -1)
            {
                CanAtk = false;
            }

            if (sufDmg.Contains(other.curDmgId))
            {
                CanAtk = false;
            }

            return CanAtk;
        }

        public bool canBulletAtkSelf(BulletEff other)
        {
            bool CanAtk = other.canDmg;
            float selfY = 0;
            float otherY = 0;
            if (aniComponent.stateString == AniName.jump || aniComponent.stateString == AniName.jumpattack
                                                         || aniComponent.stateString == AniName.hit3)
            {
                selfY = startPosY;
            }
            else
            {
                selfY = transform.position.y;
            }

            otherY = other.startPosY;
            if (Math.Abs(selfY - otherY) > 0.5f)
            {
                CanAtk = false;
            }

            if (other.curDmgId == -1)
            {
                CanAtk = false;
            }

            if (sufDmg.Contains(other.curDmgId))
            {
                CanAtk = false;
            }

            return CanAtk;
        }

        void onKeyBoard()
        {
            switch (controlType)
            {
                case ControlType.keyboard:
                    if (Input.anyKey)
                    {
                        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                        {
                            if (Input.GetKeyDown(keyCode))
                            {
                                this.aniComponent.onEvent((int) StateEventType.keyDown, keyCode);
                            }
                        }
                    }

                    KeyCode[] k = new[]
                    {
                        KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.J, KeyCode.K, KeyCode.Space, KeyCode.L
                        , KeyCode.U, KeyCode.I, KeyCode.O
                    };
                    foreach (KeyCode keyCode in k)
                    {
                        if (Input.GetKeyUp(keyCode))
                        {
                            this.aniComponent.onEvent((int) StateEventType.keyUp, keyCode);
                        }
                    }

                    break;
            }
        }

        void onMoveTurn()
        {
            switch (controlType)
            {
                case ControlType.keyboard:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        moveDict[0] = 1;
                    }

                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        moveDict[1] = 1;
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        moveDict[2] = 1;
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        moveDict[3] = 1;
                    }

                    if (Input.GetKeyUp(KeyCode.W))
                    {
                        moveDict[0] = 0;
                    }

                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        moveDict[1] = 0;
                    }

                    if (Input.GetKeyUp(KeyCode.D))
                    {
                        moveDict[2] = 0;
                    }

                    if (Input.GetKeyUp(KeyCode.A))
                    {
                        moveDict[3] = 0;
                    }

                    break;
                case ControlType.Ai:

                    break;
            }
        }

        public void resetPos(int type = -1)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            if (x > maxPos.x)
            {
                x = maxPos.x;
            }

            if (x < minPos.x)
            {
                x = minPos.x;
            }

            if (type == -1)
            {
                if (y > maxPos.y)
                {
                    y = maxPos.y;
                }

                if (y < minPos.y)
                {
                    y = minPos.y;
                }
            }

            transform.position = new Vector3(x, y);
        }
    }
}