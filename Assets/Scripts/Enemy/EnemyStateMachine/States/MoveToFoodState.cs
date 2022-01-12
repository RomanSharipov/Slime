using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFoodState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        if (Target != null)
        {
            Enemy.EnemyMovement.MoveTo(Target.position, _speed, _rotationSpeed);
        }
    }
}
