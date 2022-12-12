using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BxUni.ScenarioBuilder.Sample.Demo
{
    public class SelectArea : MonoBehaviour
    {
        [SerializeField] Button[] m_buttons;

        void Awake()
        {
            SetActive(false);    
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public async Task<int> SelectTask(string[] targetNames, CancellationToken ct)
        {
            if(targetNames.Length <= 0) { return -1; }

            int result = -1;

            foreach(var button in m_buttons)
            {
                button.gameObject.SetActive(false);
            }

            var pairs = m_buttons
                .Zip(targetNames, (button, label) => (button, label))
                .ToArray();

            for(int i=0; i<pairs.Length; i++)
            {
                var button = pairs[i].button;
                string label = pairs[i].label;

                var textCo = button.GetComponentInChildren<Text>();
                if(textCo != null)
                {
                    textCo.text = label;
                }
                button.gameObject.SetActive(true);

                int index = i;
                button.onClick.AddListener(() => 
                {
                    result = index;
                    Debug.Log(result);
                });
            }

            try
            {
                while(result < 0)
                {
                    ct.ThrowIfCancellationRequested();
                    await Task.Yield();
                }
            }
            finally
            {
                foreach(var (button, _) in pairs)
                {
                    button.onClick.RemoveAllListeners();
                }
            }

            return result;
        }

    }
}