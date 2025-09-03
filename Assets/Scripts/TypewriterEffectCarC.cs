using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using Object = UnityEngine.Object; 



    [RequireComponent(typeof(TextMeshProUGUI))]

    public class TypewriterEffectCarC : MonoBehaviour
    {


        private TextMeshProUGUI _textBox;
            
        private int _currentVisibleCharactersIndex;

        private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

        private WaitForSeconds _simpleDelay;
        private WaitForSeconds _interpunctuationDelay;

        [Header("Typewriter Settings")]
        [SerializeField] private float charactersPerSecond = 5.0f;
        [SerializeField] private float interpunctuationDelay = 0.5f;

    private WaitForSeconds _textBoxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;






        private void Awake()
        {
            _textBox = GetComponent<TextMeshProUGUI>();

            _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
            _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _textBoxFullEventDelay = new WaitForSeconds(sendDoneDelay);


        GridGenerator3P.OnCarCPopulate += OnCallTypewriter;
        GridGenerator.OnCarCPopulate += OnCallTypewriter;


    }

    private void OnCallTypewriter()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText);
    }

    private void OnTextComplete()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText);
    }

    private void Start()
        {
        
    }


        public void PrepareForNewText(Object obj)
        {

        _textBox.color = Color.black;

        if (!_readyForNewText)
        {
            return;
        }

        _readyForNewText = false;
        
        
        if (_typewriterCoroutine != null)

                StopCoroutine(_typewriterCoroutine);


            _textBox.maxVisibleCharacters = 0;
            _currentVisibleCharactersIndex = 0;


            _typewriterCoroutine = StartCoroutine(routine:Typewriter());

        }

    IEnumerator Typewriter()
    {

        TMP_TextInfo textInfo = _textBox.textInfo;
        Debug.Log(_textBox.textInfo.characterCount);

        while (_currentVisibleCharactersIndex < textInfo.characterCount + 1)
        {

            var lastCharacterIndex = textInfo.characterCount - 1;

            if(_currentVisibleCharactersIndex == lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters++;
                yield return _textBoxFullEventDelay;
                CompleteTextRevealed?.Invoke();
                OnTextComplete();
                _readyForNewText = true;
                yield break;
            }



            char character = textInfo.characterInfo[_currentVisibleCharactersIndex].character;


            _textBox.maxVisibleCharacters++;

            yield return _simpleDelay;

            CharacterRevealed?.Invoke(character);
            _currentVisibleCharactersIndex++;
        }

       
    }

    }

