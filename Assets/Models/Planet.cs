using UnityEngine;
using System;
using System.Collections;

public class Planet : ILocatable
{

    public string name;
    public Nation owner;

    public enum PlanetType { Earth, Ice, Rock, Gas }
    public PlanetType type { get; set; }
    public float x, y;
    public int id { get; protected set; }
    public SolarSystem solarSystem;
   
    float _theta;
    public float theta
    {
        get { return _theta; }
        set
        {
            _theta = value;
            while (_theta > Mathf.PI * 2)
            {
                _theta -= Mathf.PI * 2;
            }
            UpdatePosUsingTheta();
        }
    }
    public float distFromSun;

    Action<Planet> onPlanetChanged;

    public Planet(float x, float y, int id, SolarSystem solarSystem, float theta,float distFromSun, PlanetType planetType = PlanetType.Earth) {
        this.x = x;
        this.solarSystem = solarSystem;
        this.y = y;
        this.theta = theta;
        this.distFromSun = distFromSun;
        this.type = planetType;
        this.id = id;

    }

    public void RegisterOnPlanetChanged(Action<Planet> callback) {
        onPlanetChanged += callback;
    }
    public void UnregisterOnPlanetChanged(Action<Planet> callback) {
        onPlanetChanged -= callback;
    }


    void UpdatePosUsingTheta() {
        x = distFromSun * Mathf.Cos(theta) + solarSystem.sun.x;
        y = distFromSun * Mathf.Sin(theta) + solarSystem.sun.y;
        if (onPlanetChanged != null)
        {
            onPlanetChanged(this);
        }
    }

    public Vector2 CurrentLocation()
    {
        return new Vector2(x, y);
    }
}
