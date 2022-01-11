using UnityEngine;

public class MoveOnPointsState : State
{
    [SerializeField] private Transform _path;
    [SerializeField] private int _speed;

    private Transform[] _points;
    private int _currentPoint;
    private float _previousFramePosition;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void FixedUpdate()
    {
        MoveOnPoint();
    }

    private void MoveOnPoint()
    {
        Transform _target = _points[_currentPoint];
        _transform.position = Vector3.MoveTowards(_transform.position, new Vector3(_target.position.x, _transform.position.y, _target.position.z), _speed * Time.deltaTime);
        _transform.LookAt(new Vector3(_target.position.x, _transform.position.y, _target.position.z));

        if (_transform.position.x == _target.position.x && _transform.position.z == _target.position.z)
        {
            _currentPoint++;
            if (_currentPoint == _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}
