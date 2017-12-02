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

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
	}
    // b jump, a sprint
    // left right walk
    // Update is called once per frame

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
            Debug.Log("Grounded");
            _numGrounded++;
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

    void FixedUpdate () {
        var h = Input.GetAxis("Horizontal");

        _rigidbody.velocity += VelocityMultiplier * h * Vector2.right * Time.fixedDeltaTime;

        if (Input.GetButton("Jump") && _grounded)
        {
            _rigidbody.velocity += Vector2.up * JumpVelocity * Time.fixedDeltaTime;
        }

        if (_rigidbody.velocity.y < -0.01)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (_rigidbody.velocity.y > 0.01 && !Input.GetButton("Jump"))
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        //if (Input.anyKey)
        //{
        //    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        //    {
        //        if (Input.GetKeyDown(kcode))
        //            Debug.Log("Key pressed : " + kcode);
        //    }
        //}
    }
}
