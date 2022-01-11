using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slime : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blot;
    [SerializeField] private Transform _blotSpawnPoint;
    [SerializeField] private float _delayBetweenSpawnBlot;
    [SerializeField] private Material _transparentMaterial;

    private float _lastSpawnTime;
    private Transform _transform;
    private UpgradingSlime _upgradingSlime;
    private ICountable _countable;

    public Transform Transform => _transform;
    public event UnityAction SlimeWasUpgraded;
    public event UnityAction<Item> ItemWasEaten;

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
            ParticleSystem blot = Instantiate(_blot, _transform);
            blot.transform.parent = null;
            blot.transform.position = _blotSpawnPoint.position;
            _lastSpawnTime = _delayBetweenSpawnBlot;
        }
        _lastSpawnTime -= Time.deltaTime;
    }

    public void TryEat(Item item)
    {
        if (_upgradingSlime.LevelSlime >= item.RequiredLevel)
        {
            Eat(item);
            ItemWasEaten?.Invoke(item);
            if (_countable.Score % UpgradingSlime.CountScoreForUpgrade == 0)
            {
                SlimeWasUpgraded?.Invoke();
            }
        }
        else
        {
            item.SetTransparentMaterial(_transparentMaterial);
        }
    }

    private void Eat(Item item)
    {
        item.Die(this);
        StartCoroutine(item.Drown(this));
    }
}
