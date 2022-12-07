using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _shufflePosition;
    [SerializeField] private ShuffleController _shuffleController;

    [SerializeField] private List<Card> _cards;
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private List<Sprite> _images;

    [SerializeField] private float _pauseBeforeShowImage;
    [SerializeField] private float _pauseShowImage;

    [SerializeField] private UnityEvent OnTrueAnswer;
    [SerializeField] private UnityEvent OnFalseAnswer;

    private bool _isClickable;
    private Card _currentCard;
    

    public bool IsClickableButtons => _isClickable;
    public Transform ShufflePosition => _shufflePosition;
    public List<Card> CardsList => _cards;
    public ShuffleController ShuffleController => _shuffleController;

    private void Start()
    {
        CreateCards();
        StartGame();
    }

    private void StartGame()
    {
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
        _currentCard = _cards[Random.Range(0, _cards.Count)];
        _currentCard.GetComponent<Card>().Image = randomImage;
    }

    private IEnumerator OpenImage()
    {
        _isClickable = false;

        yield return new WaitForSeconds(_pauseBeforeShowImage);

        foreach (var card in _cards)
            card.Open();

        yield return new WaitForSeconds(_pauseShowImage);

        foreach (var card in _cards)
            card.Close();

        _isClickable = true;
    }

    public void ContinueGame(bool madeMistake, Card card)
    {
        if (madeMistake)
        {
            OnFalseAnswer.Invoke();
            card.SetRedBlink();
        }
        else
        {
            OnTrueAnswer.Invoke();
            card.SetGreenBlink();
        }

        StartCoroutine(ContinueWithPause());
    }

    private IEnumerator ContinueWithPause()
    {
        yield return new WaitForSeconds(1);

        _shuffleController.Shuffle();
        StartGame();
    }
}
