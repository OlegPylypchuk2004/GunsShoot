using Cysharp.Threading.Tasks;
using GameModeSystem;
using Global;
using System;
using UnityEngine;

namespace StageSystem
{
    public class StageManager
    {
        private StagesWrapper _stagesWrapper;

        public event Action StagesLoaded;
        public event Action<int> StageNumberChanged;
        public event Action StagesAreOver;

        public StageManager()
        {
            StageNumber = 1;
        }

        public int StageNumber { get; private set; }

        public int StagesLeft
        {
            get
            {
                if (_stagesWrapper == null)
                {
                    return 0;
                }

                return _stagesWrapper.Stages.Length + 1 - StageNumber;
            }
        }

        public StageData StageData
        {
            get
            {
                if (_stagesWrapper == null)
                {
                    return null;
                }

                int stageIndex = StageNumber - 1;

                if (stageIndex > _stagesWrapper.Stages.Length - 1)
                {
                    if (LocalGameData.GameModeConfig.Type == GameModeType.Endless)
                    {
                        stageIndex = _stagesWrapper.Stages.Length - 1;
                    }
                    else
                    {
                        StagesAreOver?.Invoke();

                        return null;
                    }
                }

                return _stagesWrapper.Stages[stageIndex];
            }
        }

        public async UniTask LoadStages()
        {
            JsonDataLoader jsonDataLoader = new JsonDataLoader();
            _stagesWrapper = await jsonDataLoader.LoadJsonDataAsync<StagesWrapper>(LocalGameData.GameModeConfig.GetStagesConfigFilePath());

            if (_stagesWrapper == null)
            {
                Debug.LogError("Failed to load stages config file.");
            }
            else
            {
                Debug.Log("Stages config file loading completed.");

                StagesLoaded?.Invoke();
            }
        }

        public void IncreaseStageNumber()
        {
            StageNumber++;

            StageNumberChanged?.Invoke(StageNumber);
        }
    }
}