using UnityEngine;
using System.Collections;

public class DrawOrbits : MonoBehaviour
{

    //debug
    public GameObject sunGO, planetGO;

    public Star sun;
    public Planet planet;

    public float ThetaScale = 0.01f;
    float radius;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;
    float sunX, sunY;

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
            }
        }   
    }

    void DrawOrbit(LineRenderer render,Star sun, Planet planet)
    {
        radius = Vector2.Distance(new Vector2(planet.x, planet.y), new Vector2(sun.x, sun.y));
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        render.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta) + sun.x;
            float y = radius * Mathf.Sin(Theta) + sun.y;
            render.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
