using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum Turn {Player, Enemy}
    private Turn currentTurn;
    bool actionCompleted;
    public GameObject[] buttons;
    public GameObject enemyObj, playerObj, winPlaque, losePlaque, endButtons;
    EnemyActionAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = Turn.Player;
        actionCompleted = false;
        ToggleButtonInteractability();
        enemyAI = enemyObj.GetComponentInChildren<EnemyActionAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCompleted)
        {
            SwitchTurn();
        }
        if (playerObj.GetComponent<CharacterStats>().IsCharacterDead()||enemyObj.GetComponent<CharacterStats>().IsCharacterDead())
        {
            GameEnd();
        }
    }

    public void ToggleButtonInteractability()
    {
        if(currentTurn == Turn.Player)
        {
            foreach (var obj in buttons)
            {
                obj.GetComponent<Button>().interactable = true;
            }
        }
        else if(currentTurn == Turn.Enemy)
        {
            foreach (var obj in buttons)
            {
                obj.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void ToggleButtonInteractabilityOff()
    {
        foreach (var obj in buttons)
        {
            obj.GetComponent<Button>().interactable = false;
        }
    }

    void BeginPlayerTurn()
    {
        currentTurn = Turn.Player;
        Debug.Log("Player's Turn");
    }

    public void BeginEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        Debug.Log("Enemy's Turn");
        enemyAI.EnemyActionSequence();
    }

    void SwitchTurn()
    {
        actionCompleted = false;
        if (currentTurn == Turn.Player)
        {
            BeginEnemyTurn();
        }
        else if (currentTurn == Turn.Enemy)
        {
            BeginPlayerTurn();
        }
        ToggleButtonInteractability();
    }

    public bool GetTurnInfo(Turn askedTurn)
    {
        if (askedTurn == currentTurn){
            return true;
        }
        else return false;
    }

    public void CompleteAction()
    {
        actionCompleted = true;
    }

    void GameEnd()
    {
        ToggleButtonInteractabilityOff();
        if(playerObj.GetComponent<CharacterStats>().IsCharacterDead())
        {
            losePlaque.SetActive(true);
        }
        else if(enemyObj.GetComponent<CharacterStats>().IsCharacterDead())
        {
            winPlaque.SetActive(true);
        }
        endButtons.SetActive(true);
    }
}
