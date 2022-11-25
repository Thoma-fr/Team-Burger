using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject vCam;
    [SerializeField] private GameObject bullet;
    
    // Update is called once per frame
    private void Start()
    {
        Destroy(gameObject, 2f);

    }
    void Update()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
        bullet.transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
    }
    private IEnumerator waiTospawn()
    {
        yield return new WaitForSeconds(0.5f);
        vCam.SetActive(true);
    }
}
