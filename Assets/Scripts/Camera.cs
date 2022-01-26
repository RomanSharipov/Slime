using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Vector3 _upgradingStepOffset;
    [SerializeField] private LayerMask _itemLayerMask;
    [SerializeField] private float _maxDistance;

    private Vector3 _startOffset;
    private Vector3 _directionRay;
    private CinemachineTransposer _cinemachineTransposer;
    private Player _player;
    private RaycastHit _hit;
    private Item _detectedItem;

    private void Start()
    {
        _startOffset = _cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        if (_player == null)
            return;

        _directionRay = _player.Transform.position - transform.position;

        if (Physics.Raycast(transform.position, _directionRay, out _hit, _maxDistance, _itemLayerMask))
        {
            if (_hit.collider.gameObject.TryGetComponent(out Item item))
            {
                _detectedItem = item;
                _detectedItem.SetTransparentMaterial();
            }
        }

        else
        {
            if (_detectedItem != null) 
            {
                _detectedItem.SetStartMaterial();
            }
        }
    }

    public void Init(Player player)
    {
        _player = player;
        _camera.Follow = player.Transform;
        _camera.LookAt = player.Transform;
        _cinemachineTransposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        _player.Slime.SlimeWasUpgraded += UpdatePosition;
    }

    private void UpdatePosition()
    {
        _cinemachineTransposer.m_FollowOffset += _upgradingStepOffset;
    }

    private void OnDisable()
    {
        _player.Slime.SlimeWasUpgraded -= UpdatePosition;
    }

    public void ResetPosition()
    {
        _cinemachineTransposer.m_FollowOffset = _startOffset;
    }
}
