using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform planetPanel;

    void Start ()
	{
	    Nation n = new Nation("_name_", "description here", 0, 10000);
        n.RegisterOnPlanetColonized((Planet p) => { RefreshPlanetUI(); });
        n.ColonizePlanet(Galaxy.Instance.GetRandomPlanet());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void RefreshPlanetUI()
    {

    }
}
