using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPointsTransition : Transition
{
    [SerializeField] private float _distanceToPlayer;

    private bool _playerNearby;
    private List<Item> _availableItems;

    private void Update()
    {
        if (Enemy.Player != null)
            _playerNearby = Vector3.Distance(transform.position, Enemy.Player.Transform.position) < _distanceToPlayer;
        if (_playerNearby && Enemy.Slime.UpgradingSlime.LevelSlime > Enemy.Player.Slime.UpgradingSlime.LevelSlime)
            return;

        if (Enemy.Target != null)
            return;
        SwitchOnTransition();
    }
}
