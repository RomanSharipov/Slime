using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingSlime : MonoBehaviour
{
    [SerializeField] private float _stepAddScale;
    [SerializeField] private float _offsetFromGroundY;
    [SerializeField] private float _speedGrowScale;
    [SerializeField] private int _levelSlime = 1;

    private Player _player;
    public const float CountScoreForUpgrade = 7;
    public int LevelSlime => _levelSlime;
    public void Init()
    {
        _player = GetComponent<Player>();
        _player.SlimeWasUpgraded += OnUpgradeSlimeLevel;
    }


    private void OnUpgradeSlimeLevel()
    {
        _levelSlime++;
        StartCoroutine(UpgradeSlimeScale());
        _player.Transform.position = new Vector3(_player.Transform.position.x, _player.Transform.position.y + _offsetFromGroundY, _player.Transform.position.z);
    }

    private IEnumerator UpgradeSlimeScale()
    {
        Vector3 targetScale = new Vector3(_player.Transform.localScale.x + _stepAddScale, _player.Transform.localScale.y + _stepAddScale, _player.Transform.localScale.z + _stepAddScale);
        while (_player.Transform.localScale.y < targetScale.y)
        {
            _player.Transform.localScale = Vector3.MoveTowards(_player.Transform.localScale, targetScale, _speedGrowScale * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDisable()
    {
        _player.SlimeWasUpgraded -= OnUpgradeSlimeLevel;
    }
}
