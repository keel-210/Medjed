using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField]
    float DestroyTime;

    void Start()
    {
        StartCoroutine(this.DelayMethod(DestroyTime, () =>
         {
             Destroy(this.gameObject);
         }));
    }
}
