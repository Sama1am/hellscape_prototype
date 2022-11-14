using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class menuLighting : MonoBehaviour
{
    public float a = 0.3f;
    public float maxVal = 2f;
    public bool flickIntensity = true;
    public Light lightFlick;
    //[SerializeField] Light lightFlick;

    private void Start()
    {
        FlickIntensity();

        // Set color and position
        lightFlick = GameObject.FindGameObjectWithTag("light_menu").GetComponent<Light>();
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
                lightFlick.intensity = r;
                t = Random.Range(0, 0.5f);
                yield return wait;
            }
            else yield return null;
        }
    }
}
