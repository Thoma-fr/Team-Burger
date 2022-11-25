using DG.Tweening;
using UnityEngine;

public class facingCamera : MonoBehaviour
{
    private bool hasRotate;
    private bool isup;

    // Start is called before the first frame update
    void Start()
    {
        isup = true;
        //GameManager.instance.faceTheCam.Add(gameObject);
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
    public void rotateTowardPlayer()
    {
        if (!hasRotate)
        {
            Debug.Log("hello1");
            transform.DOLocalRotateQuaternion(PlayerController.Instance.FPSvcam.transform.rotation, 0.8f).SetEase(Ease.OutBounce).OnComplete(() => hasRotate = true);
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
            Debug.Log("func2");
            transform.DORotate(new Vector3(0, 0, 0), 0f).OnComplete(() => hasRotate = false);
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.02f);
            hasRotate = false;
            isup=true;
        }
    }
   
}
    

    
    
    
    
    
    
    
    

