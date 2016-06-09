using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarSystem
{

    int numPlanets;
    public Star sun;
    public int x, y;
    public Dictionary<int, Planet> planets { get; protected set; }
    float radius; //radius of solar system (from sun to last planet)
    public int id { get; protected set; }

    public SolarSystem(int x, int y, int numPlanets, float radius)
    {
        this.numPlanets = numPlanets;
        this.radius = radius;
        InitSolarSystem(x, y);
    }

    void InitSolarSystem(int x, int y)
    {
        float dist = radius / numPlanets; //common distance
        sun = Galaxy.Instance.CreateStar(x, y);

        planets = new Dictionary<int, Planet>();

        for (int i = 0; i < numPlanets; i++)
        {
            float distSq = Mathf.Pow( (dist * (i+1)), 2f);  //square of dist from sun to this planet

            //get a random x offset and find y offset so that the new planet will have correct distance from last/star
            float xoffset = Mathf.Sqrt((float)Galaxy.random.NextDouble() * distSq);
            float yoffset = Mathf.Sqrt(distSq - xoffset);

            //50/50 chance either x or y is negative to cover all angles not just first quadrant
            int neg = Galaxy.random.Next(1,5); //any number 1-4
            if (neg > 2) {
                yoffset = -yoffset;
            }
            if (neg ==2 || neg == 3) {
                xoffset = -xoffset;
            }

            Planet p = Galaxy.Instance.CreatePlanet(x + xoffset, y + yoffset);
            planets.Add(p.id, p);
        }
    }

    void CreateRandomPlanet()
    {
        System.Random rand = Galaxy.random;
        int pid = rand.Next();
        while (Galaxy.Instance.planets.ContainsKey(pid))
        {
            Debug.Log("duplicate planet id. Reassigning...");
            pid = rand.Next();
        }
        Planet p = new Planet(rand.Next(Galaxy.Instance.width), rand.Next(Galaxy.Instance.height), pid, (Planet.PlanetType)rand.Next(3));
        Galaxy.Instance.planets.Add(p.id, p);
    }
}
