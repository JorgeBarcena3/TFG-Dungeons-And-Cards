using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Assets.Script.Tools.Interfaces;
/// <summary>
/// Rellena la informacion de la carta
/// </summary>
public class HUDCard : IInfoUIElement<InfoCard>
{
    /// <summary>
    /// Almacena la informacion de la carta
    /// </summary>
    private InfoCard info;
    /// <summary>
    /// Label del nombre
    /// </summary>
    public Text namelbl;

    /// <summary>
    /// Label de la descripcion
    /// </summary>
    public Text descriptionlbl;

    /// <summary>
    /// Label del coste
    /// </summary>
    public Text costlbl;

    /// <summary>
    /// Imagen del prefab 
    /// </summary>
    public Image imagelbl;

    /// <summary>
    /// Se usa en la creacción de mazo
    /// </summary>
    private DeckCollectionUI deck_collection;
    /// <summary>
    /// true si pulsas encima de la carta
    /// </summary>
    private bool selected;
    /// <summary>
    /// Evita que por error añadas la carta mas de una vez en una sola acción
    /// </summary>
    private bool do_once;

    
    /// <summary>
    /// en la creación de mazo recibe la referencia a la collección
    /// </summary>
    /// <param name="collection"></param>
    public void set_collection(DeckCollectionUI collection) 
    {
        deck_collection = collection;
        selected = false;
        do_once = false;
        SwipeDetector.OnSwipe += add_card_in_deck;
    }

    /// <summary>
    /// Seleccionamos un arte de una carta
    /// </summary>
    public void SetCardArt(Sprite spr)
    {
        Material myMaterial = Instantiate(imagelbl.material);
        myMaterial.SetTexture("_text", spr.texture);
        imagelbl.material = myMaterial;
    }
    /// <summary>
    /// Rellena la informacion de la carta
    /// </summary>
    /// <param name="info"></param>
    public override void fillInfo(InfoCard info)
    {
        if (gameObject.GetComponent<CardAction>())
            gameObject.GetComponent<CardAction>().setRadio();

        costlbl.text = info.Cost.ToString();
        descriptionlbl.text = info.Description.ToString();
        namelbl.text = info.Name.ToString();
        SetCardArt(Resources.Load<Sprite>(info.Art.ToString()) != null ? Resources.Load<Sprite>(info.Art.ToString()) : Resources.Load<Sprite>("TEMPLATE"));
        this.info = info;
    }
    /// <summary>
    /// En la creación de mazo se añade la carta a un mazo
    /// </summary>
    private void add_card_in_deck(SwipeData data)
    {
        if (GameManager.Instance.state == States.INMENU)
        {
            if (selected)
            {
                if (data.Direction == SwipeDirection.Right)
                {
                    if (do_once)
                    {
                        deck_collection.add_card(info);
                        do_once = false;                    
                    }
                    
                }
                else
                {
                    selected = false;
                }
            }

        }
    }

    void OnMouseDown()
    {
        selected = true;
        do_once = true;
    }
    private void OnDestroy()
    {
        SwipeDetector.OnSwipe -= add_card_in_deck;
    }
}
