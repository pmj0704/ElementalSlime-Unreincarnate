﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField] Texture2D Lava;
    private MeshRenderer meshRenderer = null;
    private Material material = null;
    private Vector2 offset = Vector2.zero;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = GetComponent<Material>();
    }

    void Update()
    {
        if (meshRenderer == null) return;
        offset.y += speed * Time.deltaTime;
        meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
    public IEnumerator NextLavaStage()
        {
            yield return new WaitForSeconds (0f);
            meshRenderer.material.SetTexture("_MainTex",Lava);
        }
}
