using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;

    public PlayerInput PlayerInput => _playerInput;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAnimator PlayerAnimator => _playerAnimator;

    public void Init()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Init();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerAnimator.Init();
    }
}
