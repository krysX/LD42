using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAppearAnimation : MonoBehaviour {

    public float apperanceSpeed;

    SpriteRenderer sr;
    Color originalColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        StartCoroutine("AppearAnimation");
    }

    

    public void SetSpeed(float speed)
    {
        apperanceSpeed = speed;
    }

    public IEnumerator AppearAnimation()
    {
        float lerpValue = 0f;
        Color startColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        while(lerpValue < 1)
        {
            sr.color = Color.Lerp(startColor, Color.white, lerpValue);
            lerpValue += apperanceSpeed * 2 * Time.deltaTime;
            yield return null;
        }

        lerpValue = 0f;

        while(lerpValue < 1)
        {
            sr.color = Color.Lerp(Color.white, originalColor, lerpValue);
            lerpValue += apperanceSpeed * 2 * Time.deltaTime;
            yield return null;
        }
    }
}
