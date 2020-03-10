using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Script.Tools.Interfaces;

public class PanelList<T> : MonoBehaviour
{
    /// <summary>
    /// Lista de gameobjects;
    /// </summary>
    [HideInInspector]
    public List<T> list;
    /// <summary>
    /// Prefab con la que se forma la lista
    /// </summary>
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        list = new List<T>();
    }

    // Update is called once per frame
    /// <summary>
    /// añade un item a la lista
    /// </summary>
    /// <param name="item"></param>
    public void add_item(T item) 
    { 
        list.Add(item);
        //sincList();
    }
    /// <summary>
    /// Retorna la lista de gameobjects
    /// </summary>
    /// <returns></returns>
    public List<T> get_list() 
    {
        return list;
    }
    /// <summary>
    /// retorna un item de la lista
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public T get_item(int i) 
    {
        if (i > list.Count)
            return list[list.Count - 1];
        return list[i];
    }
    /// <summary>
    /// Elimina un item de la lista
    /// </summary>
    /// <param name="item"></param>
    public void delete_item(GameObject item) 
    {
        //list.Remove(item);
        sincList();
    }
    public void delete_last() 
    {
        list.Remove(list[list.Count-1]);
        sincList();
    }
    /// <summary>
    /// Resetea la lista
    /// </summary>
    public void Reset()
    {
        list.Clear();
        sincList();
    }
    /// <summary>
    /// Sincroniza la lista visual con la list
    /// </summary>
    public virtual void sincList()
    {
        
        {
            int i = 0;
            if(gameObject.transform.childCount > 0)
            {
                do
                {
                    Destroy(gameObject.transform.GetChild(i).gameObject);
                    i++;
                } while (gameObject.transform.childCount < i);

            }
           
        }
        for (int i = 0; i < list.Count; i++) 
        {
            GameObject item = Instantiate(prefab, Vector3.zero, default, gameObject.transform);
            item.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
            item.GetComponent<IInfoUIElement<T>>().fillInfo(list[i]);
        }

        
    }
}
