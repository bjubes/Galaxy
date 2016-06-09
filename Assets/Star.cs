using UnityEngine;
using System;
using System.Collections;

public class Star {

    public float x, y;
    public int id { get; protected set; }
    public SolarSystem solarSystem;


    public Star(float x, float y, int id) {
        this.x = x;
        this.y = y;
        this.id = id;
    }
}
