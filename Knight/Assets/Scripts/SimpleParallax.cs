using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    // Hızları ve objeleri Inspector'dan tek tek ekleyeceğiz
    [Header("Arka Plan Katmanları")]
    public Transform[] layers;      // Sahnedeki Layer0, Layer1 vb. objeleri buraya sürükle
    public float[] scrollSpeeds;    // Her katman için bir hız değeri (0 ile 1 arası)

    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        // Kameranın ne kadar yer değiştirdiğini hesapla
        float deltaX = cam.position.x - lastCamPos.x;

        // Her katmanı kendi hızıyla hareket ettir
        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] != null)
            {
                // Obje kamerayla beraber gitsin ama hız çarpanı kadar "geride" kalsın
                float parallax = deltaX * scrollSpeeds[i];
                layers[i].position += new Vector3(parallax, 0, 0);
            }
        }

        lastCamPos = cam.position;
    }
}