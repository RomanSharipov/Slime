using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private State _currentState;

    private Enemy _enemy;
    private State[] _states;
    private MoveOnPointsState _moveOnPointsState;

    public State Current => _currentState;

    public void Init(Transform path)
    {
        _enemy = GetComponent<Enemy>();
        Reset(_firstState);
        _states = GetComponents<State>();
        _moveOnPointsState = GetComponent<MoveOnPointsState>();
        _moveOnPointsState.InitPath(path);

        foreach (var state in _states)
        {
            state.Init();
        }
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
            _currentState.Enter();
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
            _currentState.Enter();
        }
    }
}
