using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioVisualizer : MonoBehaviour {

    public void SetSlim(bool isSlim)
    {
        _isSlim = isSlim;
        // repeat last state
        ChangeAnimator();
    }

    private bool _isSlim;
    private Animator _animator;
    enum States { 
        Walking,
        WalkingBackwards,
        Jumping,
        JumpingBackwards,
        Idle
    }
    private States _lastState = States.Idle;

    void ChangeAnimator()
    {
        switch (_lastState)
        {
            case States.Walking:
                _animator.SetTrigger(AdjustName("Walk"));

                break;
            case States.WalkingBackwards:
                _animator.SetTrigger(AdjustName("WalkBackwards"));

                break;
            case States.Jumping:
                _animator.SetTrigger(AdjustName("Jump"));

                break;
            case States.JumpingBackwards:
                _animator.SetTrigger(AdjustName("JumpBackwards"));

                break;
            case States.Idle:
                _animator.SetTrigger(AdjustName("Idle"));

                break;
        }
    }

	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	void Update () {
		//if (Input.GetKeyDown(KeyCode.F))
  //      {
  //          Debug.Log("Get Slim");
  //          SetSlim(!_isSlim);
  //      }

  //      if (Input.GetKey(KeyCode.W))
  //      {
  //          Walk();
  //      }
    }

    private string AdjustName(string command)
    {
        return command + (_isSlim ? "" : "Fat");
    }
    
    public void Walk()
    {
        _lastState = States.Walking;
    }

    public void WalkBackwards()
    {
        _lastState = States.WalkingBackwards;

    }

    public void Jump()
    {
        _lastState = States.Jumping;

    }

    public void JumpBackwards()
    {
        _lastState = States.JumpingBackwards;

    }

    public void Idle()
    {
        _lastState = States.Idle;


    }
}
