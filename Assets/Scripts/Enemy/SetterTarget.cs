using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetterTarget : MonoBehaviour
{
    [SerializeField] private Player _player;

    private MoveToFoodState _moveToFoodState;

    private void Awake()
    {
        _moveToFoodState = GetComponent<MoveToFoodState>();
    }
}
