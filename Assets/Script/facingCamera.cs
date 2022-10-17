using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facingCamera : MonoBehaviour
{
    public Transform castPoints1, castPoints2;
    private int baseORder;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.faceTheCam.Add(gameObject);
        baseORder = GetComponent<SpriteRenderer>().sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Bestiole")
        {
            if(GameManager.instance.isShooting)
            {
                RaycastHit hit;
                if (Physics.Raycast(castPoints1.position, -transform.forward, out hit, 1000) || Physics.Raycast(castPoints2.position, -transform.forward, out hit, 1000))
                {
                    Debug.Log("hit");
                    if (hit.transform.gameObject.tag == "Bush")
                    {
                        Debug.Log("c'est un bush");
                        GetComponent<SpriteRenderer>().sortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;

                        Debug.DrawRay(castPoints1.position, -transform.forward * 100, Color.red);
                        Debug.DrawRay(castPoints2.position, -transform.forward * 100, Color.red);
                    }
                   
                }
                else
                {
                    GetComponent<SpriteRenderer>().sortingOrder = baseORder;
                }
            }
            

        }
    }
}
    

    
    
    
    
    
    
    
    

