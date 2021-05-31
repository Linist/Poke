using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public Pokemon player, enemy;
    public List<Pokemon> selectablePokemon;
    public GameObject movesBtns;
    public GameObject playerPokemon, enemyPokemon, Ditto;
    public Transform playerSpawnpoint, enemySpawnpoint;
    public int enemyLight, playerLight;
    public SerialController serialController;
    public bool playerTurn;

    public void PreBattle()
    {
        serialController = GameObject.Find("PokeSerialController").GetComponent<SerialController>();
        serialController.SendSerialMessage("6");

        enemy = this.gameObject.GetComponent<PokeSpawner>().enemy;

        enemySpawnpoint.position = GameObject.Find("ARCamera").GetComponent<PlaneGeneration>().GetEnemySpawnpoint();

        if (enemy.name != "Bulbasaur" || enemy.name != "Charmander" || enemy.name != "Squirtle" || enemy.name != "Pikachu")
        {
            Instantiate(Ditto, enemySpawnpoint);
        }
        else
        {
            enemyPokemon = GameObject.Find(enemy.name);
            Instantiate(enemyPokemon, enemySpawnpoint);
        }
        
    }

    public void ScanPlayer(GameObject selectedPokemon)
    {
        string pokeName = selectedPokemon.transform.GetChild(0).name;
        selectablePokemon = GameObject.Find("PokeDex").GetComponent<ScanningPokemon>().pokeDeck;

        for(int i = 0; i < selectablePokemon.Count; i++)
        {
            if (selectablePokemon[i].name == pokeName)
            {
                player = selectablePokemon[i];
            }
        }
        player.level = enemy.level;

        
        movesBtns.transform.GetChild(0).GetComponentInChildren<Text>().text = player.moves[0].name;
        if (player.moves[1].name != "" || player.moves[1].name != null)
        {
            movesBtns.transform.GetChild(1).GetComponentInChildren<Text>().text = player.moves[1].name;
        }
        if (player.moves[2].name != "" || player.moves[2].name != null)
        {
            movesBtns.transform.GetChild(2).GetComponentInChildren<Text>().text = player.moves[2].name;
        }

        StartBattle();
    }

    // Start is called before the first frame update
    public void StartBattle()
    {
        playerPokemon = GameObject.Find(player.name);

        playerSpawnpoint.position = GameObject.Find("ARCamera").GetComponent<PlaneGeneration>().GetPlayerSpawnpoint();
        // set rotation.

        Instantiate(playerPokemon, playerSpawnpoint);

        playerTurn = true;
        StartCoroutine(PlayerTurn());
    }

    public IEnumerator PlayerTurn()
    {
        playerTurn = true;
        while (playerTurn == true)
        {
            // enable move buttons
            movesBtns.SetActive(true);

            //click on move will call function to turn bool to false
            //Also attacks and do damage here instead of below
            yield return new WaitForSeconds(.1f);
        }
        

        int damageOnEnemy = player.Attack(enemy);
        enemyLight = Mathf.CeilToInt(damageOnEnemy * (30/enemy.maxHp));

        SendEnemyLight(enemyLight);

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
        movesBtns.SetActive(false);

        yield return new WaitForSeconds(5);

        int damageOnplayer = enemy.Attack(player);
        playerLight = Mathf.CeilToInt(damageOnplayer * (30/player.maxHp));

        SendPlayerLight(playerLight);
        
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
        yield return new WaitForSeconds(1);
        Destroy(playerPokemon);
        Destroy(enemyPokemon);

        serialController.SendSerialMessage("5");
    }

    void ChooseMove()
    {
        // Need to set move
        string move = ""; 

        playerTurn = false;
    }

    public void SendPlayerLight(int playerLight)
    {
        switch (playerLight)
        {
            case 1:
                serialController.SendSerialMessage("A");
                break;
            case 2:
                serialController.SendSerialMessage("B");
                break;
            case 3:
                serialController.SendSerialMessage("C");
                break;
            case 4:
                serialController.SendSerialMessage("D");
                break;
            case 5:
                serialController.SendSerialMessage("E");
                break;
            case 6:
                serialController.SendSerialMessage("F");
                break;
            case 7:
                serialController.SendSerialMessage("G");
                break;
            case 8:
                serialController.SendSerialMessage("H");
                break;
            case 9:
                serialController.SendSerialMessage("I");
                break;
            case 10:
                serialController.SendSerialMessage("J");
                break;
            case 11:
                serialController.SendSerialMessage("K");
                break;
            case 12:
                serialController.SendSerialMessage("L");
                break;
            case 13:
                serialController.SendSerialMessage("M");
                break;
            case 14:
                serialController.SendSerialMessage("N");
                break;
            case 15:
                serialController.SendSerialMessage("O");
                break;
            case 16:
                serialController.SendSerialMessage("P");
                break;
            case 17:
                serialController.SendSerialMessage("Q");
                break;
            case 18:
                serialController.SendSerialMessage("R");
                break;
            case 19:
                serialController.SendSerialMessage("S");
                break;
            case 20:
                serialController.SendSerialMessage("T");
                break;
            case 21:
                serialController.SendSerialMessage("U");
                break;
            case 22:
                serialController.SendSerialMessage("V");
                break;
            case 23:
                serialController.SendSerialMessage("W");
                break;
            case 24:
                serialController.SendSerialMessage("X");
                break;
            case 25:
                serialController.SendSerialMessage("Y");
                break;
            case 26:
                serialController.SendSerialMessage("Z");
                break;
            case 27:
                serialController.SendSerialMessage("1");
                break;
            case 28:
                serialController.SendSerialMessage("2");
                break;
            case 29:
                serialController.SendSerialMessage("3");
                break;
            case 30:
                serialController.SendSerialMessage("4");
                break;
            default:
                Debug.Log("playerLight is: 0");
                break;
        }
    }

    public void SendEnemyLight(int enemyLight)
    {
        switch (enemyLight)
        {
            case 1:
                serialController.SendSerialMessage("a");
                break;
            case 2:
                serialController.SendSerialMessage("b");
                break;
            case 3:
                serialController.SendSerialMessage("c");
                break;
            case 4:
                serialController.SendSerialMessage("d");
                break;
            case 5:
                serialController.SendSerialMessage("e");
                break;
            case 6:
                serialController.SendSerialMessage("f");
                break;
            case 7:
                serialController.SendSerialMessage("g");
                break;
            case 8:
                serialController.SendSerialMessage("h");
                break;
            case 9:
                serialController.SendSerialMessage("i");
                break;
            case 10:
                serialController.SendSerialMessage("j");
                break;
            case 11:
                serialController.SendSerialMessage("k");
                break;
            case 12:
                serialController.SendSerialMessage("l");
                break;
            case 13:
                serialController.SendSerialMessage("m");
                break;
            case 14:
                serialController.SendSerialMessage("n");
                break;
            case 15:
                serialController.SendSerialMessage("o");
                break;
            case 16:
                serialController.SendSerialMessage("p");
                break;
            case 17:
                serialController.SendSerialMessage("q");
                break;
            case 18:
                serialController.SendSerialMessage("r");
                break;
            case 19:
                serialController.SendSerialMessage("s");
                break;
            case 20:
                serialController.SendSerialMessage("t");
                break;
            case 21:
                serialController.SendSerialMessage("u");
                break;
            case 22:
                serialController.SendSerialMessage("v");
                break;
            case 23:
                serialController.SendSerialMessage("w");
                break;
            case 24:
                serialController.SendSerialMessage("x");
                break;
            case 25:
                serialController.SendSerialMessage("y");
                break;
            case 26:
                serialController.SendSerialMessage("z");
                break;
            case 27:
                serialController.SendSerialMessage("0");
                break;
            case 28:
                serialController.SendSerialMessage("9");
                break;
            case 29:
                serialController.SendSerialMessage("8");
                break;
            case 30:
                serialController.SendSerialMessage("7");
                break;
            default:
                Debug.Log("EnemyLight is: 0");
                break;
        }
    }
}
