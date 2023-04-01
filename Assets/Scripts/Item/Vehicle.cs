using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(VehicleMovement))]
public class Vehicle : Item
{
    private VehicleMovement _vehicleMovement;

    public void Init(PathCreator pathCreator,float distanceTraveled)
    {
        _vehicleMovement = GetComponent<VehicleMovement>();
        _vehicleMovement.Init(pathCreator,distanceTraveled);
    }

    public override void BeEaten(Slime slime)
    {
        _vehicleMovement.enabled = false;
        base.BeEaten(slime);
    }
}
