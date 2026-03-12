using UnityEngine;
using UnityEngine.UI;

public class UIParallaxManager : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public bool isMoving = true;
    public float globalSpeed = 100f;

    [Header("Katman Ayarları")]
    public RectTransform[] layerObjects; 
    public float[] layerSpeedMultiplier; 

    [Header("Görsel Genişliği")]
    public float imageWidth = 1920f; 

    private float[] startPosX;

    void Start()
    {
        startPosX = new float[layerObjects.Length];

        for (int i = 0; i < layerObjects.Length; i++)
        {
            if (layerObjects[i] != null)
            {
                // 1. Orijinal objenin başlangıç pozisyonunu kaydet
                startPosX[i] = layerObjects[i].anchoredPosition.x;

                // 2. OTOMATİK KOPYALAMA: 
                // Boşluğu doldurmak için her katmanın yanına bir tane daha oluşturuyoruz.
                GameObject twin = Instantiate(layerObjects[i].gameObject, layerObjects[i].parent);
                RectTransform twinRect = twin.GetComponent<RectTransform>();
                
                // Kopyayı orijinalin tam sağın yerleştir (imageWidth kadar sağa)
                Vector2 twinPos = layerObjects[i].anchoredPosition;
                twinPos.x += imageWidth;
                twinRect.anchoredPosition = twinPos;

                // Kopyanın içindeki scripti sil (çakışmasınlar)
                Destroy(twin.GetComponent<UIParallaxManager>());
                
                // Kopyayı orijinalin "child"ı yaparsan beraber hareket ederler
                twin.transform.SetParent(layerObjects[i]);
                twinRect.anchoredPosition = new Vector2(imageWidth, 0); 
            }
        }
    }

    void Update()
    {
        if (!isMoving) return;

        for (int i = 0; i < layerObjects.Length; i++)
        {
            if (layerObjects[i] == null) continue;

            float speed = globalSpeed * layerSpeedMultiplier[i];
            Vector2 currentPos = layerObjects[i].anchoredPosition;
            
            // Sola kaydır
            currentPos.x -= speed * Time.deltaTime;

            // DÖNGÜ KONTROLÜ:
            // Eğer ana obje tamamen sola çıktıysa, sanki hiç hareket etmemiş gibi
            // başlangıç pozisyonuna geri ışınla. 
            // Yanındaki kopya sayesinde bu geçiş fark edilmeyecek.
            if (currentPos.x <= startPosX[i] - imageWidth)
            {
                currentPos.x = startPosX[i];
            }

            layerObjects[i].anchoredPosition = currentPos;
        }
    }
}