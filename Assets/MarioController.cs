using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour {

    private Rigidbody2D _rigidbody;
    public float VelocityMultiplier = 1f;

    public float JumpVelocity = 1f;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float MaxSpeed = 2f;

    public MarioVisualizer marioVisual;

    // Use this for initialization
    void Start () {
        marioStates = new MarioStates();
        marioVisual = GetComponent<MarioVisualizer>();
        _rigidbody = GetComponent<Rigidbody2D>();
	}

    private bool _grounded
    {
        get
        {
            return _numGrounded > 0;
        }
    }
    private int _numGrounded;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("Grounded ");
            _numGrounded++;

            if (_numGrounded > 1)
            {
                var velo = _rigidbody.velocity;
                velo.y = 0;
                _rigidbody.velocity = velo;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Enemy");
            EnemiesTrigger();
        }
        else if (collision.CompareTag("Burger"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Burger");
            BurgerTrigger();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("Ungrounded");

            _numGrounded--;

            
        }
    }

    public float SlowDownLerp = .3f;
    void Update () {
        var h = Input.GetAxis("Horizontal");

        _rigidbody.velocity += VelocityMultiplier * h * Vector2.right * Time.deltaTime;

        if (Input.GetButton("Jump") && _grounded)
        {
            _rigidbody.velocity += Vector2.up * JumpVelocity * Time.deltaTime;
            // different velocities based on speed
        }

        if (_rigidbody.velocity.y < -0.01)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rigidbody.velocity.y > 0.01 && !Input.GetButton("Jump"))
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //if (Input.anyKey)
        //{
        //    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        //    {
        //        if (Input.GetKeyDown(kcode))
        //            Debug.Log("Key pressed : " + kcode);
        //    }
        //}

        var velo = _rigidbody.velocity;
        velo.x = Mathf.Clamp(velo.x, -MaxSpeed, MaxSpeed);

        if (Math.Abs(h) < 0.01)
        {
            velo.x = Mathf.Lerp(velo.x, 0, SlowDownLerp);
        }

        _rigidbody.velocity = velo;

        if (_rigidbody.velocity.x > 0)
        {
            marioVisual.Walk();
        }
        else if (_rigidbody.velocity.x < 0)
        {
            marioVisual.WalkBackwards();
        } else
        {
            marioVisual.Idle();
        }
    }


    public void EnemiesTrigger()
    {
        marioStates.burgersEaten--;
        this.transform.localScale = new Vector3(marioStates.burgersEaten * 0.2f - 1, marioStates.burgersEaten * 0.2f - 1, marioStates.burgersEaten * 0.2f - 1);
        if (marioStates.burgersEaten <= 0)
        {
            this.transform.localScale = Vector3.one;
        }
    }

    public void BurgerTrigger()
    {
        marioStates.burgersEaten++;
        this.transform.localScale = new Vector3(marioStates.burgersEaten * 0.2f + 1, marioStates.burgersEaten * 0.2f + 1, marioStates.burgersEaten * 0.2f + 1);
        if(marioStates.burgersEaten <= 0)
        {
            this.transform.localScale = Vector3.one;
        }
    }
    
    public void CarrotHelper()
    {
        marioStates.burgersEaten++;
        this.transform.localScale = new Vector3(marioStates.burgersEaten * 0.2f + 1, marioStates.burgersEaten * 0.2f + 1, marioStates.burgersEaten * 0.2f + 1);
        if (marioStates.burgersEaten <= 0)
        {
            this.transform.localScale = Vector3.one;
        }
    }

    public void OnBeforeTransformParentChanged()
    {
        marioVisual.SetSlim(marioStates.burgersEaten < 0);
    }
    public MarioStates marioStates;

}

public class MarioStates
{
    public int burgersEaten = 0;
    
}
