using UnityEngine;
using UnityEngine.Events;

namespace gedou
{
    public class AnimCallBack:MonoBehaviour
    {
        public UnityAction callBack;
        public void MyCallBack() => callBack?.Invoke();
    }
}