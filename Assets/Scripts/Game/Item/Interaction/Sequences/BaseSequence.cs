using System.Collections;
using UnityEngine;

namespace Game.Sequence
{
    public abstract class BaseSequence : MonoBehaviour
    {
        public abstract IEnumerator Sequence();
        public abstract float TotalTime { get; }
    }
}