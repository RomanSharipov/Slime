using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Item : MonoBehaviour, IEatable
{
    [SerializeField] private int _requiredLevel;
    [SerializeField] private int _reward;
    [SerializeField] private float _destroyPositionY;
    [SerializeField] private float _speedDrown;
    [SerializeField] private float _forceReduceScale = 0.8f;
    [SerializeField] private float _speedReduceScale;
    [SerializeField] private Material _transparentMaterial;

    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private Material _startMaterial;
    private BoxCollider _boxCollider;

    public int RequiredLevel => _requiredLevel;
    public int Reward => _reward;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _transform = GetComponent<Transform>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _startMaterial = _meshRenderer.material;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Slime slime))
        {
            _meshRenderer.material = _startMaterial;
        }
    }

    public IEnumerator Drown(Slime slime)
    {
        Vector3 targetPosition = new Vector3();
        Vector3 targetScale = _transform.localScale * _forceReduceScale;
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

    public void SetTransparentMaterial()
    {
        _meshRenderer.material = _transparentMaterial;
    }

    public virtual void BeEaten(Slime slime)
    {
        _boxCollider.enabled = false;
        _transform.parent = slime.Transform;
        StartCoroutine(Drown(slime));
    }

    public void BeNotEaten(Slime slime)
    {
        SetTransparentMaterial();
    }

    public void SetStartMaterial()
    {
        _meshRenderer.material = _startMaterial;
    }
}
