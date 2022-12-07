using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationController))]
public class Card : MonoBehaviour
{
    [SerializeField] private bool _isHaveImage;
    [SerializeField] private Sprite _cover;
    [SerializeField] private Sprite _notImage;
    [SerializeField] private GameObject _outline;
    
    private Sprite _image;
    private AnimationController _animationController;

    public bool IsHaveImage => _image != null;
    public Sprite Image { get { return _image; } set { _image = value; } }

    private void Start()
    {
        _animationController = GetComponent<AnimationController>();
    }

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

        RotateCards();
    }

    public void Close()
    {
        ChangeSprite(_cover);
        RotateCards();
    }

    private void RotateCards()
    {
        _animationController.RotateCard();
    }

    private void OnMouseDown()
    {
        Continue();
    }

    private void Continue()
    {
        var clickable = GameManager.Instance.IsClickableButtons;
        if (!clickable)
            return;

        var madeMistake = !IsHaveImage;
        GameManager.Instance.ContinueGame(madeMistake, this);
    }

    private void ChangeColor(Color color)
    {
        _outline.GetComponent<SpriteRenderer>().color = color;
    }

    private void SetBlink(Color color)
    {
        ChangeColor(color);
        _animationController.BlinkOutline();
    }

    public void SetGreenBlink()
    {
        SetBlink(Color.green);
    }

    public void SetRedBlink()
    {
        SetBlink(Color.red);
    }
}
