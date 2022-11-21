using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseController : MonoBehaviour
{
    protected Collider2D col { get; set; }
    protected Rigidbody2D rb { get; set; }


}

//enum RELATION
//{
//    ENEMY,
//    FRIENDLY,
//    NEUTRAL
//}