using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla el timescale del juego
/// </summary>
public class SpeedController : MonoBehaviour
{

    /// <summary>
    /// Modificador del timescale
    /// </summary>
    public List<float> timeModificator;

    /// <summary>
    /// Current time scale
    /// </summary>
    public float currentSpeed = 1;

    /// <summary>
    /// Label de la speed
    /// </summary>
    public Text speedlbl;

    /// <summary>
    /// Realiza el start y organiza el array del time scale
    /// </summary>
    public void Start()
    {
        timeModificator = timeModificator.OrderBy(o => o).ToList();
        setTimeScaleInfoText();
    }

    /// <summary>
    /// Cuando se hace click en el boton
    /// </summary>
    public void onChangeTimeScale()
    {
        if (GameManager.Instance.state == States.INGAME)
        {
            int i = timeModificator.IndexOf(currentSpeed);

            currentSpeed = timeModificator[(i + 1) >= timeModificator.Count ? 0 : (i + 1)];
            setTimeScaleInfoText();

            Time.timeScale = currentSpeed;
        }
    }

    /// <summary>
    /// Time scale info
    /// </summary>
    private void setTimeScaleInfoText()
    {
        speedlbl.text = "x " + currentSpeed.ToString();
    }
}
