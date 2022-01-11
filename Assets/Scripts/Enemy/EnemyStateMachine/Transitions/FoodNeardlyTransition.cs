using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNeardlyTransition : Transition
{
    [SerializeField] private int _distanceToFood;
    private List<Item> _itemsNeardly = new List<Item>();

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Enemy.transform.position, _distanceToFood,Enemy.LayerMask);
        if (hitColliders.Length == 0)

            return;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Item item))
            {
                if (Enemy.UpgradingSlime.LevelSlime >= item.RequiredLevel)
                {
                    if (item != null)
                    {
                        _enemyStateMachine.SetTarget(item.transform);
                        SwitchOnTransition();
                        return;
                    }
                }
            }
        }
    }
}
