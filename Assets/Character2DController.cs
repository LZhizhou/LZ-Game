using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public Animator Animator;
    public float MovementSpeed = 1;
    public float JumpForce = 10;
    private float movingInput;
    private bool facingRight = true;
    private BoxCollider2D boxCollider2d;

    public int ExtraJump = 2;
    private int ExtraJumpValue;
   [SerializeField] private LayerMask  WhatIsGround ;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        ExtraJumpValue = ExtraJump;
        Time.timeScale = 1;
    }
    private void Update(){
        if (Input.GetButtonDown("Jump") && ExtraJumpValue > 0)
        {
            jump();
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        
        movingInput = Input.GetAxis("Horizontal");
        Animator.SetFloat("Speed", Mathf.Abs(movingInput));
        Debug.Log(movingInput);
        _rb.velocity = new Vector2(movingInput * MovementSpeed, _rb.velocity.y);
        if (facingRight && movingInput < 0)
        {
            flip();
        }
        else if (!facingRight && movingInput > 0)
        {
            flip();
        }
        if (isGround())
        {
            Animator.SetBool("IsGround",true);
            ExtraJumpValue = ExtraJump;
        }else{
            Animator.SetBool("IsGround",false);
        }


    }
    private void jump(){
            
        _rb.velocity = Vector2.up * JumpForce;
        ExtraJumpValue--;
    }
    private bool isGround(){
       float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, WhatIsGround);

        Color rayColor;
        if (raycastHit.collider != null) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);

        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
