using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField,Range(0,10)]
    float Speed;
    [SerializeField,Range(0,20)]
    float JumpPower;
    [SerializeField, Tag]
    string Ground;
    [SerializeField,Tag]
    string EnemyAttackTag;
    [SerializeField]
    Vector3 Offset;
    [SerializeField]
    float InvincibleTime;
    [SerializeField]
    Collider2D HitCollider;

    Rigidbody2D rb;
    EyeRay er;
    Health health;
    bool Up, Down,Shot;
    float Move;
    Vector2 movement;
    Vector3 MousePos,MouseDir;
    bool OnGround, JumpStrict, InvincibleFlg;
    //constructer
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        er = GetComponent<EyeRay>();
        er.Offset = Offset;
        health = GetComponent<Health>();
	}
    //ForRigidbody
	void FixedUpdate()
    {
        movement = new Vector2(Move * Speed, rb.velocity.y);
        if(Move != 0)
        {
            rb.velocity = movement;
            transform.rotation = Quaternion.Euler(0, 90 * (Move - 1), 0);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Up && !Down && !JumpStrict)
        {
            rb.velocity += new Vector2(0, JumpPower);
            JumpStrict = true;
        }
        if (Down)
        {
            gameObject.layer = 11;
        }
        else
        {
            gameObject.layer = 9;
        }
        if (Shot)
        {
            GameObject hitGO;
            if (hitGO = er.Emit(MouseDir))
            {
            }
        }
        else
        {
            er.Reset();
        }
        if (InvincibleFlg)
        {
            HitCollider.enabled = false;
        }
        else
        {
            HitCollider.enabled = true;
        }
    }
    //ForInput
	void Update ()
    {
        Up = Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Jump") > 0;
        Down = Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Jump") < 0;
        Move = Input.GetAxisRaw("Horizontal");
        Shot = Input.GetAxisRaw("Fire1") > 0;
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseDir = MousePos - (transform.position + Offset);
        MouseDir = Vector3.Normalize(new Vector3(MouseDir.x, MouseDir.y, 0));
        Debug.DrawRay(transform.position + Offset, MouseDir * 50);
    }
    //ForOnGround
    void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.tag == Ground && rb.velocity.y <= 0)
        {
            OnGround = true;
            JumpStrict = false;
        }
    }
    void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == Ground)
        {
            OnGround = false;
        }
    }
    //ForDamege
    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.tag == EnemyAttackTag)
        {
            EnemyAttack ea = obj.GetComponent<EnemyAttack>();
            Debug.Log("hit" + ea.Damage);
            InvincibleFlg = true;
            StartCoroutine(this.DelayMethod(InvincibleTime, () =>
             {
                 InvincibleFlg = false;
             }));
            if (health.health <= 0)
            {
                GameOver();
            }
        }
    }
    void GameOver()
    {

    }
}
