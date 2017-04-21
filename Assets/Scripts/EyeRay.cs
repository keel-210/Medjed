using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRay : MonoBehaviour {
    [SerializeField]
    LayerMask Layermask;
    [SerializeField, Tag]
    string enemyTag;
    [SerializeField]
    Object StartParticle;
    [SerializeField]
    Object EndParticle;
    [SerializeField]
    float DamegeRatio;
    
    public Vector3 Offset { get; set; }

    LineRenderer lr;
    GameObject StartPar, EndPar;
    void Start ()
    {
        lr = GetComponent<LineRenderer>();
        if (StartParticle)
        {
            StartPar = (GameObject)Instantiate(StartParticle, transform.position + Offset, Quaternion.identity);
            StartPar.transform.position = transform.position + Offset;
            StartPar.transform.parent = transform;
        }
        if (EndParticle)
        {
            EndPar = (GameObject)Instantiate(EndParticle, transform.position + Offset, Quaternion.identity);
            EndPar.transform.position = transform.position + Offset;
            EndPar.transform.parent = transform;
        }
    }
	
	public GameObject Emit(Vector3 Direction)
    {
        GameObject hitGO = null;
        RaycastHit2D hit;
        lr.enabled = true;
        if (StartPar)
        {
            StartPar.SetActive(true);
            StartPar.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(Direction.y,Direction.x) * Mathf.Rad2Deg);
        }
        if (hit = Physics2D.Raycast(transform.position + Offset * transform.transform.localScale.x, Direction, 20, Layermask))
        {
            lr.SetPosition(0, transform.position + Offset);
            lr.SetPosition(1, hit.point);
            if (EndPar)
            {
                EndPar.SetActive(true);
                EndPar.transform.position = hit.point;
                StartPar.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
            }

            hitGO = hit.collider.gameObject;
            if(hit.collider.tag == enemyTag)
            {
                EnemyController ec = hit.collider.GetComponent<EnemyController>();
                ec.health.health -= Time.deltaTime * DamegeRatio;
            }
        }
        else
        {
            lr.SetPosition(0, transform.position + Offset);
            Vector3 end = transform.position + Offset + Direction * 15;
            end = new Vector3(end.x, end.y, 0);
            lr.SetPosition(1, end);
        }
        return hitGO;
    }
    public void Reset()
    {
        lr.enabled = false;
        if (StartPar)
        {
            StartPar.SetActive(false);
        }
        if (EndPar)
        {
            EndPar.SetActive(false);
        }
    }
}
