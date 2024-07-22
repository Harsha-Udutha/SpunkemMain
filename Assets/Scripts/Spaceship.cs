using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Spaceship
{ 
    public  int Index { get; set; }
    public string Name { get; set; }
    public float Health { get; set; }
    public float Speed { get; set; }
    public float FireRange { get; set; }
    public float DamageRate { get; set; }
    public string MissileAmmo { get; set; }
    public int Price { get; set; }

    public  Spaceship(int index,string name, float health, float speed, float fireRange, float damageRate, string missileAmmo, int price)
    {
        this.Index = index;
        this.Name = name;
        this.Health = health;
        this.Speed = speed;
        this.FireRange = fireRange;
        this.DamageRate = damageRate;
        this.MissileAmmo = missileAmmo;
        this.Price = price;
    }
}
