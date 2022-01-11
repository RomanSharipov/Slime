using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFoodState : State
{

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _targetRotation;
    private Vector3 _direction = new Vector3();

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Target != null)
        {
            _direction = Target.position - _transform.position;
            _targetRotation = Quaternion.LookRotation(_direction);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _targetRotation, _rotationSpeed*Time.deltaTime);
            _transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z), _speed * Time.deltaTime);

        }
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
