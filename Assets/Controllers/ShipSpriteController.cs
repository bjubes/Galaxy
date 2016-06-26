using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipSpriteController : MonoBehaviour
{

    public Sprite shipSprite;

    Galaxy galaxy;
    Dictionary<Ship, GameObject> shipGameObjects = new Dictionary<Ship, GameObject>();

    void Start()
    {
        galaxy = Galaxy.Instance;
        if (galaxy.planets.Values.Count == 0)
            return;

        foreach (Ship s in Galaxy.Instance.ships.Values)
        {
            GameObject shipGO = CreateShipGO(s);
        }
    }

    void RedrawMovedShip(Ship s)
    {
        GameObject go = shipGameObjects[s];
        go.transform.position = new Vector3(s.x, s.y, 0);
        //go.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, s.Dir)) ;

        float angle = Mathf.Atan2(s.Dir.y, s.Dir.x) * Mathf.Rad2Deg;
        angle -= 90; //DEBUG: due to sprite facing up and not at 2pi
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

    }

    GameObject CreateShipGO(Ship s)
    {
        GameObject go = new GameObject("Ship_" + s.id);
        go.transform.parent = this.transform;
        go.transform.position = new Vector3(s.x, s.y, 0);
        go.AddComponent<SpriteRenderer>().sprite = shipSprite;
        s.RegisterOnPlanetChanged(RedrawMovedShip);
        shipGameObjects.Add(s, go);
        return go;
    }
}
