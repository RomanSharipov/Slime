using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class State : MonoBehaviour
{
    protected Transform _transform;
    protected Enemy _enemy;

    [SerializeField] private List<Transition> _transitions;

    public Transform Target { get; protected set; }
    public Enemy Enemy => _enemy;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        _enemy = GetComponent<Enemy>();
    }

    public void Enter(Transform target)
    {
        if (enabled == false)
        {
            Target = target;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }
}
