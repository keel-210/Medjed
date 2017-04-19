using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZakoEnemy : EnemyController
{
    [SerializeField]
    float Speed;
    [SerializeField]
    float SlipTime;

    GameObject player;
    Transform ptf;
    Collider2D col;
    Collider2D AtCol;
    Rigidbody2D rb;
    float dir, dirCash;
    bool deathflg = true;
    float dirTimer;
    void Start ()
    {
        base.Init();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        AtCol = transform.GetChild(0).GetComponent<Collider2D>();
        player = GameObject.Find("Player");
        ptf = player.transform;
        dir = 1;
        dirTimer = 0;
    }
	
	void FixedUpdate ()
    {
        if (deathflg)
        {
            if (ptf.position.x - transform.position.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            if(Mathf.Abs(ptf.position.x - transform.position.x) > 20)
            {
                Destroy(gameObject);
            }
            if (dirCash != dir)
            {
                dirTimer += Time.deltaTime;
                if (dirTimer > SlipTime)
                {
                    dirTimer = 0;
                    dirCash = dir;
                }
            }
            transform.localScale = new Vector3(-dir, 1, 1);
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(dirCash * Speed, 0), 0.05f);
        }
        if (health.health <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.rotation += 10f;
            if (deathflg)
            {
                rb.velocity += new Vector2(0, 10);
                rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y);
                col.enabled = false;
                AtCol.enabled = false;
                deathflg = false;
                StartCoroutine(this.DelayMethod(3f, () =>
                {
                    Destroy(gameObject);
                }));
            }
        }
	}
}
