using System.Collections;
using System.Collections.Generic;
using System.Timers;
using gedou;
using Spine.Unity;
using UnityEngine;

public class BulletEff : MonoBehaviour
{
    public SkeletonAnimation eff = null;
    public bool canDmg = false;
    public int curDmgId = 0;
    public string effString = "";
    private float SpeedX = 0.3f;
    public int turn = 1;
    public float life = 0;
    public float startPosY = 0;
    void Start()
    {
        life = 1;
    }

    public void setState(string name)
    {
        bool isLoop = name == EffName.skill2;
        eff.state.SetAnimation(0,name,isLoop);
    }
    // Update is called once per frame
    void Update()
    {
        if (canDmg)
        {
           transform.position = new Vector3(
                transform.position.x + SpeedX *turn,
                transform.position.y , 0);
        }
        
        life -= Time.deltaTime;
        if (life <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
