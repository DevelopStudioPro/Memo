using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private bool _isHaveImage;
    [SerializeField] private Sprite _cover;
    [SerializeField] private Sprite _image;
    [SerializeField] private Sprite _notImage;

    public bool IsHaveImage => _image != null;
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
            ChangeSprite(_notImage);
    }

    public void Close()
    {
        ChangeSprite(_cover);
    }

    private void OnMouseDown()
    {
        var clickable = GameManager.Instance.IsClickableButtons;
        if (!clickable)
            return;

        var madeMistake = !IsHaveImage;
        GameManager.Instance.ContinueGame(madeMistake);
    }
}
