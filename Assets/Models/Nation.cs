using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nation
{
    public string name;
    public string desc;

    public int credits;
    public int population;

    public List<Planet> planets;
    public Action<Planet> onPlanetColonzied;

    public Nation(string name, string desc, int credits, int population, List<Planet> planets = null)
    {
        this.name = name;
        this.desc = desc;
        this.credits = credits;
        this.population = population;
        this.planets = planets ?? new List<Planet>();
    }

    public void ColonizePlanet(Planet p)
    {
        if (p == null)
        {
            Debug.LogError("Planet cannot be null");
        }
        if (planets == null)
        {
            Debug.Log("wFocusType");
        }

        planets.Add(p);
        if (onPlanetColonzied != null)
        {
            onPlanetColonzied(p);
        }
        Debug.Log("Nation " + name + " has colonized planet " + p.id);
    }

    public void RegisterOnPlanetColonized(Action<Planet> callback)
    {
        onPlanetColonzied += callback;
    }
    public void UnregisterOnPlanetColonized(Action<Planet> callback)
    {
        onPlanetColonzied -= callback;
    }
}
