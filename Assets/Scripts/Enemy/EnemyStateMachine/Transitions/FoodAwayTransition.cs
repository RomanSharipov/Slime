using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAwayTransition : Transition
{
    [SerializeField] private int _distanceToFood;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Enemy.transform.position, _distanceToFood, Enemy.LayerMask);
        if (hitColliders.Length == 0)
            SwitchOnTransition();
    }
}
