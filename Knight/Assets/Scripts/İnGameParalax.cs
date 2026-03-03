using Unity.Android.Gradle;
using UnityEngine;
[System.Serializable]

public class BackGroundElements{
    public SpriteRenderer backGroundSprite;
    [Range(0,1)]public float scrollSpeed;
    [HideInInspector]public Material spriteMaterial;

}
public class İnGameParalax : MonoBehaviour
{
    private const float SCROLL_MULTIPLIER = 0.01f;

    [SerializeField] private BackGroundElements[] backGroundElements;

    private void Start()
    {
        foreach (BackGroundElements element in backGroundElements)
        {
            element.spriteMaterial = element.backGroundSprite.material;
        }
    }
    private void Update()
    {
         foreach (BackGroundElements element in backGroundElements)
        {
            element.spriteMaterial.mainTextureOffset = new Vector2(transform.position.x * element.scrollSpeed * SCROLL_MULTIPLIER, 0);
        }
    }



}
