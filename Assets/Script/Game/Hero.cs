using System;
using System.Collections.Generic;
using UnityEngine;

namespace gedou
{
    public class Hero : Entity
    {
        // public  Hero(EntityType i):base(i)
        // {
        //     EntityType = i;
        // }
        public HeroType heroType = HeroType.Gedou;
        public Dictionary<string, float> Atk_Jump;
        public Dictionary<string, float> Atk_Jitui;
        public Dictionary<string, bool> isHitJUmp;

        public override void Init()
        {
            base.Init();
            aniComponent.regState(AniName.attack1, new Attack1_State(this));
            aniComponent.regState(AniName.attackback1, new Attack1_Back_State(this));
            aniComponent.regState(AniName.attack2, new Attack2_State(this));
            aniComponent.regState(AniName.attackback2, new Attack2_Back_State(this));
            aniComponent.regState(AniName.jumpattack, new Jump_Attack_State(this));
            aniComponent.regState(AniName.jump, new Jump_State(this));
            // aniComponent.regState(AniName.hit1back, new HitStand_Back_State(this));
            aniComponent.regState(AniName.hit2back, new HitJump_BackIdle_State(this));
            aniComponent.regState(AniName.defense, new Defense_State(this));
            aniComponent.regState(AniName.defenseback, new Defense_Back_State(this));
            aniComponent.regState(AniName.skill1, new Skill0_State(this));
            aniComponent.regState(AniName.skill2, new Skill1_State(this));
            aniComponent.regState(AniName.skill3, new Skill2_State(this));
            aniComponent.regState(AniName.xuli1, new Xuli_State(this));
            aniComponent.regState(AniName.xuli2, new Xuli1_State(this));
            aniComponent.regState(AniName.collision, new Collosoon_State(this));
            InitAtkConfig();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other as CircleCollider2D)
            {
                BulletEff bulletEff = other.GetComponent<BulletEff>();
                if (bulletEff != null)
                {
                    AtkedByBullet(bulletEff);
                    return;
                }

                Hero atker = other.transform.parent.GetComponent<Hero>();
                if (atker == null)
                {
                    Monster monster = other.transform.parent.GetComponent<Monster>();
                    AtkedByMonster(monster);
                }
                else
                {
                    AtkedByHero(atker);
                }
            }
        }
        
        public void AtkedByMonster(Monster atker)
        {
            if (canAtkSelf(atker))
            {
                if (atker.transform.position.x > transform.position.x)
                {
                    AtkTurn = -1;
                }
                else
                {
                    AtkTurn = 1;
                }

                Jump_Vel = 0.08f;
                Jitui_Vel = 0f;
                aniComponent.onEvent((int) StateEventType.hit, atker.curDmgId);
                ClientEvent.dispatchEvent(CusEventType.ShowEff, new object[] {atker, this});
            }
        }

        public void AtkedByHero(Hero atker)
        {
            if (canAtkSelf(atker))
            {
                if (atker.transform.position.x > transform.position.x)
                {
                    AtkTurn = -1;
                }
                else
                {
                    AtkTurn = 1;
                }

                Atk_Jump.TryGetValue(atker.aniComponent.stateString, out Jump_Vel);
                Atk_Jitui.TryGetValue(atker.aniComponent.stateString, out Jitui_Vel);
                isHitJUmp.TryGetValue(atker.aniComponent.stateString, out bool isJump);
                tempDownVeol += 0.0001f;
                HitNum += 1;
                CanTan = true;
                aniComponent.onEvent(isJump?(int) StateEventType.hitJump:(int) StateEventType.hit, atker.curDmgId);
                ClientEvent.dispatchEvent(CusEventType.ShowEff, new object[] {atker, this});
            }
        }

