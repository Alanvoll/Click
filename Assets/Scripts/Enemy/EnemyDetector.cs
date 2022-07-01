using UnityEngine;


public class EnemyDetector : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Enemy _target;
    [SerializeField] private GameObject _image;

    private void Awake()
    {
        _camera = Camera.main;
        _image.SetActive(false);
        GameController.OnGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        Enemy.OnDeath -= OnEnemyDeath;
        GameController.OnGameEnd -= OnGameEnd;
    }

    private void Update()
    {
        if (_target == null)
            return;

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

    public void SetTarget(Enemy enemy)
    {
        _target = enemy;
        Enemy.OnDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
       if(enemy != _target)
           return;
       
       Destroy(gameObject);
    }

    private void OnGameEnd()
    {
        Destroy(gameObject);
    }
}