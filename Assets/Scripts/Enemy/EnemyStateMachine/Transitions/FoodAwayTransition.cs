using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAwayTransition : Transition
{
    private List<Item> _availableItems = new List<Item>();

    private void Update()
    {
        _availableItems = Enemy.GetNearbyAvailableItems();
        if (_availableItems.Count == 0)
            SwitchOnTransition();
    }
}
