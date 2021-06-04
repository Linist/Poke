using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeSpawner : MonoBehaviour
{
    
    public Pokemon enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int range = Random.Range(0, 10);
            if (range == 0)
                enemy = PokemonStatsImporter.CreateRandom();

            //this.gameObject.GetComponent<BattleScript>().PreBattle();
        }
        else
        {
            return;
        }
        
    }
}
