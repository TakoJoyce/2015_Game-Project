﻿using UnityEngine;
using System.Collections;

public class PlayerControllerKB : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public Transform groundCheck;
    public TouchInput input;

    //animator parameter Id
    static int isGroundedId;
    static int hVeloId;
    static int vSpeedId;
    static int triggerJumpId;
    static int twinkleId;

    //player status
    public bool facingRight { private set; get; }
    public bool isGrounded { private set; get; }
    Transform checkPoint;

    //character configuration
    public float walkSpeed = 8.0f;
    public float maxJumpTime = 0.2f;
    public float jumpSpeed = 16.0f;

    //misc
    public LayerMask groundMask;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        isGroundedId    = Animator.StringToHash("isGrounded");
        hVeloId         = Animator.StringToHash("hVelo");
        vSpeedId        = Animator.StringToHash("vSpeed");
        triggerJumpId   = Animator.StringToHash("triggerJump");
        twinkleId       = Animator.StringToHash("twinkle");

        facingRight = true;
        isGrounded = false;
    }

    void Update()
    {
        //Move
        GroundCheck();

        float horizSpeed = Input.GetAxis("Horizontal") * walkSpeed;
        if ((facingRight && horizSpeed < 0) || (!facingRight && horizSpeed > 0))
        {
            facingRight = !facingRight;
            Flip();
        }
        rb.velocity = new Vector2(horizSpeed, rb.velocity.y);
        anim.SetFloat(hVeloId, Mathf.Abs(rb.velocity.x));
        anim.SetFloat(vSpeedId, rb.velocity.y);
        //Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger(triggerJumpId);
            StartCoroutine("JumpCoroutine");
        }
    }

    void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1.0f;
        transform.localScale = newScale;
    }

    IEnumerator JumpCoroutine()
    {
        float timer = 0.0f;

        while (timer < maxJumpTime && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            yield return null;
            timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        switch (c.tag)
        {
            case "CheckPoint":
                checkPoint = c.transform;
                break;
            case "KillZone":
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillZone"))
            Kill();
    }

    void Kill()
    {
        Vector3 rebirthPos = checkPoint.position;
        transform.position = rebirthPos;
        rb.velocity = Vector2.zero;
        anim.SetTrigger(twinkleId);
    }

    void GroundCheck()
    {
		RaycastHit2D hit = Physics2D.Linecast(transform.position, groundCheck.position, groundMask);
		if  (hit)
        {
            isGrounded = !hit.collider.isTrigger;
        }
        else
        {
            isGrounded = false;
        }

        anim.SetBool(isGroundedId, isGrounded);
    }
}
