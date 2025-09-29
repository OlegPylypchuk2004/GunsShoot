using CurrencyManagment;
using SaveSystem;
using SecretCodeSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.SectionSystem
{
    public class SecretCodeSection : Section
    {
        [Space(10f), SerializeField] private Button _backButton;
        [SerializeField] private Section _previousSection;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _enterButton;
        [SerializeField] private TMP_Text _hintTextMesh;

        private CurrencyWallet _currencyWallet;
        private SecretCodeConfig[] _codeConfigs;
        private SecretCodeConfig _enteredCodeConfig;

        [Inject]
        private void Construct(CurrencyWallet currencyWallet)
        {
            _currencyWallet = currencyWallet;
        }

        private void Awake()
        {
            _codeConfigs = Resources.LoadAll<SecretCodeConfig>("Configs/SecretCodes");
        }

        private void OnEnable()
        {
            _inputField.text = string.Empty;
            _enterButton.interactable = false;
            _hintTextMesh.text = string.Empty;

            _backButton.onClick.AddListener(OnBackButtonClicked);
            _inputField.onValueChanged.AddListener(OnInputFieldClicked);
            _enterButton.onClick.AddListener(OnEnterButtonClicked);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            _inputField.onValueChanged.RemoveListener(OnInputFieldClicked);
            _enterButton.onClick.RemoveListener(OnEnterButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            _sectionChanger.Change(_previousSection);
        }

        private void OnInputFieldClicked(string value)
        {
            if (value == string.Empty)
            {
                _enterButton.interactable = false;
                _hintTextMesh.text = string.Empty;
            }

            _enteredCodeConfig = FindCode(value);

            if (_enteredCodeConfig == null)
            {
                _enterButton.interactable = false;
                _hintTextMesh.text = "The code is not valid";
            }
            else
            {
                if (IsAlreadyEntered(value))
                {
                    _enterButton.interactable = false;
                    _hintTextMesh.text = "The code has already been used";
                }
                else
                {
                    _enterButton.interactable = true;
                    _hintTextMesh.text = "The code is valid";
                }
            }
        }

        private void OnEnterButtonClicked()
        {
            if (_enteredCodeConfig == null)
            {
                return;
            }

            if (IsAlreadyEntered(_enteredCodeConfig.Key))
            {
                return;
            }

            if (_enteredCodeConfig == null)
            {
                _enteredCodeConfig = FindCode(_inputField.text);
            }

            if (_enteredCodeConfig != null && _enteredCodeConfig.Reward != null)
            {
                _currencyWallet.TryIncrease(_enteredCodeConfig.Reward);
            }

            SaveManager.Data.EnteredSecretCodes.Add(_enteredCodeConfig.Key);
            SaveManager.Save();

            _inputField.text = string.Empty;
            _hintTextMesh.text = string.Empty;
        }

        private SecretCodeConfig FindCode(string key)
        {
            foreach (SecretCodeConfig codeConfig in _codeConfigs)
            {
                if (codeConfig.Key.Equals(key))
                {
                    return codeConfig;
                }
            }

            return null;
        }

        private bool IsAlreadyEntered(string key)
        {
            return SaveManager.Data.EnteredSecretCodes.Contains(_enteredCodeConfig.Key);
        }
    }
}