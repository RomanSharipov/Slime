using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceTraveled;

    private void Update()
    {
        _distanceTraveled += _speed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTraveled);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTraveled) * Quaternion.Euler(0, 0, 90);
    }
}
