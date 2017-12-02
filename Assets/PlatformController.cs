using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Please obey the rules and update the namespace of this MonoBehaviour PlatformController !

public class PlatformController : MonoBehaviour 
{
    private Rigidbody2D _rigidbody;

    bool jump = false;

    bool move = false;

    float directionHor = 0;

    public float speed;

    public float jumpPower;


	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Input.GetAxisRaw("Horizontal"));

        directionHor = Input.GetAxisRaw("Horizontal");
	}


    private void FixedUpdate()
    {
        Vector2 currentVel = _rigidbody.velocity;



        if (directionHor != 0)
        {
            _rigidbody.velocity = new Vector2(speed * directionHor, currentVel.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(speed * directionHor, currentVel.y);
        }
    }
}
