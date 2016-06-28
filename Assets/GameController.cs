using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform planetPanel;
    public Dictionary<Planet, Button> PlanetButtons = new Dictionary<Planet, Button>();
    public GameObject planetButtonPrefab;

    Nation myNation;
    void Start ()
	{
	    myNation = new Nation("_name_", "description here", 0, 10000);
        myNation.RegisterOnPlanetColonized((Planet p) => { RefreshPlanetUI(); });
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
                var closureFixPlanet = p; //TODO: check if this actually fixes closure problem.
                GameObject btn = (GameObject)Instantiate(planetButtonPrefab);
                btn.transform.SetParent(planetPanel);
                Button.ButtonClickedEvent btnEvent =  btn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
                btnEvent.AddListener( () => { CameraController.Instance.SnapToObject(closureFixPlanet); } );
            }
        }
    }
}
