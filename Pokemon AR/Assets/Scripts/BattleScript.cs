using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    public Pokemon player, enemy;
    public List<Pokemon> selectablePokemon;
    public GameObject movesBtns;
    public Button startBattleBtn;
    public Text battleText;
    public GameObject playerPokemon, enemyPokemon;
    public GameObject CharmanderPrefab, BulbasaurPrefab, SquitlePrefab, PikachuPrefab, DittoPrefab;
    public Transform playerSpawnpoint, enemySpawnpoint;
    public int damageOnEnemy, damageOnplayer;
    public int enemyLight, playerLight;
    public SerialController serialController;
    public bool playerTurn;
    /*
    public void EnemySpawnpoint(Transform enemyTransform)
    {
        enemySpawnpoint.position = enemyTransform.position;
        // Set rotation
    }*/
    public void PlayerSpawnpoint(Transform playerTransform)
    {
        playerSpawnpoint.position = playerTransform.position;
        //Set rotation
    }

    public void PreBattle(Transform enemyTransform)
    {
        
        enemySpawnpoint.position = enemyTransform.position;

        serialController = GameObject.Find("PokeSerialController").GetComponent<SerialController>();
        serialController.SendSerialMessage("6");

        battleText.gameObject.SetActive(true);

        //enemy = this.gameObject.GetComponent<PokeSpawner>().enemy;
        enemy = PokemonStatsImporter.CreateRandom();

        
        if (enemy.name != "Bulbasaur" || enemy.name != "Charmander" || enemy.name != "Squirtle" || enemy.name != "Pikachu")
        {
            Instantiate(DittoPrefab, enemySpawnpoint);
        }
        else
        {
            enemyPokemon = SpecifyPokemon(enemy.name);
            Instantiate(enemyPokemon, enemySpawnpoint);
        }
        battleText.text = "A wild "+enemy.name+" appear!";

    }

    public void ScanPlayer(GameObject selectedPokemon)
    {
        string pokeName = selectedPokemon.transform.GetChild(0).name;

        player = PokemonStatsImporter.Create(enemy.level, pokeName);
        /*
        selectablePokemon = GameObject.Find("PokeDex").GetComponent<ScanningPokemon>().pokeDeck;

        if (selectablePokemon.Count >= 3)
        {
            for (int i = 0; i < selectablePokemon.Count; i++)
            {
                if (selectablePokemon[i].name == pokeName)
                {
                    player = selectablePokemon[i];
                }
            }
        }
        else
        {
            player = null;
        }
        */

        playerPokemon = SpecifyPokemon(player.name);

        //player.level = enemy.level;

        Instantiate(playerPokemon, playerSpawnpoint);

        battleText.text = "Go " + player.name + "!";

        StartCoroutine(StartBattle());
    }

    // Start is called before the first frame update
    public IEnumerator StartBattle()
    {
        movesBtns.SetActive(true);

        movesBtns.transform.GetChild(0).GetComponentInChildren<Text>().text = player.moves[0].name;
        if (player.moves[1].name != "" || player.moves[1].name != null)
        {
            movesBtns.transform.GetChild(1).GetComponentInChildren<Text>().text = player.moves[1].name;
        }
        if (player.moves.Count() >= 3)
        {
            if (player.moves[2].name != "" || player.moves[2].name != null)
            {
                movesBtns.transform.GetChild(2).GetComponentInChildren<Text>().text = player.moves[2].name;
            }
            else
            {
                movesBtns.transform.GetChild(2).GetComponentInChildren<Text>().text = "Tackle";
            }
        }
        //serialController.SendSerialMessage("5");

        yield return new WaitForSeconds(2f);

        //StartCoroutine(PlayerTurn());
    }

    public IEnumerator PlayerTurn()
    {
        Debug.Log("PlayerTurn");

        /*
        playerTurn = true;
        while (playerTurn == true)
        {
            Debug.Log("In playerTurn while-loop");
            battleText.text = "Choose a move!";

            //click on move will call function to turn bool to false
            //Also attacks and do damage here instead of below
            yield return new WaitForSeconds(.1f);
        }*/
        battleText.text = "Choose a move!";
        yield return new WaitForSeconds(.1f);
        
        //Debug.Log("After while-loop in playerTurn");

        damageOnEnemy = player.Attack(enemy)* (30 / enemy.maxHp);
        //enemyLight = Mathf.RoundToInt(damageOnEnemy * (30/enemy.maxHp));
        enemyLight = (int)damageOnEnemy;

        if(enemyLight == 0)
        {
            enemyLight = Random.Range(1, 30);
        }

        SendEnemyLight(enemyLight);

        if (enemy.hp <= 0)
        {
            battleText.text = enemy.name + " faints. You win!";
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

        battleText.text = enemy.name + " uses " + enemy.moves[Random.Range(0,enemy.moves.Count-1)];

        damageOnplayer = enemy.Attack(player) * (30 / player.maxHp);
        //playerLight = Mathf.RoundToInt(damageOnplayer * (30/player.maxHp));
        playerLight = (int)damageOnplayer;

        if (playerLight == 0)
        {

            playerLight = Random.Range(1, 10);
        }
        SendPlayerLight(playerLight);
        
        if (player.hp <= 0)
        {
            battleText.text = player.name + " faints. You lost!";
            StartCoroutine(EndBattle());
        }
        else
        {
            movesBtns.SetActive(true);
            //StartCoroutine(PlayerTurn());
        }
    }

   public IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1);
        battleText.gameObject.SetActive(false);

        Destroy(playerPokemon);
        Destroy(enemyPokemon);

        //serialController.SendSerialMessage("5");

        startBattleBtn.gameObject.SetActive(true);
    }

    GameObject SpecifyPokemon(string pokemonName)
    {
        switch (pokemonName)
        {
            case "Charmander":
                return CharmanderPrefab;
                //break;
            case "Squirtle":
                return SquitlePrefab; 
                //break;
            case "Bulbasaur":
                return BulbasaurPrefab;
                //break;
            case "Pikachu":
                return PikachuPrefab;
                //break;
            default:
                return null;
                //break;
        }
        
    }

    public void ChooseMove(Text moveText)
    {
        // Need to set move
        string move = moveText.text;
        battleText.text = player.name + " uses " + move;
        //playerTurn = false;
        StartCoroutine(PlayerTurn());
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
