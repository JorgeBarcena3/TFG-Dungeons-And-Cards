using UnityEngine;
using Assets.Script.Tools.Interfaces; 
namespace Assets.Script.Tools
{
    public class PanelListInfoCard : PanelList<InfoCard>
    {
        private DeckCollectionUI collection;
        public void set_collection(DeckCollectionUI collection)
        {
            gameObject.GetComponent<HUDCard>().set_collection(collection);
        }

        public override void sincList()
        {

            {
                int i = 0;
                if (gameObject.transform.childCount > 0)
                {
                    do
                    {
                        Destroy(gameObject.transform.GetChild(i).gameObject);
                        i++;
                    } while (gameObject.transform.childCount < i);

                }

            }
            for (int i = 0; i < list.Count; i++)
            {
                GameObject item = Instantiate(prefab, Vector3.zero, default, gameObject.transform);
                item.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
                item.GetComponent<IInfoUIElement<InfoCard>>().fillInfo(list[i]);
                item.GetComponent<HUDCard>().set_collection(collection);
            }


        }
    }
}
