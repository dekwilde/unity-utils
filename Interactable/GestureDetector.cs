using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GestureDetector : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;

    public bool detectSwipeOnlyAfterRelease = false;
    public float SWIPE_THRESHOLD = 20f;

    [Header("Swipe Events")]
    public UnityEvent InvokeSwipeUp;
    public UnityEvent InvokeSwipeDown;
    public UnityEvent InvokeSwipeLeft;
    public UnityEvent InvokeSwipeRight;

    [Header("Press Events")]
    public UnityEvent InvokePressDown;
    public UnityEvent InvokePressUp;
    public UnityEvent InvokeClick;

    [Header("Move Events")]
    public UnityEvent InvokeMove;
    public UnityEvent InvokeDrag;

    private bool isDragging;
    private bool swipeDetected;

    void Update()
    {
        if (Application.isMobilePlatform)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }
    }

    // ===================== TOUCH =====================

    void HandleTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                    swipeDetected = false;
                    isDragging = true;
                    InvokePressDown.Invoke();
                    break;

                case TouchPhase.Moved:
                    InvokeMove.Invoke();
                    InvokeDrag.Invoke();

                    if (!detectSwipeOnlyAfterRelease)
                    {
                        fingerDown = touch.position;
                        CheckSwipe();
                    }
                    break;

                case TouchPhase.Ended:
                    fingerDown = touch.position;
                    CheckSwipe();
                    InvokePressUp.Invoke();

                    if (!swipeDetected)
                    {
                        InvokeClick.Invoke();
                    }

                    isDragging = false;
                    break;
            }
        }
    }

    // ===================== MOUSE =====================

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerUp = Input.mousePosition;
            fingerDown = Input.mousePosition;
            swipeDetected = false;
            isDragging = true;
            InvokePressDown.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            InvokeMove.Invoke();
            InvokeDrag.Invoke();

            if (!detectSwipeOnlyAfterRelease)
            {
                fingerDown = Input.mousePosition;
                CheckSwipe();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerDown = Input.mousePosition;
            CheckSwipe();
            InvokePressUp.Invoke();

            if (!swipeDetected)
            {
                InvokeClick.Invoke();
            }

            isDragging = false;
        }
    }

    // ===================== SWIPE =====================

    void CheckSwipe()
    {
        float vertical = VerticalMove();
        float horizontal = HorizontalMove();

        if (vertical > SWIPE_THRESHOLD && vertical > horizontal)
        {
            swipeDetected = true;

            if (fingerDown.y - fingerUp.y > 0)
                OnSwipeUp();
            else
                OnSwipeDown();

            fingerUp = fingerDown;
        }
        else if (horizontal > SWIPE_THRESHOLD && horizontal > vertical)
        {
            swipeDetected = true;

            if (fingerDown.x - fingerUp.x > 0)
                OnSwipeRight();
            else
                OnSwipeLeft();

            fingerUp = fingerDown;
        }
    }

    float VerticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float HorizontalMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    // ===================== CALLBACKS =====================

    void OnSwipeUp()
    {
        Debug.Log("Swipe Up");
        InvokeSwipeUp.Invoke();
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        InvokeSwipeDown.Invoke();
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        InvokeSwipeLeft.Invoke();
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        InvokeSwipeRight.Invoke();
    }
}
