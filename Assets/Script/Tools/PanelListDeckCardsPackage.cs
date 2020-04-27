using UnityEngine;
using UnityEngine.UI;


namespace Assets.Script.Tools
{
    public class PanelListDeckCardsPackage : PanelList<DeckCardsPackage>
    {
        private DeckCollectionUI collection;
        private int selected_deck=0;
        public void select_deck(int i)
        {
            selected_deck = i;
            sincList();
        }

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
                item.GetComponentInChildren<DeckCardsPackageUI>().fillInfo(list[i]);
                item.GetComponentInChildren<DeckCardsPackageUI>().set_collection(collection);
                if (i == selected_deck)
                    item.GetComponentInChildren<DeckCardsPackageUI>().select_deck_to_play();
                else
                {
                    item.GetComponentInChildren<DeckCardsPackageUI>().unselect_deck_to_play();
                }

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

