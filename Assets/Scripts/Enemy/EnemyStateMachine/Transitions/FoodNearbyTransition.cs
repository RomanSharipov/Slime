using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNearbyTransition : Transition
{
    [SerializeField] private int _distanceToFood;

    private List<Item> _availableItems = new List<Item>();
    private bool _targetAchieved;

    public int DistanceToFood => _distanceToFood;

    private void Update()
    {
        _availableItems = Enemy.GetNearbyAvailableItems();
        if (_availableItems.Count == 0)
            return;

        if (Target == null)
            Enemy.SetNearbyRandomTarget();
        SwitchOnTransition();
    }
}
