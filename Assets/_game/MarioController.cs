using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {

    private Rigidbody2D _rigidbody;
    public float VelocityMPerS = 1f;

    public MarioStates marioStates;

    public float JumpVelocity = 1f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float MaxSpeed = 2f;

    public MarioVisualizer marioVisual;

    [SerializeField]
    private float _groundCheckRayLength;

    void Start () {
        marioStates = new MarioStates();
        _rigidbody = GetComponent<Rigidbody2D>();

        AdjustScale();
        UpdateMarioHeight();
    }
    
    private int _numColliding;

    private bool IsGrounded()
    {
        var isGrounded = _IsGrounded();
        //Debug.Log("Grounded: " + isGrounded);
        return isGrounded;
    }
    private bool _IsGrounded()
    {
        if (_numColliding <= 0) return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, _groundCheckRayLength, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 1f);

        //if (hit.collider != null)
        //{
        //    Debug.Log("Found Collider: " + hit.collider != null);
        //    Debug.Log("Type ground: " + hit.collider.CompareTag("Ground"));
        //}
        return hit.collider != null && hit.collider.CompareTag("Ground");
    }
    private bool IsJumping()
    {
        return !IsGrounded() && _numColliding == 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _numColliding++;

            if (IsGrounded())
            {
                ResetVelocityY();
            } else
            {
                //Debug.Log("Collision with " + collision.collider.name + ". Not grounded");
            }
        } else
        {
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _numColliding--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            //Debug.Log("Enemy");
            EnemiesTrigger();
        }

        else if (collision.CompareTag("Burger"))
        {
            Destroy(collision.gameObject);
            //Debug.Log("Burger");
            BurgerTrigger();
        }
    }

    private void FixedUpdate()
    {
        ApplyDirectControls();
        ApplyMarioFlavorJump();
        ClampSpeed();
    }

    private void ResetVelocityY()
    {
        var velocity = _rigidbody.velocity;
        velocity.y = 0;
        _rigidbody.velocity = velocity;
    }

    private void ApplyDirectControls()
    {
        var xAxis = Input.GetAxis("Horizontal");

        var velocity = _rigidbody.velocity;

        var isGrounded = IsGrounded();
        if (isGrounded || IsJumping())
        {
            velocity.x = VelocityMPerS * xAxis;
        }

        if (Input.GetButton("Jump") && IsGrounded())
        {
            velocity.y = JumpVelocity;
        }

        _rigidbody.velocity = velocity;
    }

    private void ApplyMarioFlavorJump()
    {
        var velocity = _rigidbody.velocity;

        if (velocity.y < -10E-3) // if falling
        {
            velocity.y += Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (velocity.y > 10E-3 && !Input.GetButton("Jump")) // if jumping but not holding jump
        {
            velocity.y = Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        _rigidbody.velocity = velocity;
    }

    private void ClampSpeed()
    {
        var velocity = _rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        _rigidbody.velocity = velocity;
    }
    
    void Update () {
        if (_rigidbody.velocity.x > 0)
        {
            if (IsJumping())
                marioVisual.Jump();
            else
                marioVisual.Walk();
        }
        else if (_rigidbody.velocity.x < 0)
        {
            if (IsJumping())
                marioVisual.JumpBackwards();
            else
                marioVisual.WalkBackwards();
        } else
        {
            marioVisual.Idle();
        }
    }

    private void UpdateMarioHeight()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _groundCheckRayLength = spriteRenderer.size.y * transform.localScale.y / 2 + .01f;
    }

    private void AdjustScale()
    {
        if (marioStates.burgersEaten <= 0)
        {
            transform.localScale = Vector3.one;
            marioVisual.SetSlim(true);
        } else
        {
            transform.localScale = new Vector3(marioStates.burgersEaten * 0.2f + 1, marioStates.burgersEaten * 0.2f + 1, marioStates.burgersEaten * 0.2f + 1);
            marioVisual.SetSlim(false);
        }

        UpdateMarioHeight();
    }

    public void EnemiesTrigger()
    {
        marioStates.burgersEaten--;
        AdjustScale();
    }

    public void BurgerTrigger()
    {
        marioStates.burgersEaten++;
        AdjustScale();
    }
    
    public void CarrotHelper()
    {
        marioStates.burgersEaten++;
        AdjustScale();
    }
}

public class MarioStates
{
    public int burgersEaten = 0;
}
