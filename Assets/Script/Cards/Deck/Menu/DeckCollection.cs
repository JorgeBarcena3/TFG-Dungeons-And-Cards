using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class DeckCollection
{
    /// <summary>
    /// Almacena todos los mazos creados
    /// </summary>
    public List<DeckCardsPackage> deckCollection = new List<DeckCardsPackage>();
    /// <summary>
    /// indica el mazo seleccionado para la partida
    /// </summary>
    public int deck_selected;
    private FirebaseDatabaseManager _FirebaseDatabase;
    /// <summary>
    /// Crea un mazo nuevo
    /// </summary>
    /// <param name="name">nomre del mazo</param>
    /// <returns>referencia del mazo</returns>
    public DeckCardsPackage new_deck(string name) 
    {
        DeckCardsPackage my_deck = new DeckCardsPackage(name);
        deckCollection.Add(my_deck);
        return my_deck;
    }
    /// <summary>
    /// guarda un mazo en la lista de mazos
    /// </summary>
    /// <param name="deck">mazo</param>
    /// <returns>mazo</returns>
    public DeckCardsPackage new_deck(DeckCardsPackage deck)
    {
        deckCollection.Add(deck);
        return deck;
    }
    /// <summary>
    /// Elimina un mazo
    /// </summary>
    /// <param name="my_deck">referencia del mazo</param>
    public void delete_deck(DeckCardsPackage my_deck) 
    {
        deckCollection.Remove(my_deck);
    }
    /// <summary>
    /// retorna el mazo seleccionado
    /// </summary>
    /// <returns></returns>
    public DeckCardsPackage get_deck_selected()
    {
        return deckCollection[deck_selected] as DeckCardsPackage;
    }

    public int select_deck(DeckCardsPackage deck)
    {
        for (int i = 0; i < deckCollection.Count; i++)
        {
            if (deck == deckCollection[i])
            {
                deck_selected = i;
                break;
            }
        }
        return deck_selected;
    }

    public void save_decks()
    {
        _FirebaseDatabase = FirebaseDatabaseManager.Instance;
        DecksSerialized decks = new DecksSerialized();
        decks.deckCollection = new List<List<int>>();
        decks.names = new List<string>();
        for (int i = 0; i < deckCollection.Count(); i++)
        {
            List<int> ids_deck = new List<int>();
            List<InfoCard> deck = deckCollection[i].get_cards();
            for (int x = 0; x < deck.Count(); x++)
            {
                ids_deck.Add(deck[i].Id);
                
            }
            decks.deckCollection.Add(ids_deck);
            decks.names.Add(deckCollection[i].get_name());
        }
        decks.deck_selected = deck_selected;
        decks.date = System.DateTime.Now.ToString();
        string json = JsonConvert.SerializeObject(decks);
        PlayerPrefs.SetString("decks_collection", json);
        _FirebaseDatabase.addOrUpdate<DecksSerialized>("decks_collection", "decks", decks);
    }
    /// <summary>
    /// Carga los mazos guardados
    /// </summary>
    /// <param name="parser"></param>
    public async void load_decks(CSVReader parser)
    {
        DecksSerialized decks;
        /// si se puede conectar a la base de datos
        try
        {
            _FirebaseDatabase = FirebaseDatabaseManager.Instance;
            decks = await _FirebaseDatabase.get<DecksSerialized>("decks_collection/decks");

            string json = PlayerPrefs.GetString("decks_collection");
            System.DateTime date = System.DateTime.Parse(decks.date);

            ///Si la info de la base de datos es mas antigua que la local
            if (System.DateTime.Parse(decks.date) < System.DateTime.Parse(JsonConvert.DeserializeObject<DecksSerialized>(json).date))
            {
                json = PlayerPrefs.GetString("decks_collection");
                decks = JsonConvert.DeserializeObject<DecksSerialized>(json);
                _FirebaseDatabase.addOrUpdate<DecksSerialized>("decks_collection", "decks", decks);
            }

        }
        ///Si la base de datos no esta disponible
        catch
        {
            string json = PlayerPrefs.GetString("decks_collection");
            decks = JsonConvert.DeserializeObject<DecksSerialized>(json);

        }
        List<InfoCard> all_cards = parser.getCardsInfo();
        for (int i = 0; i < decks.deckCollection.Count(); i++)
        {
            List<int> ids = decks.deckCollection[i];
            List<InfoCard> deck = new List<InfoCard>();
            for (int x = 0; x < ids.Count(); x++)
            {
                deck.Add(all_cards.Where(z=>z.Id == ids[x]).First());
            }
            DeckCardsPackage cards_package = new DeckCardsPackage(decks.names[i], deck);
            deckCollection.Add(cards_package);
        }
        deck_selected = decks.deck_selected;
    }
   
}
