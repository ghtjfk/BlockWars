using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public void OnEndingButton()
    {
        // 1. 실제 저장된 파일을 삭제 (현재 슬롯 번호 전달)
        // GameManager에 파일 삭제 함수를 만들었다면 그것을 호출합니다.
        int currentSlot = GameManager.Instance.nowSlot;
        GameManager.Instance.DeleteSaveFile(currentSlot);

        // 2. 메모리 상의 데이터 초기화 (PlayerData를 새로 생성)
        GameManager.Instance.DataClear();

        // 3. 씬 이동
        SceneManager.LoadScene("StartScene");
    }
}
