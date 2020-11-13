using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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


}
