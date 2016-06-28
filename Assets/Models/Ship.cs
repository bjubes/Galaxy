using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.EventSystems;

public class Ship : ILocatable
{

    public float x, y;
    public int id { get; protected set; }
    public bool docked;
    public Planet dockedPlanet;
    public float maxSpeed;
    public float acceleration;

    Action<Ship> onShipChanged;

    private Vector2 _dir;
    public Vector2 Dir {
        get { return _dir; }
        set
        {
            _dir = value;
            _dir.Normalize();
        }
    }
    float currSpeed;

    public Ship(float x, float y, int id, bool docked, Planet dockedPlanet, float maxSpeed, float acceleration)
    {
        this.x = x;
        this.y = y;
        this.id = id;
        this.docked = docked;
        this.dockedPlanet = dockedPlanet;
        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        Dir = Vector2.up;
    }

    public void DoMovement(float deltaTime, Vector2 destination)
    {
        Dir = destination - new Vector2(x, y);
        //increase speed due to acceleration
        if (currSpeed < maxSpeed)
        {
            currSpeed += (acceleration * deltaTime);
        }
        currSpeed = Mathf.Min(currSpeed, 10f);
        //move using speed
        Vector2 moveVector = Dir*currSpeed * deltaTime;
        x += moveVector.x;
        y += moveVector.y;
        onShipChanged(this);

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log(currSpeed);
        }

    }


    public void RegisterOnPlanetChanged(Action<Ship> callback)
    {
        onShipChanged += callback;
    }

    public void UnregisterOnPlanetChanged(Action<Ship> callback)
    {
        onShipChanged -= callback;
    }

    public Vector2 CurrentLocation()
    {
        return new Vector2(x,y);
    }
}