using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MainGame main;
    [SerializeField] private LevelPattern levelPattern;

    [Header("Level Identifiers")]
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask whatIsLevel;
    [SerializeField] private LayerMask whatIsDeath;

    [Header("Game Design")]
    [SerializeField] private AudioClip jumpAud;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb2d;
    private Animator anim;
    private AudioSource audioSource;

    private float playerSpawingPoint;
    [HideInInspector] public bool grounded;
    private bool wasGrounded;

    private bool canJump = false;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = jumpAud;
    }

    public void MyStart()
    {
        playerSpawingPoint = levelPattern.firstGroundYPos - (levelPattern.GetBrickHeight() / 2f);
        transform.position = new Vector3(0f, playerSpawingPoint, 0f);
        gameObject.SetActive(true);
    }
    
    private void Update()
    {
        if (main.gameRunning)
        {
            GameOverQuery();
            
            HitQuery();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            AnimController();
        }
    }

    private void GameOverQuery()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckLeft.position, 0.02f, whatIsDeath);
        if (colliders.Length == 0)
        {
            StopAllCoroutines();
            main.GameOver();
            IdleAnim();
            gameObject.SetActive(false);
        }
    }
    
    void HitQuery()
    {
        float groundedRadius = 0.02f;

        wasGrounded = grounded;
        grounded = false;
        
        Collider2D[] collidersLeft = Physics2D.OverlapCircleAll(groundCheckLeft.position, groundedRadius, whatIsLevel);
        for (int i = 0; i < collidersLeft.Length; i++)
        {
            if (collidersLeft[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        Collider2D[] collidersRight = Physics2D.OverlapCircleAll(groundCheckRight.position, groundedRadius, whatIsLevel);
        for (int i = 0; i < collidersRight.Length; i++)
        {
            if (collidersRight[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        

        canJump = false;
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = whatIsLevel;
        float jumpRadius = 0.2f;

        RaycastHit2D[] hitsLeft = new RaycastHit2D[1];
        int hitLeft = rb2d.Cast(Vector2.down, filter, hitsLeft, jumpRadius);
        if (hitLeft > 0)
        {
            canJump = true;
        }

        RaycastHit2D[] hitsRight = new RaycastHit2D[1];
        int hitRight = rb2d.Cast(Vector2.down, filter, hitsRight, jumpRadius);
        if (hitRight > 0)
        {
            canJump = true;
        }
    }
    
    private void Jump()
    {
        if (grounded || canJump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            audioSource.Play();
        }
    }
    
    #region Animations
    private void AnimController()
    {
        if (grounded)
        {
            RunAnim();
        }
        if (wasGrounded != false && grounded)
        {
            JustLandedAnim();
        }
        else if (rb2d.velocity.y < 0f)
        {
            FallingDownAnim();
        }
        else if (rb2d.velocity.y > 0f)
        {
            JumpingUpAnim();
        }
    }

    public void IdleAnim()
    {
        anim.SetBool("notMoving", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumpingUp", false);
        anim.SetBool("isFallingDown", false);
        anim.SetBool("justLanded", false);
    }
    public void RunAnim()
    {
        anim.SetBool("notMoving", false);
        anim.SetBool("isRunning", true);
        anim.SetBool("isJumpingUp", false);
        anim.SetBool("isFallingDown", false);
        anim.SetBool("justLanded", false);
    }
    private void JumpingUpAnim()
    {
        anim.SetBool("notMoving", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumpingUp", true);
        anim.SetBool("isFallingDown", false);
        anim.SetBool("justLanded", false);
    }
    private void FallingDownAnim()
    {
        anim.SetBool("notMoving", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumpingUp", false);
        anim.SetBool("isFallingDown", true);
        anim.SetBool("justLanded", false);
    }
    private void JustLandedAnim()
    {
        anim.SetBool("notMoving", false);
        anim.SetBool("isRunning", true);
        anim.SetBool("isJumpingUp", false);
        anim.SetBool("isFallingDown", false);
        anim.SetBool("justLanded", true);
    }
    #endregion
}