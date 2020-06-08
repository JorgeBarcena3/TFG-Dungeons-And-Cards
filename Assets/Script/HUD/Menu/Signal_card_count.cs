using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Lleva un conteo de las cantidad da cartas que hay en cada baraja
/// </summary>
public class Signal_card_count : MonoBehaviour
{
    DeckCollection collection;
    private Image image;
    private Text text;
    public Image warming;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        warming.enabled = false;
        count = 0;
        
    }
    public void SetDeckCollection(DeckCollection collection) 
    {
        this.collection = collection;
    }
    public void updateSignal(int count) 
    {
        this.count = count;
        text.text = count.ToString();
        if (count < collection.count_cards)
        {
            image.color = Color.red;
        }
        else 
        {
            image.color = Color.green;
        }


    }
    private void OnMouseDown()
    {
        warming.enabled = true;
        int missing = collection.count_cards - count;
        warming.GetComponentInChildren<Text>().text = "Te faltan " + missing + " para llenar el mazo";
        Invoke("closeWarmingMessage", 1.5f);
    }
    private void closeWarmingMessage() 
    {
        GetComponentInChildren<Image>().enabled = false;
    }

}
