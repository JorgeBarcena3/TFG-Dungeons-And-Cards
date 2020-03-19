using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum OPTIONS
{
    PLAY,
    DECK,
    SETTINGS,
    EXIT
};

public class ButtonPlay : MonoBehaviour
{

    public OPTIONS my_option;
    Vector3 initPosition;
    Vector3 centerPosition;
    public GameObject deck_menu;

    // Start is called before the first frame update
    void Start()
    {
        SwipeDetector.OnSwipe += action;
        initPosition = gameObject.transform.position;
        centerPosition = new Vector3(0,0,gameObject.transform.position.z);
        
    }

    public void action(SwipeData data)
    {
        if (GameManager.Instance.state == States.INMENU && isShowingInfo())
        {
            if (data.Direction == SwipeDirection.Up)
            {
                switch ((int)my_option) 
                {
                    case 0:
                        play();
                        break;
                    case 1:
                        deck();
                        break;
                    case 2:
                        settings();
                        break;
                    case 3:
                        exit();
                        break;
                }
            }
            if (data.Direction == SwipeDirection.Down)
            {
                HideInfo();
            }
            else if (
              data.Direction == SwipeDirection.Right)
            {
                @switch();
            }
        }
        
       
    }

    bool isShowingInfo()
    {
        return gameObject.transform.position.x != initPosition.x &&
               gameObject.transform.position.y != initPosition.y;
    }
    void ShowInfo() 
    {
        if (!isShowingInfo())
        {
            StartCoroutine(AuxiliarFuncions.MoveObjectTo(gameObject.transform, centerPosition));
            StartCoroutine(AuxiliarFuncions.SetLocalScaleProgresive(gameObject.transform, gameObject.transform.localScale * 2.5f));
        }
       
      
    }
    public void HideInfo() 
    {

        if (isShowingInfo())
        {
            StartCoroutine(AuxiliarFuncions.MoveObjectTo(gameObject.transform, initPosition));
            StartCoroutine(AuxiliarFuncions.SetLocalScaleProgresive(gameObject.transform, gameObject.transform.localScale / 2.5f));
        }      

    }
    void @switch() 
    {
    }

    void play() 
    {
        Initiate.Fade("Main", Color.black, 2.0f);
    }


    void deck() 
    {
        StartCoroutine(AuxiliarFuncions.MoveObjectTo2D(deck_menu.transform, new Vector2(0,1),1f));
    
    }
    void settings() 
    {

    }
    void exit() 
    {
        Application.Quit();
    }

    private void OnMouseDown() 
    {
        if(deck_menu.transform.position.y < -10f)
            ShowInfo();
    
    }


    private void OnDestroy()
    {
        SwipeDetector.OnSwipe -= action;
    }

}
