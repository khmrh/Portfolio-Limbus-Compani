using UnityEngine;
using UnityEngine.UI;

public class PanelOpenButton : MonoBehaviour
{
    public string targetPanelName; // 열고자 하는 판넬 오브젝트의 이름
    private PanelManager panelManager;

    void Start()
    {
        panelManager = FindObjectOfType<PanelManager>();

        // 버튼 컴포넌트를 가져와 클릭 이벤트 자동 연결
        GetComponent<Button>().onClick.AddListener(() => {
            panelManager.OpenPanel(targetPanelName);
        });
    }
}