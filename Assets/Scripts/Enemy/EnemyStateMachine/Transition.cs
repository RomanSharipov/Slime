using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected bool _needTransit;
    protected Transform Target { get; private set; }
    protected Enemy _enemy { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit => _needTransit;
    public Enemy Enemy => _enemy;

    protected void SwitchOnTransition()
    {
        _needTransit = true;
    }

    public void Init(Transform target)
    {
        _needTransit = false;
        _enemy = GetComponent<Enemy>();
        Target = target;
    }
}
