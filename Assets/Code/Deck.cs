using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

namespace Code
{
    public class Deck : MonoBehaviour, IPointerClickHandler
    {
        [Inject] private CardsTexturesHolder cardsTexturesHolder;
        [Inject] private DiContainer container;

        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform cardsHolder;

        [Space, Header("Points")]
        [SerializeField] private Transform deckPoint;
        [SerializeField] private Transform firstCardPoint;
        [SerializeField] private Transform secondCardPoint;
        [SerializeField] private Transform thirdCardPoint;

        private List<Card> _cards;
        private int _currentIndex;
        private bool _isReload;

        private void Start()
        {
            FillDeck();
        }

        private void FillDeck()
        {
            ClearCards();

            var names = cardsTexturesHolder.CardNames;
            foreach (var cardName in names)
            {
                var card = container.InstantiatePrefabForComponent<Card>(cardPrefab, cardsHolder);
                card.Init(cardName);
                
                _cards.Add(card);
            }

            ShuffleCards();
            UpdateHierarchyOrder();
        }

        private void ShuffleCards()
        {
            var count = _cards.Count;

            for (var i = count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);

                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }
        
        void UpdateHierarchyOrder()
        {
            for (var i = 0; i < _cards.Count; i++)
            {
                _cards[i].transform.SetSiblingIndex(_cards.Count - i);
            }
        }

        private void ClearCards()
        {
            _cards ??= new List<Card>();

            foreach (var card in _cards)
            {
                Destroy(card.gameObject);
            }

            _cards.Clear();
        }

        private void MoveCards()
        {
            if (_currentIndex >= _cards.Count)
            {
                _currentIndex = 0;
                for (var i = 0; i < _cards.Count; i++)
                {
                    if(i != _cards.Count -1)
                        _cards[i].Move(deckPoint.position, true);
                    else
                        _cards[i].Move(deckPoint.position, true).OnComplete(() => _isReload = false);;
                }
                return;
            }

            var previousIndex = _currentIndex - 1;
            var earlierIndex = _currentIndex - 2;

            var currentCard = (_currentIndex >= 0 && _currentIndex < _cards.Count) ? _cards[_currentIndex] : null;
            var previousCard = (previousIndex >= 0 && previousIndex < _cards.Count) ? _cards[previousIndex] : null;
            var earlierCard = (earlierIndex >= 0 && earlierIndex < _cards.Count) ? _cards[earlierIndex] : null;

            currentCard.Move(firstCardPoint.position, true)
                .OnComplete(() => _isReload = false);
            currentCard.transform.SetAsLastSibling();
            
            if (previousCard != null)
                previousCard.Move(secondCardPoint.position, false);

            if (earlierCard != null)
                earlierCard.Move(thirdCardPoint.position, false);

            _currentIndex++;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_isReload)
                return;

            _isReload = true;
            MoveCards();
        }
    }
}