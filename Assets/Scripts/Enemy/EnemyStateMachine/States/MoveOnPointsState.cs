using UnityEngine;

public class MoveOnPointsState : State
{
    [SerializeField] private Transform _path;
    [SerializeField] private int _speed;
    [SerializeField] private float _rotationSpeed;

    private Transform[] _points;
    private int _currentPoint;

    public void InitPath(Transform path)
    {
        _path = path;
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }
    
    private void Update()
    {
        Transform targetPoint = _points[_currentPoint];

        Enemy.EnemyMovement.MoveTo(targetPoint.position, _speed, _rotationSpeed);

        if (_transform.position.x == targetPoint.position.x && _transform.position.z == targetPoint.position.z)
        {
            _currentPoint++;
            if (_currentPoint == _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}
