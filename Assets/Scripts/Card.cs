using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private bool _isOpened;
    [SerializeField] private bool _isHaveImage;
    [SerializeField] private Sprite _cover;
    [SerializeField] private Sprite _image;
    [SerializeField] private Vector2 _position;

    public Sprite Image { get { return _image; } set { _image = value; } }

    public void ChangeSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Open()
    {
        if (_image)
            ChangeSprite(_image);
        else
            Debug.LogError("Нет изображения");
    }

    public void Close()
    {
        if (_image)
            ChangeSprite(_cover);
        else
            Debug.LogError("Нет обложки");
    }
}
