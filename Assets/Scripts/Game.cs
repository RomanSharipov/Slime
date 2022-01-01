using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerCamera _camera;

    private void OnEnable()
    {
        _player.Init();
        _camera.Init();
    }
}
