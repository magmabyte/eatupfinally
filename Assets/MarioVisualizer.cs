using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioVisualizer : MonoBehaviour {

    public void SetSlim(bool isSlim)
    {
        // get state
        _animator = isSlim ? SlimAnimator : FatAnimmator;
        // set the same state
    }

    private Animator _animator;
    public Animator SlimAnimator;
    public Animator FatAnimmator;

	void Start () {
        _animator = SlimAnimator;

	}
	
    public bool DebugSwitchSlim = false;
    public bool DebugIsSlim = true;
	void Update () {
		if (DebugSwitchSlim)
        {
            DebugIsSlim = !DebugIsSlim;
            SetSlim(DebugIsSlim);
            DebugSwitchSlim = false;
        }
	}
    
    public void Walk()
    {
        _animator.SetTrigger("Walk");
    }

    public void WalkBackwards()
    {
        _animator.SetTrigger("WalkBackwards");
    }

    public void Jump()
    {

    }

    public void JumpBackwards()
    {

    }

    public void Idle()
    {

    }
}
