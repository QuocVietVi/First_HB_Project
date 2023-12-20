using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;
    [SerializeField] private float climbSpeed = 2;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject chooseLevel,mainPanel,player;
    [SerializeField] private Transform startPoint;
    private Collider2D platformCollider;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isClimbing ;
    private bool isRope;
    //private bool isDead = false;

    private float horizontal;
    private float vertical;
    private int score = 0;
    private Vector3 savePoint;
    private int highScore = 0;



    // Start is called before the first frame update
    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead) 
        {
            return;
        }

        isGrounded = CheckGrounded();


        // -1 -> 0 -> 1
        horizontal = Input.GetAxisRaw("Horizontal"); // lấy điều khiển từ bàn phím (chiều ngang)
        //climb
        vertical = Input.GetAxisRaw("Vertical"); // chiều dọc

        if (isRope && Mathf.Abs(vertical) > 0.1f) // khi bấm phím
        {
            isClimbing = true;
            ChangeAnim("climb");               
        }

        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //check is grounded
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            //jump
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            //change anim run
            if (Mathf.Abs(horizontal) > 0.1f) // khi bấm phím
            {
                ChangeAnim("run");

            }

            //attack
            if (Input.GetKey(KeyCode.Z) && isGrounded)
            {
                Attack();

            }

            //throw
            if (Input.GetKey(KeyCode.X) && isGrounded)
            {
                Throw();

            }

        } // end check isgrounded

        //check fall
        if (!isGrounded && rb.velocity.y < 0 && isClimbing == false)
        {
            isJumping = false;
            ChangeAnim("fall");
        }

        if (Mathf.Abs(horizontal) > 0.1f) // khi bấm phím
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            //horizontal > 0 -> trả về 0, nếu horizontal <= 0 -> trả về 180
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
            //transform.localScale = new Vector3(horizontal, 1, 1);
        }

        else if (isClimbing == false && isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }

    }
    private void FixedUpdate()
    {
        
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            speed = 2;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
            if (platformCollider != null) 
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platformCollider, true);

            }
        }
        else
        {
            rb.gravityScale = 3f;
            speed = 6;
            if (platformCollider != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platformCollider, false);
            }

        }
    }
    public override void OnInit()
    {
        base.OnInit();
        //isDead = false;
        isAttack = false;

        transform.position = savePoint;
        ChangeAnim("idle");
        DeActiveAtack();

        UIManager.instance.SetScore(score);
        UIManager.instance.SetHighScore(highScore);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    protected override void OnDead()
    {
        base.OnDead();
    }

    private bool CheckGrounded()
    {
        //chiếu 1 tia xuống để kiểm tra
        Debug.DrawLine(transform.position,transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer); 

        //if (hit.collider != null)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
        return hit.collider != null;
    }


    public void Attack()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.zero;
            ChangeAnim("attack");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);
            ActiveAtack();
            Invoke(nameof(DeActiveAtack), 0.5f);
        }

    }

    public void Throw()
    {
        if (isGrounded)
        {
            //rb.velocity = Vector2.zero;
            ChangeAnim("throw");
            isAttack = true;
            Invoke(nameof(ResetAttack), 0.5f);

            Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
        }


    }

    private void ResetAttack()
    {
        //ChangeAnim("idle");
        isAttack = false;

    }

    public void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            ChangeAnim("jump");
            rb.AddForce(jumpForce * Vector2.up);
        }

    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    private void ActiveAtack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAtack()
    {
        attackArea.SetActive(false);
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ăn tiền => cộng điểm
        if(collision.tag == "Coin")
        {
            score++;
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }
            UIManager.instance.SetScore(score);
            UIManager.instance.SetHighScore(highScore);
            //UpdateHighScore();
            Destroy(collision.gameObject);
        }
        //dead zone
        if(collision.tag == "DeathZone")
        {
            //isDead = true;
            ChangeAnim("die");

            Invoke(nameof(OnInit), 1f);
        }
        //rope
        if (collision.tag == "Rope")
        {
            isRope = true;
        }
        if (collision.tag == "Finish")
        {
            chooseLevel.SetActive(true);
            mainPanel.SetActive(true);
            transform.position = startPoint.position;
            gameObject.SetActive(false);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //khi out rope
        if(collision.tag == "Rope")
        {
            isRope = false;
            isClimbing = false;
        }
    }

   

}
