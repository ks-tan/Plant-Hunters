using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D rb;
    public bool facingRight = false;
    public float maxSpeed = 10f;
    public float moveForce = 100f;
    public float floatForce = 10f;
    //public bool doubleJump = false;
    public bool jump = false;
    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    public bool grounded = false;			// Whether or not the player is grounded.
    private Animator anim;
    private string ladybugName = "ladybug";
    private string mantisName = "mantis";

    [SerializeField]
    protected float m_jumpHeight = 11;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("/" + name + "/groundCheck");
    }

    
    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        if (grounded && rb.velocity.y <= 0)
        {
            
            //doubleJump = false;
            anim.SetBool("IsJumping", false);
        }

        if ((grounded && ((Input.GetButtonDown("Jump") && name == ladybugName) 
            || (Input.GetButtonDown("JumpP2") && name == mantisName))))
        //if ((grounded || (!doubleJump && name == ladybugName)) && Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("IsJumping", true);
            //if (!grounded)
            //if (!doubleJump && !grounded)
           // {
            //    jump = true;
            //    anim.SetBool("IsJumping", true);
                //doubleJump = true;
          //  }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if(h != 0) {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if(v == 1 && name == ladybugName) {
            rb.AddForce(Vector3.up * v * floatForce);
        }

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector3.right * h * moveForce);
        }
        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            // ... set the player's velocity to the maxSpeed in the x axis.
            rb.velocity = new Vector3(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y, 0);
        }

        if (h == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
        {
            //flip the player.
            Flip();
        }

        if (jump)
        {
           
            // Add a vertical force to the player.
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0f, m_jumpHeight), ForceMode2D.Impulse);

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }

    }

    public bool IsFacingRight() { return facingRight; }
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void SetMaxSpeed(float spd)
    {
        maxSpeed = spd;
    }

    public void SetJumpHeight(float height)
    {
        m_jumpHeight = height;
    }

    public float GetMaxSpeed() { return maxSpeed; }
    public float GetJumpHeight() { return m_jumpHeight; }
}
