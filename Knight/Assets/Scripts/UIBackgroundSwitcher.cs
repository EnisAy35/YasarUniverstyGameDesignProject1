using UnityEngine;
using UnityEngine.UI;

public class UIBackgroundSwitcher : MonoBehaviour
{
    [Header("Tema Ayarları")]
    public int backgroundNum; // 0'dan başlayan tema indeksi
    public Sprite[] layerSprites; // Tüm temaların sprite'ları (Tema1_L0, Tema1_L1... Tema2_L0...)
    
    [Header("UI Katman Grupları")]
    public GameObject[] layerObjects = new GameObject[5]; // Layer_0, Layer_1... objeleri
    
    private int maxBackgroundNum = 3;

    void Start()
    {
        // Eğer objeleri elle sürüklemediysen isimden bulmaya çalışır
        for (int i = 0; i < layerObjects.Length; i++)
        {
            if (layerObjects[i] == null)
                layerObjects[i] = GameObject.Find("Layer_" + i);
        }
        
        ChangeImages();
    }

    void Update()
    {
        // Test için klavye okları
        if (Input.GetKeyDown(KeyCode.RightArrow)) NextBG();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) BackBG();
    }

    void ChangeImages()
    {
        // İlk katman (Genelde en arkadaki sabit katman)
        layerObjects[0].GetComponent<Image>().sprite = layerSprites[backgroundNum * 5];

        // Diğer hareketli katmanlar (L1'den L4'e)
        for (int i = 1; i < layerObjects.Length; i++)
        {
            Sprite newSprite = layerSprites[backgroundNum * 5 + i];
            
            // Ana objenin resmini değiştir
            layerObjects[i].GetComponent<Image>().sprite = newSprite;

            // UI Paralaks sisteminde dikişsiz döngü için yan yana duran çocuk objeleri güncelle
            // Çocukların hiyerarşide 0 ve 1. sırada olduğunu varsayıyoruz
            if (layerObjects[i].transform.childCount >= 2)
            {
                layerObjects[i].transform.GetChild(0).GetComponent<Image>().sprite = newSprite;
                layerObjects[i].transform.GetChild(1).GetComponent<Image>().sprite = newSprite;
            }
        }
    }

    public void NextBG()
    {
        backgroundNum = (backgroundNum + 1 > maxBackgroundNum) ? 0 : backgroundNum + 1;
        ChangeImages();
    }

    public void BackBG()
    {
        backgroundNum = (backgroundNum - 1 < 0) ? maxBackgroundNum : backgroundNum - 1;
        ChangeImages();
    }
}