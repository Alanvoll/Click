using UnityEngine;

public class BoosterDetector : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Booster _target;
    [SerializeField] private GameObject _image;
    [SerializeField] private FillBar _timeBar;

    private void Awake()
    {
        _camera = Camera.main;
        _image.SetActive(false);
    }
    
    private void Update()
    {
        if (_target == null)
            return;

        _timeBar.SetAmount(_target.CurrentLifeTime / _target.MAXLifeTime);
        var targetScreenCoords = _camera.WorldToScreenPoint(_target.transform.position);
        if (targetScreenCoords.x < 0 || targetScreenCoords.x > _camera.pixelWidth ||
            targetScreenCoords.y < 0 || targetScreenCoords.y > _camera.pixelHeight)
        {
            _image.SetActive(true);
            var imagePosition = new Vector3
            {
                x = Mathf.Clamp(targetScreenCoords.x, 50, _camera.pixelWidth - 50),
                y = Mathf.Clamp(targetScreenCoords.y, 50, _camera.pixelHeight - 50)
            };
            transform.position = imagePosition;

            var direction = targetScreenCoords - transform.position;
            direction.Normalize();
            transform.right = direction;
        }
        else
        {
            _image.SetActive(false);
        }
    }

    public void SetTarget(Booster booster)
    {
        _target = booster;
        Booster.OnRelease += OnBoosterRelease;
    }

    private void OnBoosterRelease(Booster booster)
    {
        if (booster != _target)
            return;

        Booster.OnRelease -= OnBoosterRelease;
        Destroy(gameObject);
    }
}