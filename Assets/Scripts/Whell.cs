using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whell : MonoBehaviour
{
    [SerializeField] private float _speedRotation;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.rotation *= Quaternion.Euler(new Vector3(_speedRotation * Time.deltaTime, 0, 0));
    }
}
