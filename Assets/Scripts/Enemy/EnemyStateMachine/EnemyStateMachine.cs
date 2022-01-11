using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private State _currentState;

    private Transform _target;
    private float _previousFramePosition;

    public State Current => _currentState;

    private void Awake()
    {
        _target = GetComponent<Enemy>().Target.transform;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        State nextState = _currentState.GetNextState();
        
        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
        {
            _currentState.Enter(_target);
        }
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter(_target);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
