using DG.Tweening;
using UnityEngine;

public class facingCamera : MonoBehaviour
{
    public bool hasRotate;
    private Transform rot;
    public GameObject matPlane;
    public GameObject spriteImage;
    
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
    public void rotateTowardPlayer(Transform a)
    {
        rot = a;
        if (!hasRotate)
            
        {
           
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
            hasRotate = false;
        }
    }
   
}
    

    
    
    
    
    
    
    
    

