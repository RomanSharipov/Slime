using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    public event UnityAction<Vector2> Walked;
    public event UnityAction Stopped;

    private Vector2 _direction = new Vector2();

    public void Init(Joystick joystick)
    {
        _joystick = joystick;
    }

    private void Update()
    {
        _direction.Set(_joystick.Horizontal, _joystick.Vertical);

        if (_direction != Vector2.zero)
        {
            Walked?.Invoke(_direction);
        }
        else
        {
            Stopped?.Invoke();
        }
    }
}
