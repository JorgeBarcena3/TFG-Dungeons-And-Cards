using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Pinta la vida de los enemigos en el HUD de la pantalla
/// </summary>
public class EnemyHudManager : MonoBehaviour
{

    /// <summary>
    /// El enemigo del que se va a mostrar la informacion
    /// </summary>
    EnemyInfo enemyInfo;

    /// <summary>
    /// Prefab donde se va a mostrar la informacion
    /// </summary>
    public GameObject infoPrefab;

    /// <summary>
    /// Canvas donde mostrar la imagen
    /// </summary>
    public Canvas canvas;

    /// <summary>
    /// Prefab instanciado
    /// </summary>
    private GameObject cardInstance;

    /// <summary>
    /// Si estamos activos, es decir, mostrando la informacion por la pantalla
    /// </summary>
    [HideInInspector]
    public bool active;

    /// <summary>
    /// Position of relax
    /// </summary>
    private Vector2 rel_pos;


    /// <summary>
    /// iniciliza el componente
    /// </summary>
    public void init()
    {
        cardInstance = Instantiate(infoPrefab, canvas.transform);
        cardInstance.transform.localScale = Card.ORIGINAL_SIZE * 2;
        rel_pos = new Vector2(0, ((RectTransform)(canvas.transform)).sizeDelta.y + 200);
        cardInstance.transform.localPosition = rel_pos;
        cardInstance.SetActive(true);
        SwipeDetector.OnSwipe += onCardSwipe;
        active = false;

    }

    /// <summary>
    /// Detectamos si hemos hecho click en una carta
    /// </summary>
    private void onCardSwipe(SwipeData data)
    {
        if (GameManager.Instance.state == States.INGAME &&
           GameManager.Instance.turn == TURN.PLAYER)
        {
            if ((data.Direction == SwipeDirection.Down || data.Direction == SwipeDirection.Up) && active)
            {
                hideInfo();
            }
        }

    }

    /// <summary>
    /// Muestra la informacion por pantalla
    /// </summary>
    public void showInfo(Enemy enemy)
    {
        active = true;
        cardInstance.SetActive(active);

        var front = cardInstance.transform.GetChild(0);
        front.transform.Find("Namelbl").GetComponent<Text>().text = enemy.info.name;
        front.transform.Find("Descriptionlbl").GetComponent<Text>().text = enemy.info.description;
        cardInstance.transform.Find("Vida").Find("lifelbl").GetComponent<Text>().text = "1";


        GameManager.Instance.deck.infoBackground.transform.SetSiblingIndex(100);
        cardInstance.transform.SetSiblingIndex(101);

        StartCoroutine(AuxiliarFuncions.FadeIn(GameManager.Instance.deck.infoBackground.GetComponent<Image>(), GameManager.Instance.deck.infoBackground, 65, 1, true));
        StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)cardInstance.transform, Vector3.zero));

    }

    /// <summary>
    /// Esconde la informacion del enemigo
    /// </summary>
    public void hideInfo()
    {
        var front = cardInstance.transform.GetChild(0);
        active = false;

        StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)cardInstance.transform, Vector3.zero));

        StartCoroutine(AuxiliarFuncions.FadeOut(GameManager.Instance.deck.infoBackground.GetComponent<Image>(), GameManager.Instance.deck.infoBackground, 1, true));
        StartCoroutine(AuxiliarFuncions.MoveObjectToLocal((RectTransform)cardInstance.transform, rel_pos));
    }
}
