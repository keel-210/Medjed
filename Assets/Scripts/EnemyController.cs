using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Health health { get; set; }

    Transform EnemyParent;
    protected void Init ()
    {
        EnemyParent = GameObject.Find("EnemyParent").transform;
        transform.parent = EnemyParent;
        health = GetComponent<Health>();
	}
}
