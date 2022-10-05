using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable<T>
{
    public void OnInteraction(T actuator);
}

public interface IInteractible
{

}