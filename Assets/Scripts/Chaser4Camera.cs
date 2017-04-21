using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser4Camera : MonoBehaviour
{
    [SerializeField]
    Transform ChaseObj;
    [SerializeField]
    float Max_x;
    [SerializeField]
    float Min_x;
    [SerializeField]
    float Max_y;
    [SerializeField]
    float Min_y;
    [SerializeField]
    float ChaserRatio;
    [SerializeField]
    float OffsetY;

    bool Fixed;
    Vector3 FixedPosition;
    float Ratio4Lerp;

    public void Fix(Vector3 Pos,float LerpRatio)
    {
        Fixed = true;
        FixedPosition = Pos;
        Ratio4Lerp = LerpRatio;
    }
    public void UnFix()
    {
        Fixed = false;
    }
    void FixedUpdate()
    {
        if (Fixed)
        {
            transform.position = Vector3.Lerp(transform.position,FixedPosition,Ratio4Lerp);
        }
        else
        {
            Vector3 NextPos = Vector3.Lerp(transform.position + new Vector3(0, OffsetY, 0), ChaseObj.position, ChaserRatio);
            if (NextPos.x <= Max_x && NextPos.x >= Min_x)
            {
                transform.position = new Vector3(NextPos.x, transform.position.y, transform.position.z);
            }
            else if (NextPos.x > Max_x)
            {
                transform.position = new Vector3(Max_x, transform.position.y, transform.position.z);
            }
            else if (NextPos.x < Min_x)
            {
                transform.position = new Vector3(Min_x, transform.position.y, transform.position.z);
            }
            if (NextPos.y <= Max_y && NextPos.y >= Min_y)
            {
                transform.position = new Vector3(transform.position.x, NextPos.y, transform.position.z);
            }
            else if (NextPos.y > Max_y)
            {
                transform.position = new Vector3(transform.position.x, Max_y, transform.position.z);
            }
            else if (NextPos.y < Min_y)
            {
                transform.position = new Vector3(transform.position.x, Min_y, transform.position.z);
            }
        }
    }
}
