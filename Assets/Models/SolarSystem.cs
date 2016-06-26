using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarSystem
{

    int numPlanets;
    public Star sun;
    public int x, y;
    public Dictionary<int, Planet> planets { get; protected set; }
    public float radius; //radius of solar system (from sun to last planet)
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
        id = sun.id;

        planets = new Dictionary<int, Planet>();

        for (int i = 0; i < numPlanets; i++)
        {
            float r = dist * (i + 1);

            float theta = 2* Mathf.PI * (float)Galaxy.random.NextDouble();
            if (Settings.Debug.ForcePlanetTheta) { theta = 0; }
            float xoffset = r * Mathf.Cos(theta);
            float yoffset = r * Mathf.Sin(theta);

           
            Planet p = Galaxy.Instance.CreatePlanet(x + xoffset, y + yoffset, this,  theta, r);
            planets.Add(p.id, p);
            //Debug.Log(Vector2.Distance(new Vector2(p.x, p.y), new Vector2(sun.x, sun.y)));
        }
    }
}
