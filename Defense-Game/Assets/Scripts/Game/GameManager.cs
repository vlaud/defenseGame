using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() { instance = this; }

    private enum AudioClipType
    {
        GoodWord = 0,
        GoodLetter = 1,
        BadLetter = 2
    }
    [SerializeField]
    private FileData _data;
    private Queue<string> _wordsQueue;
    private ITextAssetReader _textReader;
    private TypingManager typingManager;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClips;
    [SerializeField]
    private InputHandler _inputHandler;

    public Spawner spawner;
    public HealthSystem health;
    public CurrencySystem currency;

    void Start()
    {
        GetComponent<HealthSystem>().Init();
        GetComponent<CurrencySystem>().Init();

        StartCoroutine(WaveStartDelay());

        _audioSource = GetComponent<AudioSource>();
        if (_data != null)
        {
            _textReader = TextAssetReaderFactory.CreateReader(_data.ResourceType);
            _wordsQueue = _textReader.ReadFile(_data.WordsFile);
            typingManager = new TypingManager(GetNextWord());
            UIManager.instance.UpdateText(typingManager.GetCurrentWord());
            FindObjectOfType<InputHandler>().AssignOnInputListener(CheckPlayerInput);
        }
        else
        {
            throw new System.Exception("No data file assigned");
        }
    }

    IEnumerator WaveStartDelay()
    {
        //Wait for X seconds
        yield return new WaitForSeconds(2f);
        //Start the enemy spawning
        GetComponent<EnemySpawner>().StartSpawning();
    }
    public string GetNextWord()
    {
        return _wordsQueue.Dequeue();
    }

    public void CheckPlayerInput(char c)
    {
        Debug.Log(c);
        if (typingManager.CheckCharacter(c))
        {
            UIManager.instance.UpdateText(typingManager.GetCurrentWord());
            if (typingManager.CheckIfWordsFinished())
            {
                _inputHandler.ResetText();
                typingManager.SetAsNewWord(GetNextWord());
                PlayFeedbackSound(AudioClipType.GoodWord);
                UIManager.instance.UpdateText(typingManager.GetCurrentWord());
                spawner.SelectUnit();
            }
            else
            {
                PlayFeedbackSound(AudioClipType.GoodLetter);
            }
        }
        else
        {
            PlayFeedbackSound(AudioClipType.BadLetter);
        }
    }

    private void PlayFeedbackSound(AudioClipType type)
    {
        _audioSource.clip = _audioClips[(int)type];
        _audioSource.Play();
    }
}
