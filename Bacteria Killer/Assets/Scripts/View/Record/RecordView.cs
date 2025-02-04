using Data;
using Services.SaveLoad;
using TMPro;
using UnityEngine;
using Utils.SaveKeys;
using Zenject;

namespace View.Record
{
    public class RecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _recordValueText;

        private ISaveLoadService _saveLoadService;
        
        [Inject]
        public void Initialize(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            Load();
        }

        private void Load()
        {
            var data = _saveLoadService.Load<ScoreData>(SaveKeys.SCORE);

            if (data == null)
                SetRecord(0);
            else 
                SetRecord(data.Score);
        }
        
        private void SetRecord(float value)
        {
            _recordValueText.text = value.ToString("0.00",
                System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }
    }
}