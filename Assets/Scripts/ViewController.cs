using UnityEngine;

public class ViewController : MonoBehaviour
{
    Vector2 _mouseOrigin;
    Vector2 _touchOrigin;
    float _swipeDelta = 100f;

    [SerializeField]
    float _rotationSpeed = 10;

    [SerializeField]
    bool _invertYAxis = false;
    private void Update()
    {
        if (Application.isEditor)
            EditorInput();
        else
            MobileInput();
    }

    void EditorInput()
    {
        Vector2 inputPosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) _mouseOrigin = inputPosition;
        else if (Input.GetMouseButton(0))
        {
            ComputeSwipe(inputPosition, _mouseOrigin);
        }
    }

    void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began) _touchOrigin = Input.touches[0].position;
            else if (Input.touches[0].phase != TouchPhase.Ended && Input.touches[0].phase != TouchPhase.Canceled)
            {
                Vector2 inputPosition = Input.touches[0].position;
                ComputeSwipe(inputPosition, _touchOrigin);
            }
        }
    }

    void ComputeSwipe(Vector3 inputDestination, Vector3 inputOrigin)
    {
        Vector2 delta = inputDestination - inputOrigin;
        if(Mathf.Abs(delta.x) >= _swipeDelta)
        {
            transform.Rotate(0, _rotationSpeed * Mathf.Sign(delta.x) * Time.deltaTime, 0);
        }

        if(Mathf.Abs(delta.y) >= _swipeDelta)
        {
            float speed = _rotationSpeed * Mathf.Sign(delta.y);
            speed *= (_invertYAxis) ? -1 : 1;
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        }
    }
}
