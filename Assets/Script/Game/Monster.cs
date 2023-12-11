using System.Collections;
using System.Collections.Generic;
using gedou;
using UnityEngine;

public class Monster : Entity
{
    public MonsterType monsterType = MonsterType.gbl;

    // public  Monster(EntityType i):base(i)
    // {
    //     EntityType = i;
    // }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other as CircleCollider2D)
        {
            BulletEff bulletEff = other.GetComponent<BulletEff>();
            if (bulletEff != null)
            {
                if (canBulletAtkSelf(bulletEff))
                {
                    aniComponent.onEvent((int) StateEventType.hit, bulletEff.curDmgId);
                    ClientEvent.dispatchEvent(CusEventType.ShowBulletEff, new object[] {this, bulletEff});
                    return;
                }
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

            Jump_Vel = 0.08f;
            if (atker.aniComponent.stateString == AniName.attack1 && atker.heroType == HeroType.Gedou)
            {
                Jump_Vel = 0.1f;
                aniComponent.onEvent((int) StateEventType.hitJump, atker.curDmgId);
            }
            else if (atker.aniComponent.stateString == AniName.attack2 && atker.heroType == HeroType.Cike)
            {
                Jump_Vel = 0.1f;
                aniComponent.onEvent((int) StateEventType.hitJump, atker.curDmgId);
            }
            else if (atker.aniComponent.stateString == AniName.skill1)
            {
                aniComponent.onEvent((int) StateEventType.hitJump, atker.curDmgId);
                if (atker.heroType == HeroType.Cike)
                {
                    Jump_Vel = 0.1f;
                }
                else
                {
                    Jump_Vel = 0.08f;
                }
            }
            else if (atker.aniComponent.stateString == AniName.xuli2)
            {
                Jitui_Vel = 0.4f;
                Jump_Vel = 0.1f;
                aniComponent.onEvent((int) StateEventType.hitJump, atker.curDmgId);
            }
            else if (atker.aniComponent.stateString == AniName.skill2)
            {
                if (atker.heroType == HeroType.Cike)
                {
                    Jitui_Vel = 0.4f;
                }
                else
                {
                    Jitui_Vel = 0.4f;
                }

                Jump_Vel = 0.1f;
                aniComponent.onEvent((int) StateEventType.hitJump, atker.curDmgId);
            }
            else
            {
                Jitui_Vel = 0f;
                aniComponent.onEvent((int) StateEventType.hit, atker.curDmgId);
            }

            ClientEvent.dispatchEvent(CusEventType.ShowEff, new object[] {atker, this});
        }
    }
}