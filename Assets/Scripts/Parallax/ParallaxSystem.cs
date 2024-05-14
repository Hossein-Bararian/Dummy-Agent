using System.Collections.Generic;
using UnityEngine;

public class ParallaxSystem : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshRenderers = new();
    [SerializeField] private List<float> layerSpeed = new();
    private readonly List<Material> _material = new();
    private readonly List<float> _offset = new();
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");


    private void Awake()
    {
        foreach (var mesh in meshRenderers)
        {
            _material.Add(mesh.material);
            _offset.Add(0);
        }
      
    }
    private void Update()
    {
        for (int i = 0; i < _material.Count; i++)
        {
            _offset[i] += Time.deltaTime * layerSpeed[i];
            _material[i].SetTextureOffset(MainTex,_offset[i]*Vector2.right);
        }
    }
}