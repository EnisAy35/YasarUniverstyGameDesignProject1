using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class LevelManager : MonoBehaviour
{
    [Header("Ayarlar")]
    [Tooltip("Yüklenecek sahnenin tam adını buraya yazın.")]
    public string levelName;

    // Oyunu başlatan fonksiyon
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogError("Hata: Yüklenecek sahne ismi boş! Lütfen Inspector üzerinden bir isim atayın.");
        }
    }

    // Oyundan çıkış yapan fonksiyon
    public void QuitGame()
    {
        Debug.Log("Oyundan çıkılıyor..."); // Editörde çalıştığını anlamak için
        Application.Quit();
    }
}