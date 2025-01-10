using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    // Start is called before the first frame update
    public string characterID; //name or other identifier for the character
    public int maximumHp; //the maximum amount of health point the character have
    public int currentHp; //current amount of health the character have at one point, if this reach 0 then the character dies
    public int attackPower; //attack power the character have when attacking
    public int defensePower; //defensive power the character have when blocking
    public int maximumPotion; //maximum ammount of potion a character can have
    public int currentPotion; //the amount of potion a character have at one point, the amount decrease when a potion is used
    private Animator animator;
    private bool isDefending;
    private bool isDead;
    public Slider hpSlider;
    public ParticleSystem defenseParticle, healParticle;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentHp = maximumHp;
        hpSlider.maxValue = maximumHp;
        hpSlider.value = currentHp;
        currentPotion = maximumPotion;
        isDefending = false;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if(isDefending){
            currentHp -= (damage-defensePower);
            Debug.Log(characterID + " takes " + (damage-defensePower) + " damage(s)");
            isDefending = false;
            defenseParticle.Stop();
        }
        else
        {
            currentHp -= damage;
            Debug.Log(characterID + " takes " + damage + " damage(s)");
        }
        animator.SetTrigger("Damaged");
        UpdateHpBar();
    }

    public void UpdateHpBar()
    {
        hpSlider.value = currentHp;
    }

    public bool UsePotion()
    {
        if (currentHp == maximumHp)
        {
            Debug.Log(characterID + " hp already full");
            return false;
        }
        else if (currentPotion<=0)
        {
            Debug.Log(characterID + " don't have any more potion");
            return false;
        }
        else if (currentPotion>0)
        {
            currentPotion -= 1;
            int hpBefore = currentHp;
            currentHp += (5 + (maximumHp/5));
            if(currentHp > maximumHp)
            {
                currentHp = maximumHp;
            }
            Debug.Log(characterID + " healed " + (currentHp-hpBefore) + " HP");
            UpdateHpBar();
            healParticle.Play();
            return true;
        }
        else {
            Debug.Log("error on UsePotion()");
            return false;
        }
    }

    public void TakeDefense()
    {
        isDefending = true;
        Debug.Log(characterID + " takes a defensive position");
        defenseParticle.Play();
    }

    public bool IsCharacterDead()
    {
        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            currentHp = 0;
            Debug.Log(characterID + " dies");
        }
        return isDead;
    }
}
