using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ICountable
{
    [SerializeField] private int _score;
    [SerializeField] private Player _target;
    [SerializeField] private LayerMask _layerMask;

    private UpgradingSlime _upgradingSlime;
    private Slime _slime;
    private Vector2 _direction = new Vector2();

    public Player Target => _target; 
    public UpgradingSlime UpgradingSlime => _upgradingSlime; 
    public Slime Slime => _slime; 
    public event UnityAction Died;
    public int Score => _score;
    public LayerMask LayerMask => _layerMask;

    private void Awake()
    {
        _upgradingSlime = GetComponent<UpgradingSlime>();
        _upgradingSlime.Init();
        _slime = GetComponent<Slime>();
        _slime.Init();
        _slime.ItemWasEaten += AddScore;
    }

    private void Update()
    {
        _slime.TryCreateBlot(_direction);
    }

    private void AddScore(Item item)
    {
        _score += item.Reward;
    }
}
