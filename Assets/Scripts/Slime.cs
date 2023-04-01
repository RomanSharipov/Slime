using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UpgradingSlime))]
public class Slime : MonoBehaviour,IEatable
{
    [SerializeField] private ParticleSystem _blotTemplate;
    [SerializeField] private ParticleSystem _destroySplashTemplate;
    [SerializeField] private Transform _splashSpawnPoint;
    [SerializeField] private float _delayBetweenSpawnBlot;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _offsetOnGround;

    private float _lastSpawnTime;
    private Transform _transform;
    private UpgradingSlime _upgradingSlime;
    private ICountable _countable;
    private Vector3 _blotSpawnPointPosition = new Vector3();

    public Transform Transform => _transform;
    public int RequiredLevel => _upgradingSlime.LevelSlime;
    public int Reward => _upgradingSlime.LevelSlime;
    public UpgradingSlime UpgradingSlime => _upgradingSlime;

    public event UnityAction SlimeWasUpgraded;
    public event UnityAction NewItemWasEaten;

    public void Init()
    {
        _countable = GetComponent<ICountable>();
        _transform = GetComponent<Transform>();
        _upgradingSlime = GetComponent<UpgradingSlime>();
        _upgradingSlime.Init();
    }

    public void TryCreateBlot(Vector2 direction)
    {
        if (_lastSpawnTime <= 0)
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _groundLayerMask))
            {
                _blotSpawnPointPosition = new Vector3( hit.point.x,hit.point.y + _offsetOnGround, hit.point.z);
                ParticleSystem blot = Instantiate(_blotTemplate, _transform);
                
                blot.transform.parent = null;
                blot.transform.position = _blotSpawnPointPosition;
                _lastSpawnTime = _delayBetweenSpawnBlot;
            }
        }
        _lastSpawnTime -= Time.deltaTime;
    }

    public void TryEat(IEatable eatable)
    {
        if (_upgradingSlime.LevelSlime > eatable.RequiredLevel)
        {
            eatable.BeEaten(this);
            _countable.AddScore(eatable.Reward);
            NewItemWasEaten?.Invoke();
            if (_countable.RequiredScoreForUpgrade == UpgradingSlime.CountScoreForUpgrade)
            {
                SlimeWasUpgraded?.Invoke();
                _countable.ResetCountScoreForUpgrade();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEatable eatable))
        {
            TryEat(eatable);
        }
    }

    public void BeEaten(Slime slime)
    {
        ParticleSystem splash = Instantiate(_destroySplashTemplate, _transform);
        splash.gravityModifier = _transform.localScale.x;
        splash.transform.parent = null;
        splash.transform.position = _splashSpawnPoint.position;
        Destroy(gameObject);
    }
}
