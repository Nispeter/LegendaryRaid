using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    public List<Hero> heroes = new List<Hero>();

    void Start()
    {
        SearchHeroes();
    }

    public void SearchHeroes()
    {
        Hero[] heroArray = FindObjectsOfType<Hero>();
        foreach (Hero hero in heroArray)
        {
            heroes.Add(hero);
        }
    }
}
