using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerState : State
{
    [SerializeField] private int _speed;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        if (Enemy.Player == null)
            return;
        Enemy.EnemyMovement.MoveTo(Enemy.Player.Transform.position, _speed, _rotationSpeed);
    }
}
