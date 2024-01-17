using System.Collections;
using UnityEngine;

public class BackgroundCrossfade : MonoBehaviour
{
    public SpriteRenderer[] backgrounds; 
    public float displayTime = 5f; 
    public float fadeTime = 2f; 

    private int currentBackgroundIndex;
    private float timer;

    void Start()
    {
        foreach (SpriteRenderer renderer in backgrounds)    
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        }

        if (backgrounds.Length > 0)
        {
            backgrounds[0].color = new Color(backgrounds[0].color.r, backgrounds[0].color.g, backgrounds[0].color.b, 1);
        }

        currentBackgroundIndex = 0;
        timer = displayTime;
    }

    void Update()
    {
        if (backgrounds.Length < 2)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            StartCoroutine(FadeBackgrounds());
            timer = displayTime + fadeTime;
        }
    }

    IEnumerator FadeBackgrounds()
    {
        int nextBackgroundIndex = (currentBackgroundIndex + 1) % backgrounds.Length;
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);

            backgrounds[currentBackgroundIndex].color = new Color(backgrounds[currentBackgroundIndex].color.r, backgrounds[currentBackgroundIndex].color.g, backgrounds[currentBackgroundIndex].color.b, alpha);
            backgrounds[nextBackgroundIndex].color = new Color(backgrounds[nextBackgroundIndex].color.r, backgrounds[nextBackgroundIndex].color.g, backgrounds[nextBackgroundIndex].color.b, 1 - alpha);

            yield return null;
        }

        currentBackgroundIndex = nextBackgroundIndex;
    }
}
