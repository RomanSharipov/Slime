using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected bool _needTransit;
    protected Enemy _enemy;
    protected Player _player;

    protected Transform Target { get; private set; }
    public State TargetState => _targetState;
    public bool NeedTransit => _needTransit;
    public Enemy Enemy => _enemy;
    public Player Player => _player;

    protected void SwitchOnTransition()
    {
        _needTransit = true;
    }

    public void Init(Transform target)
    {
        _needTransit = false;
        _enemy = GetComponent<Enemy>();
        _player = _enemy.Player;
        Target = target;
    }
}
