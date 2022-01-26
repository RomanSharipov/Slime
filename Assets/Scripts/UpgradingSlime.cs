using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingSlime : MonoBehaviour
{
    [SerializeField] private float _stepAddScale;
    [SerializeField] private float _offsetFromGroundY;
    [SerializeField] private float _speedGrowScale;
    [SerializeField] private int _levelSlime = 1;
    [SerializeField] private ParticleSystem _upgradingEffectTemplate;
    [SerializeField] private int _countScoreForUpgrade;
    [SerializeField] private int _frequencyFactorUpgradingSlime;

    private Vector3 _targetScale = new Vector3();
    private Slime _slime;
    public int LevelSlime => _levelSlime;
    public int CountScoreForUpgrade => _countScoreForUpgrade;

    public void Init()
    {
        _slime = GetComponent<Slime>();
        _slime.SlimeWasUpgraded += OnUpgradeSlimeLevel;
    }

    private void OnUpgradeSlimeLevel()
    {
        _countScoreForUpgrade += _frequencyFactorUpgradingSlime;
        _levelSlime++;
        _targetScale = new Vector3(_slime.Transform.localScale.x + _stepAddScale, _slime.Transform.localScale.y + _stepAddScale, _slime.Transform.localScale.z + _stepAddScale);
        StartCoroutine(UpgradeSlimeScale(_targetScale));
        ParticleSystem levelUpEffect = Instantiate(_upgradingEffectTemplate, transform);
        levelUpEffect.transform.localScale = _targetScale;
        _slime.Transform.position = new Vector3(_slime.Transform.position.x, _slime.Transform.position.y + _offsetFromGroundY, _slime.Transform.position.z);
    }

    private IEnumerator UpgradeSlimeScale(Vector3 targetScale)
    {
        while (_slime.Transform.localScale.y < targetScale.y)
        {
            _slime.Transform.localScale = Vector3.MoveTowards(_slime.Transform.localScale, targetScale, _speedGrowScale * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDisable()
    {
        _slime.SlimeWasUpgraded -= OnUpgradeSlimeLevel;
    }
}
