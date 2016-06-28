using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform planetPanel;
    public Dictionary<Planet, GameObject> PlanetButtons = new Dictionary<Planet, GameObject>();
    public GameObject planetButtonPrefab;

    Nation myNation;
    void Start ()
	{
	    myNation = new Nation("_name_", "description here", 0, 10000);
        myNation.RegisterOnPlanetColonized((Planet p) => { RefreshPlanetUI(); });
        myNation.ColonizePlanet(Galaxy.Instance.GetRandomPlanet());
        myNation.ColonizePlanet(Galaxy.Instance.GetRandomPlanet());

    }

    // Update is called once per frame
    void Update () {
	
	}

    void RefreshPlanetUI()
    {
        //add a button for each new planet
        foreach (Planet p in myNation.planets )
        {
            if (!PlanetButtons.Keys.Contains(p))
            {
                var closureFixPlanet = p;
                GameObject btn = (GameObject)Instantiate(planetButtonPrefab);
                PlanetButtons.Add(p,btn);
                btn.transform.SetParent(planetPanel);
                var btnEvent =  btn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                btnEvent.AddListener( () => { CameraController.Instance.SnapToObject(closureFixPlanet); } );
            }
        }
    }
}
