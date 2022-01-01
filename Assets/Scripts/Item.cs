using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _requiredScore;
    [SerializeField] private float _destroyPositionY;
    [SerializeField] private float _speedDrown;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _speedReduceScale;

    public int RequiredScore => _requiredScore;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TryEat(this);
        }
    }

    public void Die(Player player)
    {
        _transform.parent = player.Transform;
    }

    public IEnumerator Drown(Player player)
    {
        Vector3 targetPosition = new Vector3();
        float targetPositionY;
        while (_transform.position.y > _destroyPositionY)
        {
            targetPositionY = _transform.position.y - Time.deltaTime * _speedDrown;
            targetPosition.Set(player.transform.position.x, targetPositionY, player.transform.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, Time.deltaTime * _speedDrown);
            yield return null;
        }
        Destroy(gameObject);
    }
}
