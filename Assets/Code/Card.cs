using Code.Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code
{
    [RequireComponent(typeof(Image))]
    public class Card : MonoBehaviour
    {
        [Inject] private CardsConfig cardsConfig;
        [Inject] private CardsTexturesHolder cardsTexturesHolder;

        private Image _image;
        private Sprite _sprite;
        private bool _isFron;
        
        
        public string Name { get; private set; }

        public void Init(string name)
        {
            _image = GetComponent<Image>();
            Name = name;
            
            _sprite = cardsTexturesHolder.GetSprite(name);
            _image.sprite = _sprite;
            _isFron = true;
            
            Flip(true);
        }
        
        private void Flip(bool isPermanent = false)
        {
            _isFron = !_isFron;
            var time = isPermanent ? 0f : cardsConfig.FlipTime / 2f;
            transform
                .DORotate(transform.rotation.eulerAngles + Vector3.up * 90, time)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    _image.sprite = _isFron ? _sprite : cardsTexturesHolder.CardBackSprite;
                    transform
                        .DORotate(transform.rotation.eulerAngles + Vector3.up * 90, time);
                });
        }

        public Tween Move(Vector3 position, bool isFlip)
        {
            if(isFlip)
                Flip();
            
            return transform.DOMove(position, cardsConfig.FlipTime);
        }
    }
}