using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Collider2D[] col;
    Rigidbody2D rb;
    Animator anim;
    AudioSource jumpSE;
    public Transform groundCheck;
    public TouchInput input;
    public PrismController prismCon;
    public StageControl stageCon;
    public PlayerCamera playerCam;
    public ParticleSystem particle;
    public AudioClip dieSE;

    //animator parameter Id
    static int isGroundedId;
    static int hVeloId;
    static int vSpeedId;
    static int triggerJumpId;
    static int dieId;
    static int rebirthId;

    //player status
    public bool facingRight { private set; get; }
    public bool isGrounded { private set; get; }
    public Transform checkPoint;
    public bool iscontrollable = true;
    bool blockForward = false;

    //character configuration
    public float walkSpeed = 8.0f;
    public float maxJumpTime = 0.2f;
    public float jumpSpeed = 16.0f;

    //misc
    public LayerMask groundMask;


    void Awake()
    {
        col = GetComponents<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpSE = GetComponent<AudioSource>();

        isGroundedId = Animator.StringToHash("isGrounded");
        hVeloId = Animator.StringToHash("hVelo");
        vSpeedId = Animator.StringToHash("vSpeed");
        triggerJumpId = Animator.StringToHash("triggerJump");
        dieId = Animator.StringToHash("die");
        rebirthId = Animator.StringToHash("rebirth");

        facingRight = true;
        isGrounded = false;
    }

    void Update()
    {
        //Move
        GroundCheck();

        
        float horizSpeed = iscontrollable ? input.GetMoveAxis() * walkSpeed : 0;
        if ((facingRight && horizSpeed < 0) || (!facingRight && horizSpeed > 0))
        {
            facingRight = !facingRight;
            Flip();
        }
        
        if (blockForward)
            if (facingRight && horizSpeed > 0.0f || !facingRight && horizSpeed < 0.0f)
                horizSpeed = 0.0f;

        rb.velocity = new Vector2(horizSpeed, rb.velocity.y);

        //Jump
        if (isGrounded && iscontrollable && input.jump)
        {
            anim.SetTrigger(triggerJumpId);
            StartCoroutine("JumpCoroutine");
            jumpSE.Play();
        }

        //更新動畫
        anim.SetFloat(hVeloId, Mathf.Abs(rb.velocity.x));
        anim.SetFloat(vSpeedId, rb.velocity.y);
        
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

        while (timer < maxJumpTime && input.jump && iscontrollable)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            yield return null;
            timer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        blockForward = false;
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if ((1 << c.gameObject.layer & groundMask.value) != 0 && !isGrounded && !c.isTrigger)
            blockForward = true;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if ((1 << c.gameObject.layer & groundMask.value) != 0 && !c.isTrigger)
            blockForward = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillZone"))
            Kill();
    }

    public void Kill()
    {
        if (!iscontrollable)
            return;

        particle.Play();
        jumpSE.PlayOneShot(dieSE);
        iscontrollable = false;
        foreach (Collider2D c in col)
            c.enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.SetTrigger(dieId);
        if (prismCon.currerntMode != MagicColor.White)
            prismCon.ClosePrism();
    }

    public void Rebirth()
    {
        StartCoroutine(RebirthCoroutine());
    }

    IEnumerator RebirthCoroutine()
    {
        stageCon.StartCoroutine(stageCon.FadeOutScreen());
        while (stageCon.isInTransition) yield return null;

        anim.StopPlayback();
        transform.position = checkPoint.position;
        if (transform.localScale.x != 1)
        {
            facingRight = true;
            Flip();
        }
        playerCam.AimImmediately();
        iscontrollable = true;
        foreach (Collider2D c in col)
            c.enabled = true;
        rb.isKinematic = false;
        anim.SetTrigger(rebirthId);
        stageCon.ResetStage();

        stageCon.StartCoroutine(stageCon.FadeInScreen());
    }

    void GroundCheck()
    {
        isGrounded = false;
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, groundCheck.position, groundMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].collider.isTrigger)
            {
                isGrounded = !hits[i].collider.isTrigger;
                break;
            }
        }

        anim.SetBool(isGroundedId, isGrounded);
    }
}
    