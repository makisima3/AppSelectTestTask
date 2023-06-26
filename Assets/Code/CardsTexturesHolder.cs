using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Code
{
    public class CardsTexturesHolder : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;

        private Dictionary<string, Sprite> _nameToSprite;
        private Sprite _cardBackSprite;

        public Sprite CardBackSprite => _cardBackSprite;
        public string[] CardNames => _nameToSprite.Select(nts => nts.Key).ToArray();

        private void Awake()
        {
            _nameToSprite = new Dictionary<string, Sprite>();

            FillSprites();
        }
        
        private void FillSprites()
        {
            foreach (var sprite in sprites)
            {
                if(sprite.name != "Back")
                    _nameToSprite.Add(sprite.name, sprite);
                else
                    _cardBackSprite = sprite;
            }

           
        }

        /*private void LoadSpritesFromFolders(string mainFolderPath)
        {
            var subfolderPaths = System.IO.Directory.GetDirectories(Application.dataPath + mainFolderPath);

            var backTexture = Resources.Load<Texture2D>(mainFolderPath + "/Back");
            _cardBackSprite = Sprite.Create(backTexture, new Rect(0, 0, backTexture.width, backTexture.height), Vector2.zero);
            
            foreach (var folderPath in subfolderPaths)
            {
                var folderName = new System.IO.DirectoryInfo(folderPath).Name;
                var textures = Resources.LoadAll<Texture2D>(mainFolderPath + "/" + folderName);

                foreach (var texture in textures)
                {
                    var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    sprite.name = texture.name;
                    _nameToSprite.Add(sprite.name, sprite);
                }
            }
        }*/

        public Sprite GetSprite(string name)
        {
            // Проверка на наличие ключа не нужна, т.к. мы не можешь хотеть получить ту картку, какой нет в проекте.
            return _nameToSprite[name];
        }
        
    }
}