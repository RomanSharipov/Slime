using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFoodState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        if (Enemy.Target == null)
            Enemy.EnemyDetectorFood.SetNearbyRandomTarget();

        if (Enemy.Target == null)
            return;
        Enemy.EnemyMovement.MoveTo(Enemy.Target.position, _speed, _rotationSpeed);
    }
}
