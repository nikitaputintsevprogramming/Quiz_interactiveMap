using UnityEngine;
using UnityEngine.UI; // Для работы с UI элементами
using System.IO;
using UI.Pagination;

public class SetImageOnShowPage : MonoBehaviour
{
    private Sprite sprite;

    public void SetImageToFirstPage(string image)
    {
        // Путь к изображению в папке StreamingAssets
        string imagePath = Path.Combine(Application.streamingAssetsPath, image);

        if (File.Exists(imagePath))
        {
            // Загрузить изображение
            byte[] imageData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);

            // Создать спрайт из текстуры
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Установить спрайт в Image компонент

        }
        else
        {
            Debug.LogError($"Image file not found at path: {imagePath}");
        }

        Image displayImage = FindObjectOfType<Page>().GetComponentInChildren<Image>();

        if (displayImage == null)
        {
            Debug.LogError("Image component is not assigned.");
            return;
        }
        displayImage.sprite = sprite;
    }
}
