using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNearbyTransition : Transition
{
    [SerializeField] private int _distanceToFood;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _timeFollowToPlayer;

    private List<Item> _availableItems = new List<Item>();
    private bool _playerNearby;
    private bool _playerIsTarget;
    public int DistanceToFood => _distanceToFood;

    private void Update()
    {
        _availableItems = Enemy.EnemyDetectorFood.GetNearbyAvailableItems();
        _playerNearby = Vector3.Distance(transform.position, Player.transform.position) < _distanceToPlayer;
        _playerIsTarget = Enemy.Target == Enemy.Player.Transform;

        if (_playerIsTarget)
            return;
        if (_playerNearby && Enemy.UpgradingSlime.LevelSlime > Player.Slime.UpgradingSlime.LevelSlime)
        {
            StartCoroutine(FollowPlayer());
            SwitchOnTransition();
            return;
        }

        if (_availableItems.Count == 0)
            return;

        if (Target == null)
            Enemy.EnemyDetectorFood.SetNearbyRandomTarget();

        SwitchOnTransition();
    }

    private IEnumerator FollowPlayer()
    {
        var waitForSeconds = new WaitForSeconds(_timeFollowToPlayer);
        Enemy.SetTarget(Player.transform);
        yield return waitForSeconds;
        Enemy.EnemyDetectorFood.SetNearbyRandomTarget();
    }
}
