using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LazyFollowMario : MonoBehaviour {
    public float DampTime = 0.15f;
    public float LazyWaitMeter = 1f;
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
            Vector3 delta = Target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            delta.z = 0;
            delta.y = 0;

            // clamp
            if (delta.magnitude > LazyWaitMeter)
            {
                Debug.Log("Fixing camera to side");
                delta = delta - delta.normalized * LazyWaitMeter;
                Debug.DrawRay(transform.position, delta, Color.green);
                transform.position += delta;
            }
            
            Vector3 destination = transform.position + delta;
            var newPosition = Vector3.SmoothDamp(transform.position, destination, ref _velocity, DampTime);
            transform.position = newPosition;
        }

    }
}
