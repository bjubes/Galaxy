using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpriteController : MonoBehaviour
{

    public Sprite planetSprite;
    public Sprite sunSprite;

    Galaxy galaxy;
    Dictionary<Planet, GameObject> planetGameObjects = new Dictionary<Planet, GameObject>();
    Dictionary<Star, GameObject> sunGameObjects = new Dictionary<Star, GameObject>();

    void Start()
    {
        galaxy = Galaxy.Instance;
        if (galaxy.planets.Values.Count == 0)
            return;

        foreach (SolarSystem sys in Galaxy.Instance.solarSystems.Values)
        {
            GameObject sunGO = CreateStarGO(sys.sun);
            foreach (Planet p in sys.planets.Values)
            {
                CreatePlanetGO(p, sunGO);
            }
        }
    }

    void RedrawMovedPlanet(Planet p)
    {
        //orbit has progressed
        GameObject go = planetGameObjects[p];
        go.transform.position = new Vector3(p.x, p.y, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject CreateStarGO(Star s)
    {
        GameObject go = new GameObject("Star_" + s.id);
        go.transform.parent = this.transform;
        go.transform.position = new Vector3(s.x, s.y, 0);
        go.AddComponent<SpriteRenderer>().sprite = sunSprite;
        sunGameObjects.Add(s, go);
        return go;
    }

    GameObject CreatePlanetGO(Planet p, GameObject sunGO)
    {
        GameObject go = new GameObject("Planet_" + p.id);
        go.transform.parent = sunGO.transform;
        go.transform.position = new Vector3(p.x, p.y, 0);
        go.AddComponent<SpriteRenderer>().sprite = planetSprite;
        p.RegisterOnPlanetChanged(RedrawMovedPlanet);
        planetGameObjects.Add(p, go);
        return go;
    }
}
