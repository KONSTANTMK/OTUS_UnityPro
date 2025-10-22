using UnityEngine;

namespace Code.OOP
{
    public class CompositionRoot : MonoBehaviour
    {
        private CharacterMoveController _characterController;
        private CharacterFireController _characterFireController;

        private void Start()
        {
            var characterData = new CharacterData(3.0f);
            var characterView = FindObjectOfType<CharacterView>();
            _characterController = new CharacterMoveController(characterData, characterView);
            _characterFireController = new CharacterFireController(characterData, characterView);
        }
        
        private void Update()
        {
            _characterController.OnUpdate();
            _characterFireController.OnUpdate();
        }
    }
}
