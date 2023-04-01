using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ItemDrowning))]
public class Item : MonoBehaviour, IEatable
{
    [SerializeField] private int _requiredLevel;
    [SerializeField] private int _reward;
    [SerializeField] private Material _transparentMaterial;

    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private Material _startMaterial;
    private BoxCollider _boxCollider;
    private ItemDrowning _drowningItem;

    public int RequiredLevel => _requiredLevel;
    public int Reward => _reward;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _transform = GetComponent<Transform>();
        _drowningItem = GetComponent<ItemDrowning>();
        _drowningItem.Init(_transform);
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

    public void SetTransparentMaterial()
    {
        _meshRenderer.material = _transparentMaterial;
    }

    public virtual void BeEaten(Slime slime)
    {
        _boxCollider.enabled = false;
        _transform.parent = slime.Transform;
        _drowningItem.Drown(slime);
    }

    public void SetStartMaterial()
    {
        _meshRenderer.material = _startMaterial;
    }
}
