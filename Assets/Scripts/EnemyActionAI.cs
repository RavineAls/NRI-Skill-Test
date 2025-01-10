using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionAI : MonoBehaviour
{
    CharController charController;
    TurnManager turnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharController>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    public void EnemyActionSequence()
    {
        //check current turn
        if (turnManager.GetTurnInfo(TurnManager.Turn.Enemy) && !charController.characterStats.IsCharacterDead())
        {
            // check current HP and potion ammount
            if (charController.characterStats.currentHp <= 50 && charController.characterStats.currentPotion > 0)
            {
                charController.HealSequence();
            }
            else
            {
                // random number generator
                int action = Random.Range(0, 2); // 0 for attack, 1 for defense
                if (action == 0)
                {
                    charController.AttackSequence();
                }
                else
                {
                    charController.DefenseSquence();
                }
            }
        }
    }
}
