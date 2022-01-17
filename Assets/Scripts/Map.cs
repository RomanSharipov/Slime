using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPointsEnemies;
    [SerializeField] private Transform _spawnPointPlayer;
    [SerializeField] private Transform _enemyPath;
    [SerializeField] private PathCreator _pathCreator;

    public Transform[] SpawnPointsEnemies => _spawnPointsEnemies;
    public Transform SpawnPointPlayer => _spawnPointPlayer;
    public Transform EnemyPath => _enemyPath;
    public PathCreator PathCreator => _pathCreator;
}
