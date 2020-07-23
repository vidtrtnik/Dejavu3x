using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchVisualizationScript : MonoBehaviour
{
    private float lifetime = 1.0f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Image img = GetComponent<Image>();
        float alpha = 1.0f - (Time.time - startTime);
        img.color = new Color(img.color.r, img.color.b, img.color.g, alpha);

        if (Time.time - startTime > lifetime)
            Destroy(gameObject);
    }
    
    /*public void SetInitial(int fingerID, float lifetime)
    {
        this.fingerID = fingerID;
        this.lifetime = lifetime;
        this.startTime = Time.time;
    }*/
}