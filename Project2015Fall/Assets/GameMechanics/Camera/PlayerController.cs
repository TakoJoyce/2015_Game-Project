using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform groundCheck;
    Animator    anim;

    //animator parameter Id
    static int isGroundedId;
    static int hVeloId;
    static int vSpeedId;
    static int triggerJumpId;

    //player status
    public bool facingRight { private set; get; }
    public bool isGrounded { private set; get; }

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
        isGroundedId = Animator.StringToHash("isGrounded");
        hVeloId = Animator.StringToHash("hVelo");
        vSpeedId = Animator.StringToHash("vSpeed");
        triggerJumpId = Animator.StringToHash("triggerJump");

        facingRight = true;
        isGrounded = false;

        //groundMask = 1 << LayerMask.NameToLayer("Ground");
    }
	
    void FixedUpdate()
    {
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
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, groundMask);
        anim.SetBool(isGroundedId, isGrounded);
    }

	// Update is called once per frame
	void Update ()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger(triggerJumpId);
            StartCoroutine("JumpCoroutine");
        }
	}

    IEnumerator JumpCoroutine()
    {
        float timer = 0.0f;

        while (timer < maxJumpTime && !Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            yield return null;
            timer += Time.deltaTime;
        }

    }

    void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1.0f;
        transform.localScale = newScale;
    }
}
