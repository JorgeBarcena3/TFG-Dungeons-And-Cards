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

    
 
    public void set_collection(DeckCollectionUI collection) 
    {
        deck_collection = collection;
    }

    public void on_button() 
    {
        deck_collection.add_card(info);
    }

    /// <summary>
    /// Seleccionamos un arte de una carta
    /// </summary>
    public void SetCardArt(Sprite spr)
    {
        Material myMaterial = Instantiate(imagelbl.material);
        myMaterial.SetTexture("_MyTexture", spr.texture);
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
}
