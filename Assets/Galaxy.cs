using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Galaxy
{
    public static Galaxy Instance { get; protected set; }
    public static System.Random random;

    public string Seed { get; protected set; }

    int numPlanets, numSolarSystem;
    public Dictionary<int, Planet> planets { get; protected set; }
    public Dictionary<int, Star> stars { get; protected set; }
    public Dictionary<int, SolarSystem> solarSystems { get; protected set; }
    public int width;
    public int height;

    public Galaxy(int numSolarSystem, int width, int height, string seed = null)
    {
        Instance = this;
        this.numSolarSystem = numSolarSystem;
        this.width = width;
        this.height = height;
        if (seed == null)
        {
            Seed = System.DateTime.Now.Ticks.ToString();
        }
        else
        {
            Seed = seed;
        }
        random = new System.Random(Seed.GetHashCode());

        planets = new Dictionary<int, Planet>();
        stars = new Dictionary<int, Star>();
        solarSystems = new Dictionary<int, SolarSystem>();
        for (int i = 0; i < numSolarSystem; i++)
        {
            SolarSystem s = new SolarSystem(random.Next(0, width), random.Next(0, height), random.Next(2, 9), random.Next(50, 200));
            solarSystems.Add(s.id, s);

        }
        Debug.Log("Created galaxy with " + numSolarSystem + " Systems\nSeed: " + Seed);
    }

    public Planet CreatePlanet(float x, float y, Planet.PlanetType type = Planet.PlanetType.Earth)
    {
        int id = CreateUniquePlanetID();
        Planet p = new Planet(x, y, id, type);
        planets.Add(id, p);
        return p;
    }


    public Star CreateStar(float x, float y)
    {
        int id = CreateUniquePlanetID();
        Star s = new Star(x, y, id);
        stars.Add(id, s);
        return s;
    }

    int CreateUniquePlanetID()
    {
        int pid = random.Next();
        while (planets.ContainsKey(pid))
        {
            Debug.Log("duplicate planet id. Reassigning...");
            pid = random.Next();
        }
        return pid;
    }

    //debug reasons
    public Star GetFirstStar()
    {
        return stars[stars.Keys.First()];
    }

    public Planet GetFirstPlanet()
    {
        return planets[planets.Keys.First()];
    }
}
