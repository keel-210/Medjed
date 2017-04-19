using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    float _Damage;
    public float Damage
    {
        get { return _Damage; }
        set { _Damage = value; }
    }

}
