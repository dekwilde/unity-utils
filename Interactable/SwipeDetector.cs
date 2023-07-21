using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    public UnityEvent InvokeUp;
    public UnityEvent InvokeDown;
    public UnityEvent InvokeLeft;
    public UnityEvent InvokeRight;

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform)
        {
            // Código para detectar swipes em dispositivos móveis
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                }

                // Detecta o swipe enquanto o dedo ainda está se movendo
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                }

                // Detecta o swipe após o dedo ser solto
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }
        }
        else
        {
            // Código para detectar swipes usando o mouse no desktop
            if (Input.GetMouseButtonDown(0))
            {
                fingerUp = Input.mousePosition;
                fingerDown = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = Input.mousePosition;
                    checkSwipe();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                fingerDown = Input.mousePosition;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        // Checa o swipe vertical
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0) // swipe para cima
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0) // swipe para baixo
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }

        // Checa o swipe horizontal
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0) // swipe para a direita
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0) // swipe para a esquerda
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        // Nenhum movimento detectado
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
        InvokeUp.Invoke();
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        InvokeDown.Invoke();
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        InvokeLeft.Invoke();
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        InvokeRight.Invoke();
    }
}
