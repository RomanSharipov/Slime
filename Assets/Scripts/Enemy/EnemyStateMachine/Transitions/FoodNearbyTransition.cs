using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNearbyTransition : Transition
{
    [SerializeField] private int _distanceToPlayer;

    private List<Item> _availableItems = new List<Item>();
    private bool _playerNearby;



    private void Update()
    {
        _availableItems = Enemy.EnemyDetectorFood.GetNearbyAvailableItems();

        if (_availableItems.Count == 0)
            return;

        if (Enemy.Player != null)
            _playerNearby = Vector3.Distance(transform.position, Enemy.Player.Transform.position) < _distanceToPlayer;

        if (Enemy.Player == null)
        {
            SwitchOnTransition();
            return;
        }

        if (_playerNearby && Enemy.Slime.UpgradingSlime.LevelSlime > Enemy.Player.Slime.UpgradingSlime.LevelSlime)
            return;

        if (Target == null)
            Enemy.EnemyDetectorFood.SetNearbyRandomTarget();

        SwitchOnTransition();
    }
}
