using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager de la barra de maná
/// </summary>
public class ManaManager : MonoBehaviour
{

    /// <summary>
    /// Label del maná
    /// </summary>
    [SerializeField]
    private Text mana_lbl = null;

    /// <summary>
    /// Setea el lbl del maná
    /// </summary>
    /// <param name="i"></param>
    private void setManaLbl(int i)
    {
        mana_lbl.text = i.ToString();
    }
    

    /// <summary>
    /// Actializa la barra de mana
    /// </summary>
    /// <param name="i">Cantidad de maná</param>
    /// <param name="percentage">Entre 0 y 1</param>
    public void refreshMana(int i, float percentage)
    {
        setManaLbl(i);
    }

}
