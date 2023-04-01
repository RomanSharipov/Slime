using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrowning  : MonoBehaviour
{
    [SerializeField] private float _destroyPositionY = -2;
    [SerializeField] private float _percentReduceScale = 0.6f;
    [SerializeField] private float _speedDrown = 6;
    [SerializeField] private float _speedReduceScale = 0.1f;

    private Transform _transform;

    public void Init(Transform transform)
    {
        _transform = transform;
    }

    private IEnumerator DrownSmoothly(Slime slime)
    {
        Vector3 targetPosition = new Vector3();
        Vector3 targetScale = _transform.localScale * _percentReduceScale;
        float targetPositionY;
        while (_transform.position.y > _destroyPositionY)
        {
            targetPositionY = _transform.position.y - Time.deltaTime * _speedDrown;
            targetPosition.Set(slime.transform.position.x, targetPositionY, slime.transform.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, Time.deltaTime * _speedDrown);
            _transform.localScale = Vector3.MoveTowards(_transform.localScale, targetScale, Time.deltaTime * _speedReduceScale);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Drown(Slime slime)
    {
        StartCoroutine(DrownSmoothly(slime));
    }
}
