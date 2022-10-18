using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Object
{
    public string name;
    public int damage;
    public int shell;
    public int maxShell;

    public void ShootWeapon()
    {

    }

    public int Restock()
    {
        return 0;
    }
}
