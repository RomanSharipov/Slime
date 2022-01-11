using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFoodState : State
{

    [SerializeField] private float _speed;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Target != null)
        {
            _transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
            _transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z), _speed * Time.deltaTime);

        }
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
