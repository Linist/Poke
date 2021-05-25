using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScanningPokemon : MonoBehaviour
{
    public string pokemon1, pokemon2, pokemon3;

    string pokeName;

    public Pokemon pokemon;

    public List<Pokemon> pokeDeck = new List<Pokemon>(3);

    int i = 0;


    public void FoundPokemon(GameObject pokemon)
    {
        pokeName = pokemon.transform.GetChild(0).name;

        if (pokemon1 == "")
        {
            pokemon1 = pokeName;
            i = 0;
            CreatePokeDeck();
        }
        else if (pokemon2 == "" && pokeName != pokemon1)
        {
            pokemon2 = pokeName;
            i = 1;
            CreatePokeDeck();
        }
        else if (pokemon3 == "" && pokeName != pokemon2)
        {
            pokemon3 = pokeName;
            i = 2;
            CreatePokeDeck();
        }

        Debug.Log("Pokemon1 is:" + pokemon1 + ", Pokemon2 is:" + pokemon2 + ", Pokemon3 is:" + pokemon3);
    }

    public void CreatePokeDeck()
    {

        pokemon = PokemonStatsImporter.Create(10, pokeName);
        pokeDeck.Insert(i, pokemon);


    }
}
