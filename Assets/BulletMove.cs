using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 1;
    public GameObject vCam;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*1 * Time.deltaTime);
    }
    private IEnumerator waiTospawn()
    {
        yield return new WaitForSeconds(0.5f);
        vCam.SetActive(true);
    }
}
