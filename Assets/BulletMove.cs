using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    public GameObject vCam;
    // Update is called once per frame
    private void Start()
    {
        Destroy(gameObject, 5f);

    }
    void Update()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
    }
    private IEnumerator waiTospawn()
    {
        yield return new WaitForSeconds(0.5f);
        vCam.SetActive(true);
    }
}
