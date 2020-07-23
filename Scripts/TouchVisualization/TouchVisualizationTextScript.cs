using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TouchVisualizationTextScript : MonoBehaviour
{
    private float lifetime = 1.0f;
    private float startTime;

    TextMeshProUGUI textmeshPro;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        textmeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = 1.0f - (Time.time - startTime);
        textmeshPro.color = new Color(textmeshPro.color.r, textmeshPro.color.b, textmeshPro.color.g, alpha);

        if (Time.time - startTime > lifetime)
            Destroy(gameObject);
    }

    public void SetInitial(float x, float y, int fingerID)
    {
        TextMeshProUGUI textmeshPro = GetComponent<TextMeshProUGUI>();
        textmeshPro.text = "X,Y: " + (int)x + ", " + (int)y + "\n" + "ID: " + fingerID;
    }
}