        public void AtkedByBullet( BulletEff bulletEff)
        {
            if (canBulletAtkSelf(bulletEff))
            {
                if (bulletEff.transform.position.x > transform.position.x)
                {
                    AtkTurn = -1;
                }
                else
                {
                    AtkTurn = 1;
                }
                Atk_Jump.TryGetValue(AniName.skill3, out Jump_Vel);
                Atk_Jitui.TryGetValue(AniName.skill3, out Jitui_Vel);
                isHitJUmp.TryGetValue(AniName.skill3, out bool isJump);
                aniComponent.onEvent((int) StateEventType.hit, bulletEff.curDmgId);
                ClientEvent.dispatchEvent(CusEventType.ShowBulletEff, new object[] {this, bulletEff});
            }
        }
        void InitAtkConfig()
        {
            Atk_Jump = new Dictionary<string, float>();
            Atk_Jitui = new Dictionary<string, float>();
            isHitJUmp = new Dictionary<string, bool>();
            if (heroType == HeroType.Cike)
            {
                Atk_Jump.Add(AniName.attack0, 0.15f);
                Atk_Jump.Add(AniName.attack1, 0.15f);
                Atk_Jump.Add(AniName.attack2, 0.2f);
                Atk_Jump.Add(AniName.skill1, 0.15f);
                Atk_Jump.Add(AniName.skill2, 0.1f);
                Atk_Jump.Add(AniName.skill3, 0.15f);
                Atk_Jump.Add(AniName.xuli1, 0.1f);
                Atk_Jump.Add(AniName.xuli2, 0.1f);
                Atk_Jump.Add(AniName.jumpattack, 0.1f);
            }
            else
            {
                Atk_Jump.Add(AniName.attack0, 0.08f);
                Atk_Jump.Add(AniName.attack1, 0.15f);
                Atk_Jump.Add(AniName.attack2, 0.08f);
                Atk_Jump.Add(AniName.skill1, 0.1f);
                Atk_Jump.Add(AniName.skill2, 0.3f);
                Atk_Jump.Add(AniName.skill3, 0.1f);
                Atk_Jump.Add(AniName.xuli1, 0.1f);
                Atk_Jump.Add(AniName.xuli2, 0.1f);
                Atk_Jump.Add(AniName.jumpattack, 0.1f);
            }

            if (heroType == HeroType.Cike)
            {
                Atk_Jitui.Add(AniName.attack0, 0.03f);
                Atk_Jitui.Add(AniName.attack1, 0.03f);
                Atk_Jitui.Add(AniName.attack2, 0.06f);
                Atk_Jitui.Add(AniName.skill1, 0.01f);
                Atk_Jitui.Add(AniName.skill2, 0.06f);
                Atk_Jitui.Add(AniName.skill3, 0.03f);
                Atk_Jitui.Add(AniName.xuli1, 0.03f);
                Atk_Jitui.Add(AniName.xuli2, 0.03f);
                Atk_Jitui.Add(AniName.jumpattack, 0.03f);
            }
            else
            {
                Atk_Jitui.Add(AniName.attack0, 0f);
                Atk_Jitui.Add(AniName.attack1, 0f);
                Atk_Jitui.Add(AniName.attack2, 0f);
                Atk_Jitui.Add(AniName.skill1, 0f);
                Atk_Jitui.Add(AniName.skill2, 0f);
                Atk_Jitui.Add(AniName.skill3, 0f);
                Atk_Jitui.Add(AniName.xuli1, 0f);
                Atk_Jitui.Add(AniName.xuli2, 0f);
                Atk_Jitui.Add(AniName.jumpattack, 0f);
            }

            if (heroType == HeroType.Cike)
            {
                isHitJUmp.Add(AniName.attack0, false);
                isHitJUmp.Add(AniName.attack1, false);
                isHitJUmp.Add(AniName.attack2, true);
                isHitJUmp.Add(AniName.skill1, false);
                isHitJUmp.Add(AniName.skill2, true);
                isHitJUmp.Add(AniName.skill3, false);
                isHitJUmp.Add(AniName.xuli1, false);
                isHitJUmp.Add(AniName.xuli2, false);
                isHitJUmp.Add(AniName.jumpattack, false);
            }
            else
            {
                isHitJUmp.Add(AniName.attack0, false);
                isHitJUmp.Add(AniName.attack1, true);
                isHitJUmp.Add(AniName.attack2, false);
                isHitJUmp.Add(AniName.skill1, false);
                isHitJUmp.Add(AniName.skill2, true);
                isHitJUmp.Add(AniName.skill3, false);
                isHitJUmp.Add(AniName.xuli1, false);
                isHitJUmp.Add(AniName.xuli2, false);
                isHitJUmp.Add(AniName.jumpattack, false);
            }
        }
    }
}