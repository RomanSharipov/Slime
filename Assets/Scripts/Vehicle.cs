using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleMovement))]
public class Vehicle : Item
{
    private VehicleMovement _vehicleMovement;

    private void Awake()
    {
        _vehicleMovement = GetComponent<VehicleMovement>();
    }

    public override void BeEaten(Slime slime)
    {
        _vehicleMovement.enabled = false;
        base.BeEaten(slime);
    }
}
