using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_shake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    [SerializeField] private bool shakeStatus;

    public IEnumerator shake()
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        shakeStatus = false;
    }

    private void Update()
    {
       if(shakeStatus)
        {
            StartCoroutine("shake");
        }
    }

    public void setShake(bool status)
    {
        shakeStatus = status;
    }
}
