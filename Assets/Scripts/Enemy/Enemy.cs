using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FoodNearbyTransition))]
[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(UpgradingSlime))]
[RequireComponent(typeof(Slime))]
[RequireComponent(typeof(EnemyDetectorFood))]
public class Enemy : MonoBehaviour, ICountable
{
    [SerializeField] private int _score;
    [SerializeField] private int _countScoreForUpgrade;
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _layerMask;

    private Transform _randomItem;
    private Transform _transform;
    private UpgradingSlime _upgradingSlime;
    private Slime _slime;
    private Vector2 _direction = new Vector2();
    private EnemyMovement _enemyMovement;
    private EnemyStateMachine _enemyStateMachine;
    private EnemyDetectorFood _enemyDetectorFood;

    public Transform Target => _target; 
    public UpgradingSlime UpgradingSlime => _upgradingSlime; 
    public Slime Slime => _slime;
    public int Score => _score;
    public int CountScoreForUpgrade => _countScoreForUpgrade;
    public LayerMask LayerMask => _layerMask;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;
    public EnemyDetectorFood EnemyDetectorFood => _enemyDetectorFood;
    public Player Player => _player;

    private void Awake()
    {
        _enemyDetectorFood = GetComponent<EnemyDetectorFood>();
        _enemyDetectorFood.Init();
        _transform = GetComponent<Transform>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyStateMachine.Init();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyMovement.Init();
        _upgradingSlime = GetComponent<UpgradingSlime>();
        _upgradingSlime.Init();
        _slime = GetComponent<Slime>();
        _slime.Init();
        _slime.ItemWasEaten += _enemyDetectorFood.SetNearbyRandomTarget;
    }

    private void Update()
    {
        _slime.TryCreateBlot(_direction);
    }

    public void AddScore(int reward)
    {
        _countScoreForUpgrade++;
        _score += reward;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnDisable()
    {
        _slime.ItemWasEaten -= _enemyDetectorFood.SetNearbyRandomTarget;
    }

    public void ResetCountScoreForUpgrade()
    {
        _countScoreForUpgrade = 0;
    }
}
