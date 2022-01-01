using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Transform _transform;
    [SerializeField] private float heightY;
    [SerializeField] private float offsetZ;
    [SerializeField] private float _stepOffsetZ;
    [SerializeField] private float _stepHeightY;
    [SerializeField] private float _speedUpdatePosition;

    public void Init()
    {
        _transform = GetComponent<Transform>();
        offsetZ = _transform.position.z - _player.CurrentPosition.z;
        heightY = _transform.position.y - _player.CurrentPosition.y;
        _player.SlimeWasUpgraded += UpdatePositionCamera;
    }

    private void Update()
    {
        _transform.position = new Vector3(_player.CurrentPosition.x, heightY, _player.CurrentPosition.z + offsetZ);
    }

    private void UpdatePositionCamera()
    {
        StartCoroutine(UpdatePositionCameraSmoothly());
    }

    private IEnumerator UpdatePositionCameraSmoothly()
    {
        float targetPositionY = heightY + _stepHeightY;
        float targetPositionZ = offsetZ - _stepOffsetZ;

        while (heightY < targetPositionY) 
        {
            offsetZ = Mathf.MoveTowards(offsetZ, targetPositionZ, Time.deltaTime * _speedUpdatePosition);
            heightY = Mathf.MoveTowards(heightY, targetPositionY, Time.deltaTime * _speedUpdatePosition);
            yield return null;
        }
    }
}
