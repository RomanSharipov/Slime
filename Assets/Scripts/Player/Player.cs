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

    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;
    private Slime _slime;
    private Transform _transform;

    public int Score => _score;
    public PlayerInput PlayerInput => _playerInput;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAnimator PlayerAnimator => _playerAnimator;
    public Slime Slime => _slime;
    
    public Transform Transform => _transform;
    public Vector3 CurrentPosition => _transform.position;


    public void Init()
    {
        _transform = GetComponent<Transform>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
        _slime = GetComponent<Slime>();
        _slime.Init();
        _playerInput.Walked += _slime.TryCreateBlot;
    }

    public void AddScore(Item item)
    {
        _score += item.Reward;
    }

    private void OnDisable()
    {
        _playerInput.Walked -= _slime.TryCreateBlot;
    }
}
