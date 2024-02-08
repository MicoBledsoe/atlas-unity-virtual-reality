using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardVisualizer : MonoBehaviour
{
    public Material ThumbsUp, Wave;

    private Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void ShowThumbsUp()
    {
        rend.sharedMaterial = ThumbsUp;
    }

    public void ShowWave()
    {
        rend.sharedMaterial = Wave;
    }
}
