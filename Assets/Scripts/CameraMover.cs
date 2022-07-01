using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _zBound;
    [SerializeField] private float _xBound;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;
    private Camera _camera;
    private Vector3 _startPosition;
    private Vector3 _lastMousePosition;

    private void Awake()
    {
        _camera = Camera.main;
        _startPosition = _camera.transform.position;
    }

    private void OnEnable()
    {
        _camera.transform.position = _startPosition;
    }

    private void OnMouseDown()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, _layerMask))
        {
            _lastMousePosition = raycastHit.point;
        }
    }

    private void OnMouseDrag()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var raycastHit, float.MaxValue, _layerMask))
            return;

        var inputPosition = raycastHit.point;
        var direction = inputPosition - _lastMousePosition;
        direction.Normalize();
        var newPosition = _camera.transform.position - direction * _speed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -_xBound, _xBound);
        newPosition.z = Mathf.Clamp(newPosition.z, -_zBound, _zBound);
        _camera.transform.position = newPosition;
        _lastMousePosition = inputPosition;
    }
}