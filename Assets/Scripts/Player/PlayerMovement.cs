using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Player _player;
    private Vector3 _direction;
    private Transform _transform;
    private Quaternion _rotation;

    public void Init()
    {
        _player = GetComponent<Player>();
        _player.PlayerInput.Walked += Move;
        _transform = GetComponent<Transform>();
    }

    private void Move(Vector2 direction)
    {
        _direction.Set(direction.x, 0, direction.y);
        _direction.Normalize();
        _transform.Translate(_direction * Time.deltaTime * _speed, Space.World);

        if (_direction != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_direction, Vector3.up);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _rotation, _rotationSpeed);
        }

    }

    private void OnDisable()
    {
        _player.PlayerInput.Walked -= Move;
    }
}
