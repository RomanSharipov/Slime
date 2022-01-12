using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FoodNearbyTransition))]
[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(UpgradingSlime))]
[RequireComponent(typeof(Slime))]
public class Enemy : MonoBehaviour, ICountable
{
    [SerializeField] private int _score;
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _layerMask;

    private FoodNearbyTransition _foodNearbyTransition;
    private Transform _randomItem;
    private Transform _transform;
    private UpgradingSlime _upgradingSlime;
    private Slime _slime;
    private Vector2 _direction = new Vector2();
    private EnemyMovement _enemyMovement;
    private EnemyStateMachine _enemyStateMachine;

    public Transform Target => _target; 
    public UpgradingSlime UpgradingSlime => _upgradingSlime; 
    public Slime Slime => _slime;
    public int Score => _score;
    public LayerMask LayerMask => _layerMask;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

    private void Awake()
    {
        _foodNearbyTransition = GetComponent<FoodNearbyTransition>();
        _transform = GetComponent<Transform>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyStateMachine.Init();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.Init();
        _upgradingSlime = GetComponent<UpgradingSlime>();
        _upgradingSlime.Init();
        _slime = GetComponent<Slime>();
        _slime.Init();
        _slime.ItemWasEaten += SetNearbyRandomTarget;
    }

    private void Update()
    {
        _slime.TryCreateBlot(_direction);
    }

    public void AddScore(Item eatenItem)
    {
        _score += eatenItem.Reward;
    }

    public void SetNearbyRandomTarget()
    {
        _target = GetNearbyRandomTarget();
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
        Collider[] hitColliders = Physics.OverlapSphere(_transform.position, _foodNearbyTransition.DistanceToFood, LayerMask);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Item item) && UpgradingSlime.LevelSlime >= item.RequiredLevel)
            {
                items.Add(item);
            }
        }
        return items;
    }

    private void OnDisable()
    {
        _slime.ItemWasEaten -= SetNearbyRandomTarget;
    }
}
