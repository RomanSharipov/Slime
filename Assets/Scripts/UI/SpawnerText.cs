using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerText : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private UnityEngine.Camera _camera;
    [SerializeField] private RewardText _rewardText;
    [SerializeField] private float _delayBeforeDestroy;

    private Player _player;
    private Vector3 _pointNewText = new Vector3();

    public void Init(Player player)
    {
        _player = player;
        _player.AddedScore += CreateText;
    }

    public void CreateText(int reward)
    {
        _pointNewText = Random.insideUnitSphere + _spawnPoint.position;
        RewardText text  = Instantiate(_rewardText, _pointNewText, Quaternion.identity, _camera.transform);
        
        text.SetText($"+{reward}");
        Destroy(text.gameObject, _delayBeforeDestroy);
    }
}
