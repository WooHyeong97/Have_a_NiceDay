using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region 필요

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Canvas hideCanvas; // 메인화면에 보여지는 ui들의 묶음

    [SerializeField]
    private Button showButton; // 숨겨져있는 투명 버튼(스샷모드용)

    [SerializeField]
    private Canvas pop_MinMap; // 미니맵 팝업창용 캔버스

    [SerializeField]
    private Canvas pop_Illustration; // 일러스트 팝업창용 캔버스

    [SerializeField]
    private Text miniMapText; // 미니맵 미리보기에 띄울 텍스

    [SerializeField]
    private Text inMapText; // 미니맵 팝업창에서 보여줄 정보 텍스트

    [SerializeField]
    private Image[] contents = new Image[4]; // Scroll Rect Panel 묶음

    [SerializeField]
    private Image[] content = new Image[4]; // Content Panel 묶음

    [SerializeField]
    private Button[] buttons; // Collection 씬의 버튼 묶음

    public GameObject[] position; // 현재 위치를 나타내는 이미지들

    #endregion 필요

    // Main Scene UI
    #region Main_Scene_UI

    public void OnScreenShotMode() // 스크린샷 모드 기능 켜기
    {
        hideCanvas.gameObject.SetActive(false); // 전체 ui를 비활성화
        showButton.gameObject.SetActive(true); // 원래 모드로 돌아가기 위해 숨겨져있는 투명 버튼을 활성화
    }

    public void OFFScreenShotMode() // 스크린샷 모드 기능 끄기
    {
        hideCanvas.gameObject.SetActive(true); // 전체 ui 활성화
        showButton.gameObject.SetActive(false); // 투명 버튼 비활성화
    }

    public void PopUpMiniMap() // 미니맵 팝업 활성화
    {
        pop_MinMap.gameObject.SetActive(true); // 비활성화되어있던 미니맵캔버스 활성화
        canvasGroup.interactable = false; // 다른 ui들과 충돌을 막기위해 기존 ui그룹의 상호작용 비활성화
    }

    public void HideMiniMap() // 미니맵 팝업 비활성화
    {
        pop_MinMap.gameObject.SetActive(false); // 미니맵 팝업 캔버스 비활성화
        canvasGroup.interactable = true; // 기존 ui그룹의 상호작용 활성화
    }

    public void PopUpIllustration() // 일러스트 팝업 활성화
    {
        pop_Illustration.gameObject.SetActive(true); // 일러스트 팝업 캔버스 활성화
        canvasGroup.interactable = false; // 다른 ui들과 충돌을 막기위해 기존 ui그룹의 상호작용 비활성화
    }

    public void HideIllustration() // 일러스트 팝업 비활성화
    {
        pop_Illustration.gameObject.SetActive(false); // 일러스트 팝업 캔버스 비활성화
        canvasGroup.interactable = true; // 기존 ui그룹의 상호작용 활성화
    }

    #endregion Main_Scene_UI
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Collection")
        {
            foreach (Button button in buttons)
            {
                button.onClick.AddListener(() => SellectScene(button)); // 현재 누른 버튼을 참조하고 그 버튼이 참조한 함수 실행
            }
        }
    }

    public void Start()
    {
        miniMapText.text = "현재 역 : " + GameManager.Instance.currentStationNumber + "\n"
            + "현재 스테이지 : " + GameManager.Instance.currentStageNumber;
    }



    public void ScoreCheck() // 스테이지 넘기기 및 점수 확인 용 임시 버튼에 들어갈 함수
    {

        if(GameManager.Instance.currentStationNumber == 10 && GameManager.Instance.currentStageNumber == 4)
        {
            GameManager.Instance.GetScore();
            miniMapText.text = "게임 클리어";
        }
        else
        {
            GameManager.Instance.GetScore();
            miniMapText.text = "현재 역 : " + GameManager.Instance.currentStationNumber + "\n"
                + "현재 스테이지 : " + GameManager.Instance.currentStageNumber;
        }
    }


    public void MiniMapPoint() // 미니맵 내부에 정보를 표시하기 위한 함수
    {
        for (int i = 0; i < 10; i++)
        {
            position[i].gameObject.SetActive(false);
            if(GameManager.Instance.currentStationNumber == i + 1)
            {
                if (GameManager.Instance.currentStationNumber == 10 && GameManager.Instance.currentStageNumber == 4)
                {
                    position[i].gameObject.SetActive(true);
                    inMapText.text = "LAST STAGE";
                }
                else
                {
                    position[i].gameObject.SetActive(true);
                    inMapText.text = "현재 " + GameManager.Instance.currentStationNumber + "번째 역\n"
                        + GameManager.Instance.currentStageNumber + "번째 스테이지입니다.";
                }
            }
        }
    }
    public void SellectScene(Button button)
    {
        for (int i = 0; i < 4; i++)
        {
            if (button.tag == contents[i].tag) // 유저가 누른 버튼과 스크롤 화면의 태그를 이용해 씽크여부 판단
            {
                contents[i].gameObject.SetActive(true); // 버튼의 태그와 스크롤 화면의 태그가 같은면 SetActive(true)
                continue;
            }
            content[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0); // 버튼의 태그와 다른 화면의 경우 스크롤을 맨위로 올림
            contents[i].gameObject.SetActive(false); // 버튼의 태그와 다른 화면인 경우 SetActive(false)
        }
    }
}
