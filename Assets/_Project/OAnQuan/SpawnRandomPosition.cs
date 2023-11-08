using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpawnRandomPosition : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float _imageWidth;
    private float _imageHeight;

    public Vector3 Get()
    {
        RectTransform rectTransform = _image.rectTransform;
        _imageWidth = rectTransform.rect.width;
        _imageHeight = rectTransform.rect.height;
        float randomX = Random.Range(-_imageWidth / 2f, _imageWidth / 2f);
        float randomY = Random.Range(-_imageHeight / 2f, _imageHeight / 2f);

        return new Vector3(_image.transform.position.x + randomX, _image.transform.position.y + randomY, 0f);
    }
}
