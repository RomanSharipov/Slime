using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorFood 
{
    private int _distanceToFood = 10;
    private Enemy _enemy;
    private Transform _randomItem;
    private UpgradingSlime _upgradingSlime;

    public UpgradingSlime UpgradingSlime => _upgradingSlime;
    public Enemy Enemy => _enemy;

    public void Init(Enemy enemy,UpgradingSlime upgradingSlime)
    {
        _enemy = enemy;
        _upgradingSlime = upgradingSlime;
    }

    public void SetNearbyRandomTarget()
    {
        _enemy.SetTarget(GetNearbyRandomTarget());
    }

    public Transform GetNearbyRandomTarget()
    {
        List<Item> items = GetNearbyAvailableItems();
        if (items.Count == 0)
            return null;
        int randomIndex = Random.Range(0, items.Count);
        _randomItem = items[randomIndex].transform;
        return _randomItem;
    }

    public List<Item> GetNearbyAvailableItems()
    {
        List<Item> items = new List<Item>();
        Collider[] hitColliders = Physics.OverlapSphere(_enemy.Transform.position, _distanceToFood, Enemy.LayerMaskFood);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Item item) && UpgradingSlime.LevelSlime > item.RequiredLevel)
            {
                items.Add(item);
            }
        }
        return items;
    }
}
