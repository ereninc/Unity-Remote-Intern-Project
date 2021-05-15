using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Touch _touch;
    private Vector2 _touchStartPos, _touchEndPos;
    public float playerSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 5.0f;
    private float _xDistance, _yDistance;
    public bool isStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        this.transform.Rotate(0, 0, 0);
    }

    void Update()
    {
        Move();
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        TouchInput();
    }

    void Move()
    {
        if (isStarted)
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
                isStarted = true;
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
