using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

public class facingCamera : MonoBehaviour
{
    public Transform castPoints1, castPoints2;
    private int baseORder;
    public bool hasRotate;
    private Transform rot;
    public GameObject matPlane;
    public GameObject spriteImage;
    public GameObject cam;
    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        if(matPlane!= null)
            matPlane.SetActive(false);
        
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
        rot = a;
        //transform.DORotate(new Vector3(180, 180,z ), 1f).SetEase(Ease.OutElastic);
        if (!hasRotate)
            
        {
            //transform.DORotate(new Vector3(0, 90, a.transform.rotation.z), 0f);
            //Debug.Log(a.transform.rotation.y);

            //transform.localRotation = a.transform.rotation;
            //transform.DOLocalRotate(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z-20), 1f);
            if (matPlane != null)
            {
                matPlane.SetActive(true);
                spriteImage.GetComponent<SpriteRenderer>().enabled = false;
            }
            transform.DOLocalRotateQuaternion(a.transform.rotation, 1f).SetEase(Ease.OutBounce).OnComplete(() => hasRotate = true);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.07f);
            hasRotate = true;
        }
        else
        {
            if (matPlane != null)
            {
                matPlane.SetActive(false);
                spriteImage.GetComponent<SpriteRenderer>().enabled = true;
            }
            Debug.Log("func2");
            transform.DORotate(new Vector3(0, 0, 0), 0f).OnComplete(() => hasRotate = false);
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet"))
        {

            //Debug.Log("hit");
            if(cam!=null)
                cam.SetActive(true);
            
            pc.mainCam.transform.GetComponent<Volume>().enabled = false;
            Destroy(collision.gameObject);
        }
    }
}
    

    
    
    
    
    
    
    
    

