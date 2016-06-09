using UnityEngine;
using System.Collections;

public class GalaxyController : MonoBehaviour
{

    public Galaxy galaxy;
    public string seed;

    void Awake()
    {
        if (seed == "")
        {
            seed = null;
        }
        galaxy = new Galaxy(1, 50, 50, seed);
    }

    void Update()
    {

    }
}
