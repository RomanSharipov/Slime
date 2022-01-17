using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float heightY;
    [SerializeField] private float offsetZ;
    [SerializeField] private float _stepOffsetZ;
    [SerializeField] private float _stepHeightY;
    [SerializeField] private float _speedUpdatePosition;

    private Transform _transform;

    public void Init(Transform startPoint,Player player)
    {
        _transform = GetComponent<Transform>();
        _player = player;
        _transform.position = startPoint.position;
        _transform.rotation = startPoint.rotation;
        offsetZ = _transform.position.z - _player.CurrentPosition.z;
        heightY = _transform.position.y - _player.CurrentPosition.y;
        _player.Slime.SlimeWasUpgraded += UpdatePositionCamera;
    }

    private void Update()
    {
        if (_player == null)
            return;
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

    private void OnDisable()
    {
        _player.Slime.SlimeWasUpgraded -= UpdatePositionCamera;
    }
}
