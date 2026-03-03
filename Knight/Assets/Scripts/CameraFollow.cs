using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek karakter (Knight)
    public float smoothSpeed = 0.125f; // Takip yumuşaklığı
    public Vector3 offset; // Kamera ile karakter arasındaki mesafe (Z ekseni -10 olmalı)

    void LateUpdate()
    {
        if (target != null)
        {
            // Hedeflenen pozisyon
            Vector3 desiredPosition = target.position + offset;
            
            // Yumuşak geçiş (Lerp)
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // Kamerayı güncelle
            transform.position = smoothedPosition;
        }
    }
}