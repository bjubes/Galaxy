using UnityEngine;
using System;
using System.Collections;

public class Planet {

    public enum PlanetType { Earth, Ice, Rock, Gas }
    public PlanetType type { get; set; }
    public float x, y;
    public int id { get; protected set; }
    public SolarSystem solarSystem;

    Action<Planet> onPlanetChanged;

    public Planet(float x, float y, int id, PlanetType planetType = PlanetType.Earth) {
        this.x = x;
        this.y = y;
        this.type = planetType;
        this.id = id;
    }

    public void RegisterOnPlanetChanged(Action<Planet> callback) {
        onPlanetChanged += callback;
    }
    public void UnregisterOnPlanetChanged(Action<Planet> callback) {
        onPlanetChanged -= callback;
    }
}
