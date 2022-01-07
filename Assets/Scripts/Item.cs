using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    [SerializeField] private int _requiredLevel;
    [SerializeField] private float _destroyPositionY;
    [SerializeField] private float _speedDrown;

    [SerializeField] private float _forceReduceScale = 0.8f;
    [SerializeField] private float _speedReduceScale;

    public int RequiredLevel => _requiredLevel;

    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private Material _startMaterial;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _startMaterial = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TryEat(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _meshRenderer.material = _startMaterial;
        }
    }

    public virtual void Die(Player player)
    {
        _transform.parent = player.Transform;
    }

    public IEnumerator Drown(Player player)
    {
        Vector3 targetPosition = new Vector3();
        Vector3 targetScale = _transform.localScale * _forceReduceScale;
        float targetPositionY;
        while (_transform.position.y > _destroyPositionY)
        {
            targetPositionY = _transform.position.y - Time.deltaTime * _speedDrown;
            targetPosition.Set(player.transform.position.x, targetPositionY, player.transform.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, Time.deltaTime * _speedDrown);
            _transform.localScale = Vector3.MoveTowards(_transform.localScale, targetScale, Time.deltaTime * _speedReduceScale);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void SetTransparentMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
