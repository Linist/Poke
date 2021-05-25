using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningPokemon : MonoBehaviour
{
    public string pokemon1, pokemon2, pokemon3;

    string pokeName;

    public void FoundPokemon(GameObject pokemon)
    {
        pokeName = pokemon.transform.GetChild(0).name;

        if (pokemon1 == "")
        {
            pokemon1 = pokeName;
        }
        else if (pokemon2 == "" && pokeName != pokemon1)
        {
            pokemon2 = pokeName;
        }
        else if (pokemon3 == "" && pokeName != pokemon2)
        {
            pokemon3 = pokeName;
        }

        Debug.Log("Pokemon1 is:" + pokemon1 + ", Pokemon2 is:" + pokemon2 + ", Pokemon3 is:" + pokemon3);
    }
}
