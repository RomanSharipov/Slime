using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(UpgradingSlime))]
public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blot;
    [SerializeField] private Transform _blotSpawnPoint;
    [SerializeField] private float _delayBetweenSpawnBlot;
    [SerializeField] private int _score;
    [SerializeField] private Material _transparentMaterial;

    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;
    private Transform _transform;
    private UpgradingSlime _upgradingSlime;
    private float _lastSpawnTime;

    public PlayerInput PlayerInput => _playerInput;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public UpgradingSlime UpgradingSlime => _upgradingSlime;
    public Transform Transform => _transform;
    public Vector3 CurrentPosition => _transform.position;

    public event UnityAction SlimeWasUpgraded;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
        _upgradingSlime = GetComponent<UpgradingSlime>();
        _upgradingSlime.Init();
        _playerInput.Walked += TryCreateBlot;
    }

    private void TryCreateBlot(Vector2 direction)
    {
        if (_lastSpawnTime <= 0)
        {
            ParticleSystem blot = Instantiate(_blot, _transform);
            blot.transform.parent = null;
            blot.transform.position = _blotSpawnPoint.position;
            _lastSpawnTime = _delayBetweenSpawnBlot;
        }
        _lastSpawnTime -= Time.deltaTime;
    }

    public void TryEat(Item item)
    {
        if (_upgradingSlime.LevelSlime >= item.RequiredLevel)
        {
            Eat(item);
            _score++;
            if (_score % UpgradingSlime.CountScoreForUpgrade == 0)
            {
                SlimeWasUpgraded?.Invoke();
            }
        }
        else
        {
            item.SetTransparentMaterial(_transparentMaterial);
        }
    }

    private void Eat(Item item)
    {
        item.Die(this);
        StartCoroutine(item.Drown(this));
    }
}
