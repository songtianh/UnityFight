using System.Collections;
using System.Collections.Generic;
using gedou;
using UnityEngine;
using UnityEngine.Rendering;

public class Main : MonoBehaviour
{
    public GameObject friendLaya = null;
    public GameObject enemyLaya = null;
    public GameObject cike = null;
    public GameObject gedou = null;
    public GameObject gbl = null;
    public GameObject cikeEff = null;
    public GameObject gedouEff = null;
    public Transform FightScene = null;
    public List<Hero> Heroes = new List<Hero>();
    public GameObject Camera = null;
    public float minX = 0;
    public float maxX = 17.8f;
    public Hero player = null;

    void Start()
    {
        Application.targetFrameRate = 60;
        ClientEvent.showEffCall += ShowEff;
        ClientEvent.shootCall += Shoot;
        ClientEvent.showBulletEff += ShowBulletEff;
        Vector3 v = new Vector3();
        for (int i = 0; i < 2; i++)
        {
            ControlType controlType = i == 0 ? ControlType.keyboard : ControlType.Ai;
            bool isPlayer = i % 2 == 0;
            // isPlayer= i == 0;
            HeroType heroType = Random.Range(0, 2) == 1 ? HeroType.Cike : HeroType.Gedou;
            // v = new Vector3(Random.Range(-9, 27), Random.Range(-4, -1), 0);
            v = new Vector3(5+2*i, -2, 0);
            Hero hero = addRole(isPlayer, controlType, heroType, v);
            Heroes.Add(hero);
            if (i == 0)
            {
                player = hero;
            }
        }

        
        // addMonster(false, MonsterType.gbl, new Vector3(Random.Range(-9, 27), Random.Range(-4, -1), 0));
    }

    Hero addRole(bool isPlayer, ControlType controlType, HeroType heroType, Vector3 pos)
    {
        Hero role = null;
        GameObject roleNode = null;
        if (heroType == HeroType.Cike)
        {
            roleNode = Instantiate(cike);
        }
        else
        {
            roleNode = Instantiate(gedou);
        }

        roleNode.transform.parent = FightScene;
        roleNode.transform.position = pos;
        role = roleNode.GetComponent<Hero>();
        role.heroType = heroType;
        role.isPlayer = isPlayer;
        role.controlType = controlType;
        if (isPlayer)
        {
            ChangeLayer(roleNode, friendLaya.layer);
        }
        else
        {
            ChangeLayer(roleNode, enemyLaya.layer);
        }
        
        return role;
    }

    Monster addMonster(bool isPlayer, MonsterType monsterType, Vector3 pos)
    {
        Monster role = null;
        GameObject roleNode = null;
        if (monsterType == MonsterType.gbl)
        {
            roleNode = Instantiate(gbl);
        }
        
        roleNode.transform.parent = FightScene;
        roleNode.transform.position = pos;
        role = roleNode.GetComponent<Monster>();
        role.monsterType = monsterType;
        role.isPlayer = isPlayer;
        role.controlType = ControlType.Touch;
        if (isPlayer)
        {
            ChangeLayer(roleNode, friendLaya.layer);
        }
        else
        {
            ChangeLayer(roleNode, enemyLaya.layer);
        }

        return role;
    }

    // Update is called once per frame
    void Update()
    {
        float curX = 0;
        Heroes.Sort((Hero hero1, Hero hero2) => { return (int) ((hero2.startPosY - hero1.startPosY) * 100); });

        for (int i = 0; i < Heroes.Count; i++)
        {
            Heroes[i].GetComponent<SortingGroup>().sortingOrder = i + 5;
        }

        if (player != null)
        {
            curX = player.transform.position.x;
            curX = curX > maxX ? maxX : curX;
            curX = curX < minX ? minX : curX;
            Camera.transform.position = new Vector3(curX, Camera.transform.position.y, -10);
        }
    }

    public void ShowBulletEff(Hero defHero, BulletEff BulletEff)
    {
        BulletEff cloneBulletEff = Instantiate(BulletEff);
        GameObject.Destroy(BulletEff.gameObject);
        cloneBulletEff.setState(EffName.skill2_hit);
        cloneBulletEff.transform.parent = defHero.transform;
        ChangeLayer(cloneBulletEff.gameObject, defHero.gameObject.layer);
        cloneBulletEff.transform.localPosition = new Vector3(0, 2f, 0);
        cloneBulletEff.canDmg = false;
    }

    public void ShowEff(Entity Atkhero, Entity Defhero)
    {
        GameObject EffNode = null;
        string effName = "";
        Hero curAtkHero = Atkhero as Hero;
        Hero defAtkHero = Defhero as Hero;

        if (Defhero.aniComponent.stateString == AniName.defense)
        {
            if (defAtkHero != null)
            {
                if (defAtkHero.heroType == HeroType.Gedou)
                {
                    EffNode = Instantiate(gedouEff);
                }
                else
                {
                    EffNode = Instantiate(cikeEff);
                }
            }

            effName = EffName.defense;
        }
        else if (Defhero.aniComponent.stateString == AniName.hit || Defhero.aniComponent.stateString == AniName.hit3)
        {
            if (curAtkHero != null)
            {
                if (curAtkHero.heroType == HeroType.Gedou)
                {
                    EffNode = Instantiate(gedouEff);
                }
                else
                {
                    EffNode = Instantiate(cikeEff);
                }

                switch (Atkhero.aniComponent.stateString)
                {
                    case AniName.skill1:
                        if (curAtkHero.heroType == HeroType.Gedou)
                        {
                            effName = EffName.atkHit;
                        }
                        else
                        {
                            effName = EffName.skill1_1hit;
                        }

                        break;
                    case AniName.skill2:
                        effName = EffName.skill1_2hit;
                        break;
                    case AniName.skill3:
                        effName = EffName.skill2_hit;
                        break;
                    default:
                        effName = EffName.atkHit;
                        break;
                }
            }
        }

        if (EffNode == null)
        {
            return;
        }

        BulletEff bulletEff = EffNode.GetComponent<BulletEff>();
        bulletEff.setState(effName);
        EffNode.transform.parent = Defhero.transform;
        ChangeLayer(EffNode, Defhero.gameObject.layer);
        EffNode.transform.localPosition = new Vector3(0, 2f, 0);
    }

    void ChangeLayer(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = layer;
        }
    }

    public void Shoot(Hero Atker, int id)
    {
        GameObject EffNode = null;
        if (Atker.heroType == HeroType.Gedou)
        {
            EffNode = Instantiate(gedouEff);
        }
        else
        {
            EffNode = Instantiate(cikeEff);
        }

        BulletEff bulletEff = EffNode.GetComponent<BulletEff>();
        bulletEff.life = 4;
        bulletEff.setState(EffName.skill2);
        EffNode.transform.parent = Atker.transform;
        ChangeLayer(EffNode, Atker.gameObject.layer);
        EffNode.transform.localPosition = new Vector3(0, 2f, 0);

        bulletEff.canDmg = true;
        bulletEff.curDmgId = id;
        bulletEff.turn = (int) Atker.gameObject.transform.localScale.x;
        bulletEff.startPosY = Atker.startPosY;
    }
}