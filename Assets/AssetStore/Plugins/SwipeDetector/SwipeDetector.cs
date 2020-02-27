using System;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private bool useClick = false;

    private bool isClick = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Start()
    {

#if UNITY_EDITOR
        useClick = true;
#else
        useClick = false;
#endif   

    }

    private void Update()
    {
        if (GameManager.GetInstance().state == States.INGAME &&
            GameManager.GetInstance().turn == TURN.PLAYER)
        {

            if (useClick)
            {
                if (Input.GetMouseButtonDown(0) && !isClick)
                {
                    fingerUpPosition = Input.mousePosition;
                    fingerDownPosition = Input.mousePosition;
                    isClick = true;
                }
                else if (Input.GetMouseButtonUp(0) && isClick)
                {
                    fingerDownPosition = Input.mousePosition;
                    DetectSwipe();
                    isClick = false;
                }

            }
            else { 

                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerUpPosition = touch.position;
                        fingerDownPosition = touch.position;
                    }

                    if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                    {
                        fingerDownPosition = touch.position;
                        //DetectSwipe();
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        fingerDownPosition = touch.position;
                        DetectSwipe();
                    }
                }
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                SendSwipe(direction);
            }
            else
            {
                var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {

        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };

        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}