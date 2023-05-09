using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Color collectColor = Color.yellow;
    private Renderer _renderer = null;
    private MaterialPropertyBlock _materialPropertyBlock = null;
    public int myListNum;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }
    public void CollectAreaProceses()
    {
        transform.tag = "Untagged";
       _materialPropertyBlock.SetColor("_Color",collectColor);
        gameObject.layer =7;
        transform.GetComponent<MeshRenderer>().material.color=Color.yellow;

    }
    public void CollectAreaProcesesAI()
    {
        transform.tag = "Untagged";
        gameObject.layer = 7;
        transform.GetComponent<MeshRenderer>().material.color = Color.red;

    }
}
