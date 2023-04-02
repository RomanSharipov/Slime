using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private IInput _input;

    public event UnityAction<Vector2> Walked;
    public event UnityAction Stopped;

    private Vector2 _direction = new Vector2();

    public void Init(IInput input)
    {
        _input = input;
    }

    private void Update()
    {
        _direction.Set(_input.Horizontal, _input.Vertical);

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
