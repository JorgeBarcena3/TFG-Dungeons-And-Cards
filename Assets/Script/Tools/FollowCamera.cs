using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Posiciona un objeto en relacion a la main camera
/// </summary>
public class FollowCamera : MonoBehaviour
{
    private Transform tr;
    public bool coord2D;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coord2D)
            tr.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, distance);
        else 
        {
            tr.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + distance);
        }
    }
}
