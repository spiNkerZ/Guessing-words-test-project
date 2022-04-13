using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;

[RequireComponent(typeof(WordsLoader))]
[RequireComponent(typeof(WordsSeparateOnLevels))]
[RequireComponent(typeof(SelectionWordInRunningLine))]
[RequireComponent(typeof(UniqueWordFilter))]
public class RunningLine : MonoBehaviour,IGuessedWord,IGameResult
{
    [SerializeField] TextMeshProUGUI[] UIRunLineTextArray;
    [SerializeField] ContentSizeFitter[] UITextSizeFilerArray;
    [SerializeField] HorizontalLayoutGroup horLayoutGroup;
    [SerializeField] RectTransform UIRunPivot,firstWord,lastWord;

    [SerializeField] SelectionWordInRunningLine selectionWord;
    [SerializeField] WordsLoader wordLoader;
    [SerializeField] WordsSeparateOnLevels wordsSeparate;

    [SerializeField] [Range(0, 5)] float delayStartRunAfterStart;
    [SerializeField] [Range(0, 1)] float lineRunSpeed;

    Vector3 firstWordStartPos;
    [SerializeField]  TextMeshProUGUI[] originalArrayTMP;
    [SerializeField] ContentSizeFitter[] oritinalArraySizeFilter;

    bool runLine,endWords;
    string[] wordsForRead;
    float leftBorder,rightBorder, distBetweenWords;
    int wordLastID;

    void Start()
    {
        Invoke(nameof(Initialzation),delayStartRunAfterStart);
    }

    void Initialzation()
    {
        if (selectionWord) selectionWord.Init(OnRunLine, OnEncodeWord);
        if (wordLoader) wordsForRead = wordLoader.GetTextFile();
        if (wordsSeparate)
        {
            wordsSeparate.SeparateText(wordsForRead, GameManager.singelton.gameSetting.levelCounts);
            wordsForRead = wordsSeparate.GetTextByLevel(GameManager.singelton.gameLevel);
        }

        InterfaceManager.singelton?.AddIGuessedWordInArray(this);
        InterfaceManager.singelton?.AddIGameResultInArray(this);

        originalArrayTMP = new TextMeshProUGUI[UIRunLineTextArray.Length];
        oritinalArraySizeFilter = new ContentSizeFitter[UITextSizeFilerArray.Length];
        UIRunLineTextArray.CopyTo(originalArrayTMP, 0);
        UITextSizeFilerArray.CopyTo(oritinalArraySizeFilter, 0);

        leftBorder = 0;
        distBetweenWords = horLayoutGroup.spacing;
        rightBorder = Screen.width;
        firstWordStartPos = UIRunLineTextArray[0].rectTransform.position;

        ReplaceStartWords();
        OnRunLine(true);
    }

    private void Update()
    {
        if (runLine)
        {
            RectTransform lastItem = UIRunLineTextArray.Last().rectTransform;

            UIRunPivot.Translate(-transform.right * lineRunSpeed, Space.Self);
            if (!endWords)
            {
                for (int i = 0; i < UIRunLineTextArray.Length; i++)
                {
                    RectTransform item = UIRunLineTextArray[i].rectTransform;

                    if (item.position.x + (item.sizeDelta.x / 2) < leftBorder)
                    {
                        firstWord = UIRunLineTextArray[i + 1].rectTransform;
                        item.position = new Vector3((lastItem.position.x + lastItem.sizeDelta.x / 2 + item.sizeDelta.x / 2) + distBetweenWords, item.position.y, item.position.z);

                        wordLastID++;

                        if (wordLastID == wordsForRead.Length)
                        {
                            endWords = true;
                            lastWord = UIRunLineTextArray[i].rectTransform;
                            return;
                        }

                        UIRunLineTextArray[i].text = wordsForRead[wordLastID];

                        UpdateSize();

                        CodeUtilites.OffsetIndexInArray<TextMeshProUGUI>(UIRunLineTextArray, UIRunLineTextArray[i]);
                        CodeUtilites.OffsetIndexInArray<ContentSizeFitter>(UITextSizeFilerArray, UITextSizeFilerArray[i]);

                        UpdateLayoutGroup();
                    }
                }

                if (selectionWord) selectionWord.SelectionProcess(UIRunLineTextArray, rightBorder);
            }

            if (endWords)
            {
                if (lastWord.position.x + lastWord.sizeDelta.x < leftBorder) GameManager.singelton.WIN();
            }
        }
    }

    void OnRunLine(bool _isRun)
    {
        runLine = _isRun;
    }

    void ReplaceStartWords()
    {
        if (wordsForRead.Length >= UIRunLineTextArray.Length)
        {
            for (int i = 0; i < UIRunLineTextArray.Length; i++)
            {
                UIRunLineTextArray[i].text = wordsForRead[i];
            }
        }
        else
        {
            List<TextMeshProUGUI> newTextArray = new List<TextMeshProUGUI>();
            List<ContentSizeFitter> newContentSizeArray = new List<ContentSizeFitter>();

            for (int i = 0; i < wordsForRead.Length; i++)
            {
                newTextArray.Add(UIRunLineTextArray[i]);
                newTextArray[i].text = wordsForRead[i];
                newContentSizeArray.Add(UITextSizeFilerArray[i]);

            }

            for (int j = wordsForRead.Length; j < UIRunLineTextArray.Length; j++)
            {
                UIRunLineTextArray[j].gameObject.SetActive(false);
            }


            UIRunLineTextArray = newTextArray.ToArray();
            UITextSizeFilerArray = newContentSizeArray.ToArray();
        }

        wordLastID = UIRunLineTextArray.Length - 1;
        UpdateSize();
        UpdateLayoutGroup();
    }

    void UpdateSize()
    {
        for (int i = 0; i < UIRunLineTextArray.Length; i++)
        {
            UITextSizeFilerArray[i].SetLayoutHorizontal();
        }
    }

    void UpdateLayoutGroup()
    {
        for (int i = 1; i < UIRunLineTextArray.Length; i++)
        {
            RectTransform beforeUIText = UIRunLineTextArray[i - 1].rectTransform;
            RectTransform thisRect = UIRunLineTextArray[i].rectTransform;
            Vector3 thisPos = thisRect.position;
            float offsetValue = (beforeUIText.position.x + beforeUIText.sizeDelta.x / 2) + thisRect.sizeDelta.x / 2 + distBetweenWords;
            UIRunLineTextArray[i].transform.position = new Vector3(offsetValue, thisPos.y, thisPos.z);
        }
    }

    public void OnEncodeWord()
    {
        UpdateSize();
        UpdateLayoutGroup();
    }
    void Rebuild(bool _clearOldWords)
    {
        if (wordsSeparate) wordsForRead = wordsSeparate.GetTextByLevel(GameManager.singelton.gameLevel);
        
        endWords = false;
        wordLastID = 0;
        lastWord = null;
        originalArrayTMP.CopyTo(UIRunLineTextArray, 0);
        oritinalArraySizeFilter.CopyTo(UITextSizeFilerArray,0);
        UIRunLineTextArray.First().rectTransform.position = firstWordStartPos;
        firstWord = null;

        selectionWord.Restart(_clearOldWords);
        ReplaceStartWords();
        OnRunLine(true);
    }

    public void WordIsGuessed(string _originalWord)
    {
        OnRunLine(true);
        UpdateSize();
        UpdateLayoutGroup();
    }

    public void IGameWin(int _pointCount) => OnRunLine(false);
    public void IGameOver(int _pointCount) => OnRunLine(false);
    public void IGameNextLevel() => Rebuild(false);
    public void IGameRestart() => Rebuild(true);
}
