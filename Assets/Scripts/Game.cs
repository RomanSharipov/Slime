using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Map _mapTemplate;
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private Transform _spawnPointMap;
    [SerializeField] private SpawnerText _spawnerText;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Vehicle[] _vehiclesTemplate;
    [SerializeField] private UpdatingPositionCamera _updatingPositionCamera;
    [SerializeField] private float _distanceBetweenVehicles;

    private float _startDistanceTraveled;
    private Player _player;
    private Map _map;
    private List<Enemy> _enemies = new List<Enemy>();
    private List<Vehicle> _vehicles = new List<Vehicle>();

    private void Awake()
    {
        StartGame();
    }

    public void Restart()
    {
        DestroyAllObjects();
        StartGame();
        _updatingPositionCamera.ResetPosition();
    }

    public void StartGame()
    {
        _map = Instantiate(_mapTemplate, _spawnPointMap.position, _spawnPointMap.rotation);
        _player = Instantiate(_playerTemplate, _map.SpawnPointPlayer.position, _map.SpawnPointPlayer.rotation);
        _player.Init(_joystick);
        _spawnerText.Init(_player);
        _updatingPositionCamera.Init(_player);

        foreach (var spawnPointEnemy in _map.SpawnPointsEnemies)
        {
            Enemy enemy = Instantiate(_enemyTemplate, spawnPointEnemy.position, spawnPointEnemy.rotation);
            enemy.Init(_player, _map.EnemyPath);
            _enemies.Add(enemy);
        }

        foreach (var _vehicleTemplate in _vehiclesTemplate)
        {
            Vehicle vehicle = Instantiate(_vehicleTemplate);
            vehicle.Init(_map.PathCreator, _startDistanceTraveled);
            _vehicles.Add(vehicle);
            _startDistanceTraveled += _distanceBetweenVehicles;
        }
    }

    public void DestroyAllObjects()
    {
        Destroy(_map.gameObject);
        if (_player != null)
            Destroy(_player.gameObject);

        foreach (var vehicle in _vehicles)
        {
            if (vehicle != null)
                Destroy(vehicle.gameObject);
        }
        _vehicles = new List<Vehicle>();

        foreach (var enemy in _enemies)
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }
        _enemies = new List<Enemy>();
    }
}
