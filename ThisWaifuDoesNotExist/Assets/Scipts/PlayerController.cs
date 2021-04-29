using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Touch _touch;
    private Vector2 _touchStartPos, _touchEndPos;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 5.0f;
    
    void Update()
    {
        Move();
        TouchInput();
    }

    void Move()
    {
        transform.Translate(0, 0, 1 * playerSpeed * Time.deltaTime);
    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _touchStartPos = _touch.position;
            }
            else if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
            {
                _touchEndPos = _touch.position;
                float x = _touchEndPos.x - _touchStartPos.x;
                float y = _touchEndPos.y - _touchStartPos.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    Debug.Log("Tapped!");
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
                    {
                        Debug.Log("Right!");
                        transform.Rotate(0, 1 * rotateSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        Debug.Log("Left!");
                        transform.Rotate(0, -1 * rotateSpeed * Time.deltaTime, 0);
                    }
                }
                else if (Mathf.Abs(x) < Mathf.Abs(y))
                {
                    if (y > 0)
                    {
                        Debug.Log("Up!");
                    }
                    else
                    {
                        Debug.Log("Down!");
                    }
                }
            }
        }
    }
}
