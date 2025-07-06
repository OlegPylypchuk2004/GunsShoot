using SceneManagment;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.SectionSystem
{
    public class MainSection : Section
    {
        [Space(10f), SerializeField] private Button _shopButton;
        [SerializeField] private ShopSection _shopSection;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _shopButton.onClick.AddListener(OnShopButtonClicked);
        }

        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(OnShopButtonClicked);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _sceneLoader.Load(2);
            }
        }

        private void OnShopButtonClicked()
        {
            _sectionChanger.Change(_shopSection);
        }
    }
}