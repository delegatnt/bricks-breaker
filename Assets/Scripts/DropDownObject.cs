using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DDObjectTypes
{
    Expand,
    Collaps,
    SpeedUp,
    SpeedDown,
    Live
}
public class DropDownObject : MonoBehaviour
{
    public DDObjectTypes type;
    public float speed = 3f;

    private SpriteRenderer spriteRenderer;

    public Sprite expandSprite;
    public Sprite collapsSprite;
    public Sprite speedUpSprite;
    public Sprite speedDownSprite;
    public Sprite liveSprite;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        type = this.setType();

        switch(type)
        {
            case DDObjectTypes.Expand: spriteRenderer.sprite = expandSprite; break;
            case DDObjectTypes.Collaps: spriteRenderer.sprite = collapsSprite; break;
            case DDObjectTypes.SpeedUp: spriteRenderer.sprite = speedUpSprite; break;
            case DDObjectTypes.SpeedDown: spriteRenderer.sprite = speedDownSprite; break;
            case DDObjectTypes.Live: spriteRenderer.sprite = liveSprite; break;
        }
    }
    
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private DDObjectTypes setType()
    {
        float rand = Random.Range(0f, 1f);
        if (rand >= 0.8f) return DDObjectTypes.Expand;
        else if (rand >= 0.6f) return DDObjectTypes.Collaps;
        else if (rand >= 0.4f) return DDObjectTypes.SpeedDown;
        else if (rand >= 0.2f) return DDObjectTypes.SpeedUp;
        else return DDObjectTypes.Live;
    }
}
