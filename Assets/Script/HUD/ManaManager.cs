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
    /// Imagen para rellenar la barra de vida
    /// </summary>
    [SerializeField]
    private Image fillMana = null;

    /// <summary>
    /// Setea el lbl del maná
    /// </summary>
    /// <param name="i"></param>
    private void setManaLbl(int i)
    {
        mana_lbl.text = i.ToString();
    }

    /// <summary>
    /// Cambia la barra de porcentaje el porcentaje del mana
    /// </summary>
    /// <param name="percentage"> Valores entre 0 y 1 </param>
    private IEnumerator setPercentajeBar(float percentage, float time)
    {
        float t = 0;

        while (fillMana.fillAmount != percentage)
        {
            t += Time.deltaTime / time;
            fillMana.fillAmount = Mathf.Lerp(fillMana.fillAmount, percentage, t);
            yield return null;
        }

        fillMana.fillAmount = percentage;
    }

    /// <summary>
    /// Actializa la barra de mana
    /// </summary>
    /// <param name="i">Cantidad de maná</param>
    /// <param name="percentage">Entre 0 y 1</param>
    public void refreshMana(int i, float percentage)
    {
        setManaLbl(i);
        StartCoroutine( setPercentajeBar(percentage, 1f) );
    }

}
