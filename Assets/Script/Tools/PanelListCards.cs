using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Guarda un panel de listas
/// </summary>
public class PanelListCards : PanelList<InfoCard>
{
    /// <summary>
    /// Coleccion de cartas a mostrar
    /// </summary>
    private DeckCollectionUI collection;

    /// <summary>
    /// Determina la coleccion
    /// </summary>
    /// <param name="collection"></param>
    public void set_collection(DeckCollectionUI collection)
    {
        this.collection = collection;
    }

    /// <summary>
    ///  Sincrioniza las listas
    /// </summary>
    public override void sincList()
    {

        for (int i = 0; i < list.Count; i++)
        {

            GameObject item;
            if (i >= gameObject.transform.childCount)
            {
                item = Instantiate(prefab, Vector3.zero, default, gameObject.transform);
                item.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
                gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().sizeDelta.y + prefab.GetComponent<RectTransform>().sizeDelta.y * 3 + gameObject.GetComponent<VerticalLayoutGroup>().spacing);

            }
            else
            {
                item = gameObject.transform.GetChild(i).gameObject;
            }
            item.GetComponentInChildren<HUDCard>().fillInfo(list[i]);
            item.GetComponentInChildren<HUDCard>().set_collection(collection);

        }
        if (gameObject.transform.childCount > (int)list.Count)
        {
            for (int i = list.Count; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
                gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().sizeDelta.y - prefab.GetComponent<RectTransform>().sizeDelta.y * 3 - gameObject.GetComponent<VerticalLayoutGroup>().spacing);
                if (gameObject.GetComponent<RectTransform>().sizeDelta.y < 0)
                {
                    gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
                }
            }
        }

        rect.verticalNormalizedPosition = 1.0f;
    }
}

