using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PanelManager : MonoBehaviour
{
    // 모든 판넬들을 담아둘 리스트
    [SerializeField] private List<GameObject> panels;

    void Awake()
    {
        // 시작할 때 모든 판넬을 끕니다 (필요하다면 메인만 켜둠)
        CloseAllPanels();
    }

    // 판넬 이름을 받아서 해당 판넬만 활성화
    public void OpenPanel(string panelName)
    {
        foreach (var panel in panels)
        {
            if (panel.name == panelName)
            {
                panel.SetActive(true);
            }
            else
            {
                // 다른 판넬은 닫기 (중첩해서 열고 싶다면 이 줄을 주석 처리하세요)
                panel.SetActive(false);
            }
        }
    }

    public void CloseAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }
}