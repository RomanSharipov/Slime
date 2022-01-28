using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorOverlapPlayer : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _itemLayerMask;

    private Player _player;
    private Vector3 _directionRay;
    private RaycastHit _hit;
    private Item _detectedItem;

    public void Init(Player player)
    {
        _player = player;
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
}
