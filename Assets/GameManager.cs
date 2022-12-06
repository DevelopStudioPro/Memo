using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private List<Transform> _positions;
    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Sprite> _images;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Sprite spr;

    private void Start()
    {
        CreateCards();
        SetRandomImage();
        StartCoroutine(OpenImage());
    }

    private void CreateCards()
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            var card = Instantiate(_cardPrefab, _positions[i], false);

            _cards.Add(card.GetComponent<Card>());
        }
    }

    private void SetRandomImage()
    {
        foreach (var card in _cards)
        {
            if (card.GetComponent<Card>().Image != null)
                card.GetComponent<Card>().Image = null;
        }

        var randomImage = _images[Random.Range(0, _images.Count)];
        _cards[Random.Range(0, _cards.Count)].GetComponent<Card>().Image = randomImage;
    }

    private IEnumerator OpenImage()
    {
        Card currentCard = null;
        foreach (var card in _cards)
        {
            if (card.GetComponent<Card>().Image != null)
            {
                card.Open();
                currentCard = card;
            }   
        }

        yield return new WaitForSeconds(2);

        if (currentCard != null)
            currentCard.Close();
        else
            Debug.Log("Изображения нет ни в одной карте");

    }
}
