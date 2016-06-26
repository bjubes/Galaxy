using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOrbits : MonoBehaviour
{

    public float thetaScale = 0.01f;
    float radius;
    private int Size;
    private float theta = 0f;

    List<LineRenderer> orbits = new List<LineRenderer>();

    void Start()
    {
        foreach (SolarSystem sys in Galaxy.Instance.solarSystems.Values) {
            Star sun = sys.sun;
            foreach (Planet p in sys.planets.Values) {
                //create new GO
                GameObject GO = new GameObject("Orbit_" + p.id);
                GO.transform.SetParent(this.transform);
                //add Line Renderer
                LineRenderer lr= GO.AddComponent<LineRenderer>();
                lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lr.receiveShadows = false;
                //Draw orbit
                DrawOrbit(lr, sun, p);
                orbits.Add(lr);
            }
        }   
    }

    void DrawOrbit(LineRenderer render,Star sun, Planet planet)
    {
        radius = planet.distFromSun;
        theta = 0f;
        Size = (int)((1f / thetaScale) + 1f);
        render.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            theta += (2.0f * Mathf.PI * thetaScale);
            float x = radius * Mathf.Cos(theta) + sun.x;
            float y = radius * Mathf.Sin(theta) + sun.y;


            render.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    void Update()
    {
        foreach (LineRenderer lr in orbits) {
            float width = Mathf.Min( Camera.main.orthographicSize / Settings.OrbitLineSize, Settings.maxOrbitSize);
            lr.SetWidth(width, width);
        }

    }

}
