using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Rellena la informacion de la carta
/// </summary>
public class HUDCard : MonoBehaviour
{
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
    /// Rellena la informacion de la carta
    /// </summary>
    /// <param name="info"></param>
    public void fillInfo(InfoCard info)
    {
        this.gameObject.GetComponent<CardAction>().setRadio();

        costlbl.text = info.Cost.ToString();
        descriptionlbl.text = info.Description.ToString();
        namelbl.text = info.Name.ToString();

    }
}
