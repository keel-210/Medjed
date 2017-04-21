using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class WaveManager : MonoBehaviour
{
    [SerializeField]
    bool CameraFix;
    [SerializeField]
    Chaser4Camera c4c;
    [SerializeField]
    float LerpRatio;
    [SerializeField]
    Vector3 CamPos;

    PrefabListEmitter ple;
    Transform EnemyParent;
    bool activate;
    void Start()
    {
        ple = GetComponent<PrefabListEmitter>();
        GameObject obj = GameObject.Find("EnemyParent");
        EnemyParent = obj.transform;
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (CameraFix)
        {
            c4c.Fix(CamPos, LerpRatio);
        }
        activate = true;
        ple.Set();
    }
    void Update()
    {
        if (activate)
        {
            ple.Emit();
        }
        if (ple.list.Count == 0 && EnemyParent.childCount == 0)
        {
            if (CameraFix)
            {
                c4c.UnFix();
            }
            Destroy(gameObject);
        }
    }
}
