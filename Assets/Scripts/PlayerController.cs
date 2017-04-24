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
    Dir FixState;
    enum Dir
    {
        None,Up,Down,Right,Left
    }
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
        Movement();
        ShotRay();
        Invincible();
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
        if (obj.gameObject.tag == Ground)
        {
            OnGround = true;
            JumpStrict = false;
            Vector2 contact = obj.contacts[0].point - new Vector2(transform.position.x, transform.position.y);
            if(Mathf.Abs(contact.x) > Mathf.Abs(contact.y))
            {
                if(contact.x > 0)
                {
                    FixState = Dir.Right;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    FixState = Dir.Left;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else
            {
                if (contact.y > 0)
                {
                    FixState = Dir.Up;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    FixState = Dir.Down;
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }
    void OnCollisionExit2D(Collision2D obj)
    {
        if (obj.gameObject.tag == Ground)
        {
            OnGround = false;
            FixState = Dir.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
    void Movement()
    {
        rb.velocity = new Vector2(Move * Speed, rb.velocity.y);
        switch (FixState)
        {
            case Dir.Up: FixUp(); break;
            case Dir.Down: FixDown(); break;
            case Dir.Right: FixRight(); break;
            case Dir.Left: FixLeft(); break;
            default: break;
        }
    }
    void FixUp()
    {
        rb.velocity = new Vector2(Move * Speed, 0);
        if (Down)
        {
            FixState = Dir.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity += new Vector2(0, -JumpPower);
        }
    }
    void FixDown()
    {
        rb.velocity = new Vector2(Move * Speed, 0);
        if (Up)
        {
            FixState = Dir.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity += new Vector2(0, JumpPower);
        }
    }
    void FixRight()
    {
        if (Up)
        {
            rb.velocity = new Vector2(0, Speed);
        }
        else if (Down)
        {
            rb.velocity = new Vector2(0, -Speed);
        }
        else
        {
            rb.velocity = new Vector2(Speed, 0);
        }
        if(Move < 0)
        {
            FixState = Dir.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void FixLeft()
    {
        if (Up)
        {
            rb.velocity = new Vector2(0, Speed);
        }
        else if (Down)
        {
            rb.velocity = new Vector2(0, -Speed);
        }
        else
        {
            rb.velocity = new Vector2(-Speed, 0);
        }
        if (Move > 0)
        {
            FixState = Dir.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void ShotRay()
    {
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
    }
    void Invincible()
    {
        if (InvincibleFlg)
        {
            HitCollider.enabled = false;
        }
        else
        {
            HitCollider.enabled = true;
        }
    }
}
