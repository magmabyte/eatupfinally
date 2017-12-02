using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LazyFollowMario : MonoBehaviour {
    public float DampTime = 0.15f;
    private Vector3 _velocity = Vector3.zero;
    public Transform Target;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (Target)
        {
            Vector3 point = _camera.WorldToViewportPoint(Target.position);
            Vector3 delta = Target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, DampTime);
        }

    }
}
