using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected bool _needTransit;
    protected Enemy _enemy;
    protected Transform TargetFood { get; private set; }
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
        TargetFood = target;
    }
}
