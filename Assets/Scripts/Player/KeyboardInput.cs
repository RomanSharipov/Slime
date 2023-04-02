using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInput
{
    private float _vertical;
    private float _horizontal;

    public float Vertical => _vertical;
    public float Horizontal => _horizontal;

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }
}
