using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleController : MonoBehaviour
{
    private List<Card> _cards;
    private bool _inside;
    private bool _outside;

    [SerializeField] private List<Transform> _transforms;

    void Start()
    {
        _cards = GameManager.Instance.CardsList;
    }

    private void Update()
    {
        if (_inside)
        {
            foreach(var card in _cards)
                card.transform.position = Vector2.MoveTowards(card.transform.position, new Vector2(0, 0), 10 * Time.deltaTime);
        }

        if (_outside)
        {
            for(int i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.position = Vector2.MoveTowards(_cards[i].transform.position, _transforms[i].position, 10 * Time.deltaTime);
            }
        }   
    }

    private IEnumerator ShuffleCards()
    {
        float timer = 0;
        while (timer < 0.3f)
        {
            if (!_inside)
                _inside = true;

            timer += Time.deltaTime;
            yield return null;
        }
        _inside = false;

        while(timer > 0.29f && timer < 0.6f)
        {
            if (!_outside)
                _outside = true;

            timer += Time.deltaTime;
            yield return null;
        }
        _outside = false;
    }

    public void Shuffle()
    {
        StartCoroutine(ShuffleCards());
    }
}
