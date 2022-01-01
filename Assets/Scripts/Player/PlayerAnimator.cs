using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Player _player;
    private Vector3 _startScale;

    public void Init()
    {
        _player = GetComponent<Player>();
        _player.PlayerInput.Walked += OnMove;
        _player.PlayerInput.Stopped += OnIdle;
        _animator = GetComponent<Animator>();
    }

    private void OnMove(Vector2 direction)
    {
        _animator.SetBool("Walk",true);
    }

    private void OnIdle()
    {
        _animator.SetBool("Walk", false);
    }

    private void OnDisable()
    {
        _player.PlayerInput.Walked -= OnMove;
        _player.PlayerInput.Stopped -= OnIdle;
    }
}
