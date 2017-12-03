using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioVisualizer : MonoBehaviour {
    private bool _isSlim;
    private Animator _animator;

    enum States { 
        Walking,
        Jumping,
        Idle
    }

    private bool goingRight = false;
    private States _lastState = States.Idle;

    void AdjustOrientation()
    {
        var scale = transform.localScale;
        scale.x = (goingRight ? 1 : -1) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void AdjustAnimator()
    {
        switch (_lastState)
        {
            case States.Walking:
                _animator.SetTrigger(AdjustName("Walk"));
                break;
            case States.Jumping:
                _animator.SetTrigger(AdjustName("Jump"));
                break;
            case States.Idle:
                _animator.SetTrigger(AdjustName("Idle"));
                break;
        }
    }

    void AdjustVisuals()
    {
        AdjustOrientation();
        AdjustAnimator();
    }

	void Awake () {
        _animator = GetComponent<Animator>();
        AdjustVisuals();
	}

    private string AdjustName(string command)
    {
        return command + (_isSlim ? "" : "Fat");
    }

    public void SetSlim(bool isSlim)
    {
        _isSlim = isSlim;

        AdjustVisuals();
    }

    public void Walk()
    {
        _lastState = States.Walking;
        goingRight = true;
        AdjustVisuals();
    }

    public void WalkBackwards()
    {
        _lastState = States.Walking;
        goingRight = false;
        AdjustVisuals();
    }

    public void Jump()
    {
        _lastState = States.Jumping;
        goingRight = true;
        AdjustVisuals();
    }

    public void JumpBackwards()
    {
        _lastState = States.Jumping;
        goingRight = false;
        AdjustVisuals();
    }

    public void Idle()
    {
        _lastState = States.Idle;
        AdjustVisuals();
    }
}
