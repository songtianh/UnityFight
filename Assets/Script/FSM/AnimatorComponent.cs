using System.Collections.Generic;
using UnityEngine;

namespace gedou
{
    public class AnimatorComponent
    {
        private Dictionary<string, StateBase> _mapState = new Dictionary<string, StateBase>();
        public StateBase curState;
        public string stateString;

        public void regState(string key, StateBase state)
        {
            if ("" == key)
            {
                return;
            }

            if (null == key)
            {
                return;
            }

            if (this._mapState.ContainsKey(key))
            {
                return;
            }

            this._mapState.Add(key, state);
        }

        void delState(string key)
        {
            this._mapState.Remove(key);
        }

        public void switchState(string key)
        {
            if ("" == key)
            {
                return;
            }

            if (null == key)
            {
                return;
            }

            if (!_mapState.ContainsKey(key))
            {
                return;
            }

            // Debug.LogError("切换状态："+key+"上一个状态为："+stateString);
            if (this.curState != null)
            {
                if (stateString == key)
                {
                    if (key == AniName.idle)
                    {
                        return;
                    }
                }

                this.curState.onExit();
            }

            if (curState?.Entity.sa1 != null)
            {
                curState.Entity.sa1.ResetTrigger(stateString);
            }
            stateString = key;
            _mapState.TryGetValue(key, out curState);

            // curState.Entity.sa.skeleton.SetSlotsToSetupPose();
            curState?.onEnter(key);
        }

        public void onUpdate(float dt)
        {
            if (this.curState != null)
            {
                this.curState.onUpdate(dt);
                // if (this.curState.Entity.ani.GetCurrentAnimatorStateInfo(0).IsName(stateString))
                // {
                //     this.curState.onAniEnd();
                // } 
            }
        }

        public void onEvent(int type, object param = null)
        {
            switch ((StateEventType) type)
            {
                case StateEventType.keyDown:
                    curState.Entity.keySet.Add((KeyCode) param);
                    break;
                case StateEventType.keyUp:
                    curState.Entity.keySet.Remove((KeyCode) param);
                    break;
                case StateEventType.hit:
                    curState.Entity.sufDmg.Add((int) param);
                    break;
                case StateEventType.hitJump:
                    curState.Entity.sufDmg.Add((int) param);
                    break;
            }

            curState.onEvent(type, param);
        }

        public void onAniEnd(object any)
        {
            if (this.curState != null)
            {
                curState.onAniEnd();
            }
        }
    }
}