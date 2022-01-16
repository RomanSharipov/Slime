using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAwayTransition : Transition
{
    private bool _playerIsTarget;
    private List<Item> _availableItems = new List<Item>();

    private void Update()
    {
        _playerIsTarget = Enemy.Target == Enemy.Player.Transform;
        if (_playerIsTarget)
            return;
        _availableItems = Enemy.EnemyDetectorFood.GetNearbyAvailableItems();
        if (_availableItems.Count == 0)
            SwitchOnTransition();
    }
}
