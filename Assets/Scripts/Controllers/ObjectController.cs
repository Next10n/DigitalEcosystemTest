using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    SpriteRenderer spriteRender;
    MeshRenderer meshRender;

    private void Awake()
    {
        TryGetComponent<SpriteRenderer>(out spriteRender);
        TryGetComponent<MeshRenderer>(out meshRender);
    }


    public void SetActive(bool flag)
    {        
        if(spriteRender != null)
            spriteRender.enabled = flag;
        
        if (meshRender != null)
            meshRender.enabled = flag;
    }
}
