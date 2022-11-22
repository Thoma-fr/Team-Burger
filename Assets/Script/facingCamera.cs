using DG.Tweening;
using UnityEngine;

public class facingCamera : MonoBehaviour
{
    public bool hasRotate;
    private Transform rot;
    public GameObject matPlane;
    public GameObject spriteImage;
    public bool isup;
    private PlayerController pc;
    public bool updateRot;

    private PlayerController.PLAYER_MODE playerenum;
    // Start is called before the first frame update
    void Start()
    {
        isup = true;
        playerenum = PlayerController.playerInstance.playerMode;
        if(matPlane!= null)
            matPlane.SetActive(false);
        
        GameManager.instance.faceTheCam.Add(gameObject);
        //baseORder = GetComponent<SpriteRenderer>().sortingOrder;
    }
    private void Update()
    {

        switch (PlayerController.playerInstance.playerMode)
        {
            case PlayerController.PLAYER_MODE.ADVENTURE_MODE:
                RotateUp();
                break;
            case PlayerController.PLAYER_MODE.SHOOTING_MODE:
                rotateTowardPlayer();

                break;
            case PlayerController.PLAYER_MODE.DIALOGUE_MODE:
                break;
            case PlayerController.PLAYER_MODE.COMBAT_MODE:
                transform.rotation = Camera.main.transform.rotation;
                break;
            default:
                break;
        }
    }
    
    // Update is called once per frame
    public void rotateTowardPlayer()
    {
        //rot = a;
        if (!hasRotate)
        {
            if (matPlane != null)
            {
                matPlane.SetActive(true);
                spriteImage.GetComponent<SpriteRenderer>().enabled = false;
            }
            Debug.Log("hello1");
            transform.DOLocalRotateQuaternion(GameManager.instance.mainCam.transform.rotation, 0.8f).SetEase(Ease.OutBounce).OnComplete(() => hasRotate = true);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.07f);
            hasRotate = true;
            isup = false;
        }
    }
    public void InstaRot(Transform a)
    {
        transform.rotation=a.transform.rotation;
    }
    public void RotateUp()
    {
        if (!isup)
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
            isup=true;
        }
    }
   
}
    

    
    
    
    
    
    
    
    

