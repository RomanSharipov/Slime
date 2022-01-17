using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearbyTransition : Transition
{
    [SerializeField] private float _distanceToPlayer;

    private bool _playerNearby;

    private void Update()
    {
        if (Enemy.Player == null)
            return;
        _playerNearby = Vector3.Distance(transform.position, Enemy.Player.Transform.position) < _distanceToPlayer;

        if (_playerNearby == false)
            return;
        if (Enemy.Slime.UpgradingSlime.LevelSlime > Enemy.Player.Slime.UpgradingSlime.LevelSlime)
            SwitchOnTransition();
    }
}
