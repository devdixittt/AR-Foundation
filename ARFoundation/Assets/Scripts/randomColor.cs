using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class randomColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.material.color = new Color(Random.value, Random.value, Random.value);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
