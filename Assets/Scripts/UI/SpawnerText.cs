using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpawnerText : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private RewardText _rewardText;
    [SerializeField] private float _delayBeforeDestroy;

    private Vector3 _pointNewText = new Vector3();

    private void OnEnable()
    {
        _player.AddedScore += CreateText;
    }

    public void CreateText(int reward)
    {
        _pointNewText = Random.insideUnitSphere + _player.Transform.position;
        RewardText text  = Instantiate(_rewardText, _pointNewText, Quaternion.identity, _camera.transform);
        
        text.SetText($"+{reward}");
        Destroy(text.gameObject, _delayBeforeDestroy);
    }
}
