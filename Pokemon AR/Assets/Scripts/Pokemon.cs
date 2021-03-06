using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Elements
{
    Fire,
    Water,
    Grass,
    Electric
}

[System.Serializable]
public class Pokemon
{

    public string name;
    public int level;
    public int baseAttack;
    public int baseDefence;
    public int hp;
    public int maxHp;
    public Elements element;
    public List<Move> moves;

    public Pokemon(string name, int level, int baseAttack,
        int baseDefence, int hp, Elements element, List<Move> moves)
    {
        this.level = level;
        this.baseAttack = baseAttack;
        this.baseDefence = baseDefence;
        this.name = name;
        this.hp = hp * level;
        this.maxHp = hp * level;
        this.element = element;
        this.moves = moves;
    }


    public int Attack(Pokemon enemy)
    {

        int attack = baseAttack * level;
        attack = CalculateElementalEffects(attack, enemy.element);
        int defence = enemy.CalculateDefence();
        int damage = attack - defence;

        if (damage < 0)
        {
            damage = 0;
           
        }
       
        else if (damage >= 100)
        {
            damage = 100;
            enemy.ApplyDamage(damage);
         
        }
      
        else if (damage > 0 && damage < 100)
        {
            enemy.ApplyDamage(damage);
           
        }

        return damage;
    }


    public int CalculateDefence()
    {
        return baseDefence * level;
    }


    public int CalculateElementalEffects(int damage, Elements enemyType)
    {
        if (element == Elements.Water && enemyType == Elements.Fire ||
            element == Elements.Grass && enemyType == Elements.Water ||
            element == Elements.Fire && enemyType == Elements.Grass ||
            element == Elements.Electric && enemyType == Elements.Water
            )
        {

            return damage * 2;
        }
        if (element == Elements.Fire && enemyType == Elements.Water ||
            element == Elements.Water && enemyType == Elements.Grass ||
            element == Elements.Grass && enemyType == Elements.Fire ||
            element == Elements.Electric && enemyType == Elements.Grass
            )
        {

            return damage / 2;
        }
        return damage;
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;
    }

    public void Restore()
    {
        hp = maxHp;
    }
}
