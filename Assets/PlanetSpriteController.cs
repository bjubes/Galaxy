using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpriteController : MonoBehaviour {

    public Sprite planetSprite;
    public Sprite sunSprite;

    Galaxy galaxy;
    Dictionary<Planet, GameObject> planetGameObjects = new Dictionary<Planet, GameObject>();
	void Start () {
        galaxy = Galaxy.Instance;
        if (galaxy.planets.Values.Count == 0)
            return;
	   foreach( Planet p in galaxy.planets.Values )
        {
            GameObject go = new GameObject("Planet_" + p.id);
            go.transform.parent = this.transform;
            go.transform.position = new Vector3(p.x, p.y, 0);
            go.AddComponent<SpriteRenderer>().sprite = planetSprite;

            p.RegisterOnPlanetChanged(OnPlanetChanged);
            planetGameObjects.Add(p, go);

        }

        foreach (Star s in galaxy.stars.Values) {
            GameObject go = new GameObject("Star_" + s.id);
            go.transform.parent = this.transform;
            go.transform.position = new Vector3(s.x, s.y, 0);
            go.AddComponent<SpriteRenderer>().sprite = sunSprite;

            //planetGameObjects.Add(p, go);
        }
	}

   void OnPlanetChanged(Planet p) {

    }

	// Update is called once per frame
	void Update () {
	
	}
}
