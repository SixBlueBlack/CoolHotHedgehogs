using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    public Color Color = Color.white;

    [Range(0, 16)]
    public int OutlineSize = 2;

    private SpriteRenderer spriteRenderer;

    public bool IsOutlined;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateOutline(true);
    }

    void OnDisable()
    {
        UpdateOutline(false);
    }

    void Update()
    {
        UpdateOutline(IsOutlined);
    }

    void UpdateOutline(bool outline)
    {
        var mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", Color);
        mpb.SetFloat("_OutlineSize", OutlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}