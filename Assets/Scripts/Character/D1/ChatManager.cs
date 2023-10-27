using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueWithButtonsAndSceneChange : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Image image;
    public Dialogue[] dialogues;
    public GameObject continueButton;
    public GameObject exitButton;
    public string nextSceneName; // 다음 씬의 이름을 여기에 입력합니다.
    public string nextSceneName2;
    private int currentDialogueIndex = 0;
    private int currentCharacterIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        continueButton.SetActive(false);
        exitButton.SetActive(false);
        // 대사 출력 시작
        StartCoroutine(TypeDialogue());
    }

    private void Update()
    {
        if (isTyping)
        {
            // 대사가 출력 중일 때 마우스 클릭 입력을 감지하지 않도록 합니다.
            return;
        }

        // 마우스 클릭을 기다립니다.
        if (Input.GetMouseButtonDown(0))
        {
            // 다음 대사로 넘어갑니다.
            ShowNextDialogue();
        }
    }

    IEnumerator TypeDialogue()
    {
        while (currentDialogueIndex < dialogues.Length)
        {

            // 이미지를 활성화 또는 비활성화합니다.
            if (dialogues[currentDialogueIndex].showImage)
            {
                image.gameObject.SetActive(true);
            }
            else
            {
                image.gameObject.SetActive(false);
            }

            nameText.text = dialogues[currentDialogueIndex].name;
            string dialogue = dialogues[currentDialogueIndex].dialogue;
            while (currentCharacterIndex < dialogue.Length)
            {
                dialogueText.text += dialogue[currentCharacterIndex];
                currentCharacterIndex++;
                yield return new WaitForSeconds(0.05f); // 타이핑 속도 조절
            }
            // 대화가 끝나면 버튼을 활성화합니다.
            if (currentDialogueIndex >= dialogues.Length-1)
            {
                continueButton.SetActive(true);
                exitButton.SetActive(true);
            }
            currentDialogueIndex++;
            currentCharacterIndex = 0;
            isTyping = true;


            // 마우스 클릭을 기다리도록 설정
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // 한 글자씩 출력하는 대사가 끝났을 때 다음 대사 출력을 위해 대사창을 초기화합니다.
            dialogueText.text = "";
            nameText.text = "";
            isTyping = false;




            yield return null; // 대사 간의 딜레이를 조절하려면 이 부분을 수정하세요.
        }
    }

    private void ShowNextDialogue()
    {
        // 대사 출력 중에 마우스 클릭 시 다음 대사로 넘어갑니다.
        if (isTyping)
        {
            currentCharacterIndex = dialogues[currentDialogueIndex].dialogue.Length;
        }
    }

    public void ContinueDialogue()
    {
        // "계속하기" 버튼 클릭 시 다음 씬으로 넘어갑니다.
        SceneManager.LoadScene(nextSceneName);
    }

    public void ExitDialogue()
    {
        // "나가기" 버튼 클릭 시 게임을 종료합니다.
        SceneManager.LoadScene(nextSceneName2);
    }
}

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string dialogue;
    public bool showImage; // 이미지 활성화 여부를 설정
}