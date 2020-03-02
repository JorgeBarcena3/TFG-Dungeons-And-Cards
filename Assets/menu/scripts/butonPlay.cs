using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class butonPlay : MonoBehaviour
{
    public enum Options 
    {
        PLAY,
        DECK,
        SETTINGS,
        EXIT
    };
    public Options my_option;
    public butonPlay [] others; 
    Vector3 initPosition;
    Vector3 centerPosition;
    [HideInInspector]
    public bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        SwipeDetector.OnSwipe += action;
        pressed = false;
        initPosition = gameObject.transform.position;
        centerPosition = new Vector3(0,0,gameObject.transform.position.z);
        
    }

    void action(SwipeData data)
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
            pressed = false;
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
        //SceneManager.LoadScene("Main");
        
    }
    void deck() 
    {

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
        ShowInfo();
        pressed = true;
        for (int i = 0; i < others.Length; i++) 
        {
            others[i].pressed = false;
            others[i].HideInfo();
        }
    }
    private void OnMouseUp()
    {
        pressed = false;
    }
   
}
