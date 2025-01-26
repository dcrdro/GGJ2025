using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Drawn : MonoBehaviour
{
    public Transform[] t;
    private Vector3[] pos;

    private void Awake()
    {
        pos = new Vector3[t.Length];
        for (int i = 0; i < t.Length; i++)
        {
            Transform tt = t[i];
            pos[i] = tt.transform.position;
}
    }

    [ContextMenu("D")]
    public void Drawnn()
    {
        for (int i = 0; i < t.Length; i++)
        {
            Transform tt = t[i];
            var j = i;
            tt.DOMoveY(pos[j].y - 0.3f, 2f).OnComplete( () => tt.position = pos[j]);

        }
    }
}
