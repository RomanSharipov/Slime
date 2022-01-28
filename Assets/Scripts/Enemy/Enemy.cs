using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(Slime))]
public class Enemy : MonoBehaviour, ICountable
{
    [SerializeField] private int _score;
    [SerializeField] private int _countScoreForUpgrade;
    [SerializeField] private Transform _target;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _layerMaskFood;

    private Transform _randomItem;
    private Transform _transform;
    private Slime _slime;
    private Vector2 _direction = new Vector2();
    private EnemyMovement _enemyMovement;
    private EnemyStateMachine _enemyStateMachine;
    private EnemyDetectorFood _enemyDetectorFood ;

    public Transform Target => _target;
    public Transform Transform => _transform;
    public Slime Slime => _slime;
    public int Score => _score;
    public int CountScoreForUpgrade => _countScoreForUpgrade;
    public LayerMask LayerMaskFood => _layerMaskFood;
    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyDetectorFood EnemyDetectorFood => _enemyDetectorFood;
    public Player Player => _player;

    public void Init(Player player,Transform path)
    {
        _player = player;
        _slime = GetComponent<Slime>();
        _slime.Init();
        _enemyDetectorFood = new EnemyDetectorFood();
        _enemyDetectorFood.Init(this, _slime.UpgradingSlime);
        _transform = GetComponent<Transform>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _enemyStateMachine.Init(path);
        _enemyMovement = new EnemyMovement();
        _enemyMovement.Init(_transform);
        _slime.NewItemWasEaten += _enemyDetectorFood.SetNearbyRandomTarget;
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
        _slime.NewItemWasEaten -= _enemyDetectorFood.SetNearbyRandomTarget;
    }

    public void ResetCountScoreForUpgrade()
    {
        _countScoreForUpgrade = 0;
    }
}
