using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using System.Numerics;

public class TestDotweenLight : MonoBehaviour
{
    private Light2D light;
    public Color noon;
    public Color night;
    public Color day;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        light=GetComponent<Light2D>();
        Sequence sequence = DOTween.Sequence().SetLoops(-1, LoopType.Yoyo);
        sequence.Append(DOTween.To(() => light.color, x => light.color = x, noon, speed));
        sequence.Join(DOTween.To(() => light.intensity, x => light.intensity = x, 0.7f, speed));
        sequence.Append(DOTween.To(() => light.color, x => light.color = x, night, speed));
        sequence.Join(DOTween.To(() => light.intensity, x => light.intensity = x, 0.3f, speed));
        sequence.Append(DOTween.To(() => light.color, x => light.color = x, day, speed));
        sequence.Join(DOTween.To(() => light.intensity, x => light.intensity = x, 1, speed));

    }

    // Update is called once per frame
    void Update()
    {
        //light.color = Vector4.Lerp(light.color, noon, speed*Time.deltaTime);
    }

    
}
