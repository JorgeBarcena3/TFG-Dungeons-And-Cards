using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Tools
{
    public class PanelListCardsInDeck : PanelList<InfoCard> 
    {
        private DeckCollectionUI collection;
        public void set_collection(DeckCollectionUI collection)
        {
            this.collection = collection;
        }
        /// <summary>
        /// Sincroniza la lista visual con la list
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
                    gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().sizeDelta.y + prefab.GetComponent<RectTransform>().sizeDelta.y + gameObject.GetComponent<VerticalLayoutGroup>().spacing);

                }
                else
                {
                    item = gameObject.transform.GetChild(i).gameObject;
                }
                item.GetComponentInChildren<CardInDeckUI>().fillInfo(list[i]);
                item.GetComponentInChildren<CardInDeckUI>().set_collection(collection);

            }
            if (gameObject.transform.childCount > (int)list.Count)
            {
                for (int i = list.Count; i < gameObject.transform.childCount; i++)
                {
                    Destroy(gameObject.transform.GetChild(i).gameObject);
                    gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, gameObject.GetComponent<RectTransform>().sizeDelta.y - prefab.GetComponent<RectTransform>().sizeDelta.y - gameObject.GetComponent<VerticalLayoutGroup>().spacing);
                    if (gameObject.GetComponent<RectTransform>().sizeDelta.y < 0)
                    {
                        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
                    }
                }
            }

            rect.verticalNormalizedPosition = 1.0f;

        }
    }
}
