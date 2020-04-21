using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageCanvasAnimator : MonoBehaviour
{

    // set the controller you want to use in the inspector
    public RuntimeAnimatorController controller;

    // the UI/Image component
    Image imageCanvas;
    // the fake SpriteRenderer
    public SpriteRenderer fakeRenderer;



    void Start()
    {
        imageCanvas = GetComponent<Image>();
    }

    public void setRuntimAnimatorController(RuntimeAnimatorController ctrl)
    {
        //animator.runtimeAnimatorController = ctrl;
    }

    void Update()
    {

        imageCanvas.sprite = fakeRenderer.sprite;

    }

}