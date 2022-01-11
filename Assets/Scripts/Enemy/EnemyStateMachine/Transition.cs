using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;
    
    protected EnemyStateMachine _enemyStateMachine;
    
    protected bool _needTransit;
    protected Transform Target { get; private set; }
    protected Enemy Enemy { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit => _needTransit;

    private void OnEnable()
    {
        _needTransit = false;
        Enemy = GetComponent<Enemy>();
    }

    protected void SwitchOnTransition()
    {
        _needTransit = true;
    }

    public void Init(Transform target)
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();

        Target = target;
    }
}
