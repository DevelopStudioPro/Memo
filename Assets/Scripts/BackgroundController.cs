using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private float _blinkTime;

    private IEnumerator Blink (Color color)
    {
        var currentColor = _background.color;
        _background.color = color;

        yield return new WaitForSeconds(_blinkTime);
        
        _background.color = currentColor;
    }

    public void SetGreenBlink()
    {
        StartCoroutine(Blink(Color.green));
    }

    public void SetRedBlink()
    {
        StartCoroutine(Blink(Color.red));
    }
}
