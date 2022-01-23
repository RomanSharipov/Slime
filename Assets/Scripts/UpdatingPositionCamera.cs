using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatingPositionCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Vector3 _upgradingStepOffset;

    [SerializeField] private Vector3 _startOffset ;
    private CinemachineTransposer _cinemachineTransposer;
    private Player _player;


    private void Awake()
    {
        _startOffset = _cinemachineTransposer.m_FollowOffset;
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
