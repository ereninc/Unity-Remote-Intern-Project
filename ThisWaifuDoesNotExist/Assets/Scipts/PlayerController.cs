using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlayerController : MonoBehaviour
{
    private Touch _touch;
    private Vector2 _touchStartPos, _touchEndPos;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 5.0f;
    private float _xDistance, _yDistance;
    private bool _isStarted = false;
    
    void Update()
    {
        Move();
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        TouchInput();
    }

    void Move()
    {
        if (_isStarted)
        {
            transform.Translate(0, 0, 1 * playerSpeed * Time.deltaTime);
        }
    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _isStarted = true;
                _touchStartPos = _touch.position;
            }
            else if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
            {
                _touchEndPos = _touch.position;
                _xDistance = _touchEndPos.x - _touchStartPos.x;
                _yDistance = _touchEndPos.y - _touchStartPos.y;

                if (Mathf.Abs(_xDistance) > Mathf.Abs(_yDistance))
                {
                    if (_xDistance > 0)
                    {
                        transform.Rotate(0, 1 * rotateSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        transform.Rotate(0, -1 * rotateSpeed * Time.deltaTime, 0);
                    }
                }
            }
        }
    }
}
