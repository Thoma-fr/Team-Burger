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

public interface IBrowsable
{
    public Sprite GetSprite();
    public string GetName();
    public string GetContent();
    public string GetDescription();
}