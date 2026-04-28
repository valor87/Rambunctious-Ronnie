using TMPro;
using UnityEngine;

public class ConstantFading : MonoBehaviour
{
    TextMeshPro textMesh;
    public float textFadeSpeed = 2;

    public bool fadingOut = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        FadeInAndOut();
    }

    void FadeInAndOut()
    {
        Color newColor = textMesh.color;

        if (!fadingOut)
        {
            newColor.a += Time.deltaTime / textFadeSpeed;
            if (newColor.a > 1)
            {
                newColor.a = 1;
                fadingOut = true;
            }
        }   
        else
        {
            newColor.a -= Time.deltaTime / textFadeSpeed;
            if (newColor.a < 0)
            {
                newColor.a = 0;
                fadingOut = false;
            }
        }
            
        textMesh.color = newColor;
    }
}
