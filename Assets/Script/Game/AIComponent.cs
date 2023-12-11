using System;
using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace gedou
{
    public class AIComponent : MonoBehaviour
    {
        private Entity owner = null;
        private Entity Target = null;
        public GameObject FightNode = null;
        public int AiLevel = 0;
        public const float XCup = 2.5f;
        public const float YCup = 0.5f;
        private float AiTime = 0.1f;
        private float curAiTime = 0;
        private bool inAtkCap = false;

        private void Start()
        {
            owner = GetComponent<Entity>();
            FightNode = owner.transform.parent.gameObject;
        }

        Entity FindTarget()
        {
            foreach (Transform child in FightNode.transform)
            {
                Hero tar = child.GetComponent<Hero>();
                if (tar)
                {
                    if (tar.isPlayer != owner.isPlayer)
                    {
                        if (Target)
                        {
                            float dis = (Target.transform.position - owner.transform.position).magnitude;
                            float dis1 = (tar.transform.position - owner.transform.position).magnitude;
                            if (dis1 < dis)
                            {
                                Target = tar;
                            }
                        }
                        else
                        {
                            Target = tar;
                        }
                    }
                }
            }

            return Target;
        }

        private void Update()
        {
            if (owner.controlType != ControlType.Ai) return;
            AiMove();
            curAiTime += Time.deltaTime;
            if (curAiTime >= AiTime)
            {
                curAiTime = 0;
                int ran = Random.Range(0, 10);
                if (Target)
                {
                    if (ran > 7)
                    {
                        FindTarget();
                    }
                    else
                    {
                        AiAction();
                    }
                }
                else
                {
                    FindTarget();
                }
            }
        }

        void AiAction()
        {
            if (inAtkCap)
            {
                owner.aniComponent.onEvent((int) StateEventType.keyUp, KeyCode.J);
            }
            else
            {
            }
        }

        void AiMove()
        {
            inAtkCap = true;
            if (Target)
            {
                Vector3 selfPos = owner.transform.position;
                Vector3 otherPos = Target.transform.position;
                if (Math.Abs(selfPos.x - otherPos.x) > XCup)
                {
                    if (selfPos.x < otherPos.x)
                    {
                        owner.moveDict[2] = 1;
                        owner.moveDict[3] = 0;
                    }
                    else
                    {
                        owner.moveDict[2] = 0;
                        owner.moveDict[3] = 1;
                    }

                    inAtkCap = false;
                }
                else
                {
                    owner.moveDict[2] = 0;
                    owner.moveDict[3] = 0;
                }

                if (Math.Abs(owner.startPosY - Target.startPosY) > YCup)
                {
                    if (owner.startPosY < Target.startPosY)
                    {
                        owner.moveDict[0] = 1;
                        owner.moveDict[1] = 0;
                    }
                    else
                    {
                        owner.moveDict[0] = 0;
                        owner.moveDict[1] = 1;
                    }

                    inAtkCap = false;
                }
                else
                {
                    owner.moveDict[0] = 0;
                    owner.moveDict[1] = 0;
                }
            }
            else
            {
                inAtkCap = false;
            }
        }

        void RandMove()
        {
            
        }
    }
}