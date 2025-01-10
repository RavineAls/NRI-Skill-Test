using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject parentObj;
    private Vector2 originalPosition;
    public Vector2 attackPosition;
    public float moveSpeed = 5f;
    private Animator animator;
    private bool movingAhead = false;
    private bool movingBack = false;
    private TurnManager turnManager;
    public CharacterStats characterStats, opponentStats;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        originalPosition = parentObj.transform.position;
        turnManager = FindObjectOfType<TurnManager>(); //get turn manager from scene
    }

    // Update is called once per frame
    void Update()
    {
        //check if the character is allowed to move or not
        if(movingAhead)
        {
            MoveCharacterTo(attackPosition);
        } 
        else if(movingBack)
        {
            MoveCharacterTo(originalPosition);
        }
        
        if(characterStats.IsCharacterDead())
        {
            CharacterDeath();
        }
    }

    public void AttackSequence()
    {
        StartCoroutine(LetAttackFinish());
    }

    public IEnumerator LetAttackFinish()
    {
        movingBack = false;
        movingAhead = true;
        animator.SetBool("Moving", true); //set moving parameter to true
        yield return new WaitForSeconds(3);
        turnManager.CompleteAction();
    }

    public void DefenseSquence()
    {
        characterStats.TakeDefense();
        turnManager.CompleteAction();
    }

    public void HealSequence()
    {
        if(characterStats.UsePotion())
        {
            turnManager.CompleteAction();
        }
    }

    void MoveCharacterTo(Vector2 destination)
    {
        //move to character to position
        parentObj.transform.position = Vector2.MoveTowards(parentObj.transform.position, destination, moveSpeed * Time.deltaTime);

        if ((Vector2)parentObj.transform.position == destination)//when position reached
        {
            if (movingAhead)
            {
                // start attack
                movingAhead = false;
                animator.SetTrigger("Attacking"); // Trigger the Attacking animation
                animator.SetBool("Moving", false);
                opponentStats.TakeDamage(characterStats.attackPower);
            }
            else if (movingBack)
            {
                // end movement
                movingBack = false;
                animator.SetBool("Moving", false);
            }
        }
    }

    public void AttackComplete()
    {
        //called after attack
        movingBack = true; 
        animator.SetBool("Moving", true);
    }

    public void CharacterDeath()
    {
        animator.SetBool("Dead", true);
    }
}
