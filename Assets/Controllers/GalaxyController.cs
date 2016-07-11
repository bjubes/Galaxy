using UnityEngine;
using System.Collections;

public class GalaxyController : MonoBehaviour
{
    public Galaxy galaxy;
    public string seed;
    public int solarSystems, width, height;

    void Awake()
    {
        if (seed == "")
        {
            seed = null;
        }
        galaxy = new Galaxy(solarSystems,width,height,seed);

    }

    void Update()
    {
        foreach(Planet p in galaxy.planets.Values)
        {
            p.theta += (Mathf.PI * .1f * Time.deltaTime)/p.distFromSun;
        }

        //move ships
        foreach (Ship s in galaxy.ships.Values)
        {
            Vector2 target = new Vector2(galaxy.GetFirstPlanet().x, galaxy.GetFirstPlanet().y);
            s.DoMovement(Time.deltaTime, target);
        }
    }


}
