using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDetectorFood : MonoBehaviour
{
    [SerializeField] private int _distanceToFood;

    private Enemy _enemy;
    private Transform _transform;
    private Transform _randomItem;
    private UpgradingSlime _upgradingSlime;

    public UpgradingSlime UpgradingSlime => _upgradingSlime;
    public Enemy Enemy => _enemy;

    public void Init()
    {
        _enemy = GetComponent<Enemy>();
        _upgradingSlime = GetComponent<UpgradingSlime>();
        _transform = GetComponent<Transform>();
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
        Collider[] hitColliders = Physics.OverlapSphere(_transform.position, _distanceToFood, Enemy.LayerMask);

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
