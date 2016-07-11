using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Galaxy
{
    public static Galaxy Instance { get; protected set; }
    public static System.Random random;

    public string Seed { get; protected set; }

    public Dictionary<int, Planet> planets { get; protected set; }
    public Dictionary<int, Star> stars { get; protected set; }
    public Dictionary<int, SolarSystem> solarSystems { get; protected set; }
    public Dictionary<int, Ship> ships { get; protected set; }

	public List<Nation> nations;

    public int width;
    public int height;

    float solarSystemBuffer = 10.0f;

    public Galaxy(int numSolarSystem, int width, int height, string seed = null)
    {
        Instance = this;
        this.width = width;
        this.height = height;
        Seed = seed ?? System.DateTime.Now.Ticks.ToString();// if seed is null set seed = unix time
        random = new System.Random(Seed.GetHashCode());

        planets = new Dictionary<int, Planet>();
        stars = new Dictionary<int, Star>();
        solarSystems = new Dictionary<int, SolarSystem>();
        ships = new Dictionary<int, Ship>();
		nations = new List<Nation> ();


        for (int i = 0; i < numSolarSystem; i++)
        {
            int count = 0;
            int safteyCap = 1000;

            int x = random.Next(0, width);
            int y = random.Next(0, height);
            int r = random.Next(50, 200);

            while (!IsSpaceFree(x, y, r)) {
                 x = random.Next(0, width);
                 y = random.Next(0, height);
                 r = random.Next(50, 200);
                count++;

                if (count > safteyCap) {
                    Debug.LogError("Galaxy Generation Error \nCannot find any free space: Lower the amount of solar systems or increase galaxy size \nFailed on solar system "+ i +" of "+ numSolarSystem);
                    break;
                }
            }




            SolarSystem s = new SolarSystem(x, y, random.Next(2, 9), r);
            solarSystems.Add(s.id, s);

            //debug cmd
            if (Settings.Debug.CenterCameraOnFinalSystem)
            {
                Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);
                Camera.main.orthographicSize = 150f;
            }
        }
        Debug.Log("Created galaxy with " + numSolarSystem + " Systems\nSeed: " + Seed);
        
    }

    public Planet CreatePlanet(float x, float y, SolarSystem solarSystem, float theta, float distFromSun, Planet.PlanetType type = Planet.PlanetType.Earth)
    {
        int id = CreateUniquePlanetID();
        Planet p = new Planet(x, y, id, solarSystem ,theta, distFromSun, type);
        planets.Add(id, p);
        return p;
    }


    public Star CreateStar(float x, float y)
    {
        int id = CreateUniqueStarID();
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

    int CreateUniqueStarID()
    {
        int pid = random.Next();
        while (stars.ContainsKey(pid))
        {
            Debug.Log("duplicate planet id. Reassigning...");
            pid = random.Next();
        }
        return pid;
    }

    public Ship CreateShip(float x, float y, bool docked, Planet dockedPlanet, float speed, float acceleration)
    {
        int id = CreateUniqueShipID();
        Ship s = new Ship(x, y, id, docked, dockedPlanet, speed, acceleration);
        ships.Add(id, s);
        return s;
    }

    int CreateUniqueShipID()
    {
        int id = random.Next();
        while (ships.ContainsKey(id))
        {
            Debug.Log("duplicate ship id. Reassigning...");
            id = random.Next();
        }
        return id;
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

    /// <summary>
    /// Checks whether there is no solar system within the radius given at the coordinates given
    /// </summary>
    /// <returns></returns>
    public bool IsSpaceFree(int x, int y, int r) {
        foreach (SolarSystem sys in solarSystems.Values) {
            float dist = Vector2.Distance(new Vector2(x, y), new Vector2(sys.sun.x, sys.sun.y));
            float distRequired = r + sys.radius + solarSystemBuffer;
            if (dist < distRequired)
            {
                return false;
            }
        }
        return true;
    }

    public Planet GetRandomPlanet()
    {
        int total = planets.Values.Count;
        int pnum = random.Next(0, total);
        return planets.Values.ElementAt(pnum);
    }
}
