using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    public Pokemon player;
    public Pokemon enemy;
    public int enemyLight;
    public int playerLight;
    public GameObject spawnObj;


    // Start is called before the first frame update
    void Start()
    {
        enemy = spawnObj.GetComponent<PokeSpawner>().enemy;
    }

    public IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(3);

        player.Attack(enemy);
        enemyLight = Mathf.CeilToInt((enemy.maxHp - enemy.hp) * (30/100));

        if (enemy.hp <= 0)
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
           

    }


    public IEnumerator EnemyTurn()
    {

        yield return new WaitForSeconds(3);

        enemy.Attack(player);
        playerLight = Mathf.CeilToInt((player.maxHp - player.hp) * (30 / 100));

        if (player.hp <= 0)
        {
            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(PlayerTurn());
        }
    }

   public IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(3);
    }

}
