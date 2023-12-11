using System;
using gedou;
using UnityEngine;
public class ClientEvent : MonoBehaviour
{
    public delegate void ShowEff(Entity Atkhero,Entity Defhero);
    public static ShowEff showEffCall;
    public delegate void showTxt(int type, Vector3 pos, float value);
    public static showTxt showTxtCall;

    public delegate void Shoot(Hero AtkHero,int dmgId);
    public static Shoot shootCall;
    public delegate void ShowBulletEff(Hero Defhero,BulletEff eff);
    public static ShowBulletEff showBulletEff;
    public static void dispatchEvent(CusEventType type, object[] prop = null)
    {
        if (type == CusEventType.ShowEff)
        {
            if (showEffCall != null)
            {
                Entity Atkhero = (Entity)prop[0];
                Entity Defhero = (Entity)prop[1];
                showEffCall(Atkhero,Defhero);
            }
        }
        else if (type == CusEventType.SHOW_TXT)
        {

            if (showTxtCall != null)
            {
                int p1 = (int)prop[0];
                Vector3 p2 = (Vector3)prop[1];
                float p3 = (float)prop[2];
                showTxtCall(p1, p2, p3);
            }
        }else if (type == CusEventType.Shoot)
        {
            if (shootCall != null)
            {
                Hero Atkhero = (Hero)prop[0];
                int dmgId = (int)prop[1];
                shootCall(Atkhero,dmgId);
            }
        }else if (type == CusEventType.ShowBulletEff)
        {
            if (showBulletEff != null)
            {
                Hero defhero = (Hero)prop[0];
                BulletEff eff = (BulletEff)prop[1];
                showBulletEff(defhero,eff);
            }
        }
    }
}