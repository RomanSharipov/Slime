using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    public void Init()
    {
        _player = GetComponent<Player>();
        _player.PlayerInput.Walked += Move;
        _player.PlayerInput.Stopped += Stop;
        _animator = GetComponent<Animator>();
    }

    private void Move(Vector2 direction)
    {
        _animator.SetBool("Walk",true);
    }

    private void Stop()
    {
        _animator.SetBool("Walk", false);
    }

    private void OnDisable()
    {
        _player.PlayerInput.Walked -= Move;
        _player.PlayerInput.Stopped -= Stop;
    }
}
