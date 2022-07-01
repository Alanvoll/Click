using UnityEngine;
using UnityEngine.EventSystems;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, _layerMask))
        {
            transform.position = raycastHit.point;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}