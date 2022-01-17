using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Slime))]

public class Player : MonoBehaviour, ICountable
{
    [SerializeField] private int _score;
    [SerializeField] private int _countScoreForUpgrade;

    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;
    private Slime _slime;
    private Transform _transform;

    public int Score => _score;
    public int CountScoreForUpgrade => _countScoreForUpgrade;
    public PlayerInput PlayerInput => _playerInput;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public Slime Slime => _slime;
    
    public Transform Transform => _transform;
    public Vector3 CurrentPosition => _transform.position;

    public event UnityAction<int> AddedScore;
    public event UnityAction Died;

    public void Init(Joystick joystick)
    {
        _transform = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.Init(joystick);
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
        _slime = GetComponent<Slime>();
        _slime.Init();
        _playerInput.Walked += _slime.TryCreateBlot;
    }

    public void AddScore(int reward)
    {
        _countScoreForUpgrade++;
        _score += reward;
        AddedScore?.Invoke(reward);
    }

    private void OnDisable()
    {
        _playerInput.Walked -= _slime.TryCreateBlot;
    }

    public void ResetCountScoreForUpgrade()
    {
        _countScoreForUpgrade = 0;
    }

    private void OnDestroy()
    {
        Died?.Invoke();
    }
}
