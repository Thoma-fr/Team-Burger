using DG.Tweening;
using UnityEngine;

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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Bullet"))
        {

            //Debug.Log("hit");
            if(cam!=null)
                cam.SetActive(true);
            
            //pc.mainCam.transform.GetComponent<Volume>().enabled = false;
            Destroy(collision.gameObject);
            GameManager.instance.OnBattleActivation(GetComponent<EnemyController>());
            Destroy(gameObject, 3f);
        }
    }
}
    

    
    
    
    
    
    
    
    

