using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasimaBandi : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    public float bantHizi = .5f;

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            renderer.material.SetTextureOffset("_MainTex",new Vector2(0,-Time.time * bantHizi));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Time.timeScale != 0)
        {
            other.transform.Translate((bantHizi *3) * Time.deltaTime * Vector3.back, Space.World); 
        }
    }
}
