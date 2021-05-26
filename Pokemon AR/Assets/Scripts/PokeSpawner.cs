using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeSpawner : MonoBehaviour
{
    
    public Pokemon enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        int range = Random.Range(0, 10);
        if (range == 0)
            enemy = PokemonStatsImporter.CreateRandom();
    }
}
