using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class menuLighting : MonoBehaviour
{
    public float a = 0.3f;
    public float maxVal = 2f;
    public bool flickIntensity = true;
    Light _light;


    private void Start()
    {
        _light = GetComponent<Light>();
        FlickIntensity();
    }

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (flickIntensity==true)
            {
                t0 = Time.time;
                float r = Random.Range(a, maxVal);
                _light.intensity = r;
                t = Random.Range(0, 0.5f);
                yield return wait;
            }
            else yield return null;
        }
    }
}
