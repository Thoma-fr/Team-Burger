using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class facingCamera : MonoBehaviour
{
    public Transform castPoints1, castPoints2;
    private int baseORder;
    public bool hasRotate;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.faceTheCam.Add(gameObject);
        //baseORder = GetComponent<SpriteRenderer>().sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void rotateTowardPlayer(Transform a)
    {
        //float rotz = transform.rotation.z - z;
        
        //transform.DORotate(new Vector3(180, 180,z ), 1f).SetEase(Ease.OutElastic);
        if (!hasRotate)
            
        {
            //transform.DORotate(new Vector3(0, 90, a.transform.rotation.z), 0f);
            Debug.Log(a.transform.rotation.y);
            
            //transform.localRotation = a.transform.rotation;
            //transform.DOLocalRotate(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z-20), 1f);
            transform.DOLocalRotateQuaternion(a.transform.rotation, 1f).SetEase(Ease.OutBounce);
            hasRotate = true;
        }
        else
        {
            Debug.Log("func2");
            transform.DORotate(new Vector3(0, 0, 0), 0f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.02f);
            //transform.rotation = Quaternion.Euler(78, 0, -180);
            hasRotate = false;
        }
        //return true;
    }
    public void rotateUp()
    {
        //transform.DORotate(new Vector3(78, 0, -180), 0.1f).SetEase(Ease.OutBounce);
        
        //return false;
    }
}
    

    
    
    
    
    
    
    
    

