using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TMPTypewriter : MonoBehaviour
{
    public UnityEvent OnTypingComplete;

    [SerializeField] private TMP_Text textField;
    [SerializeField] private float baseDelay = 0.03f;
    [SerializeField] private float newLineDelay = 0.5f;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private bool startOnEnable = false;

    private Coroutine typingRoutine;

    void Awake()
    {
        textField.maxVisibleCharacters = 0;
    }

    void OnEnable()
    {
        if (startOnEnable)
            TypeExistingText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RevealInstant();
    }

    [ContextMenu("Start Typing ")]
    public void TypeExistingText()
    {
        StartTyping(textField.text);
    }

    public void StartTyping(string text)
    {
        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(Type(text));
    }

    private IEnumerator Type(string text)
    {
        yield return new WaitForSeconds(startDelay);

        textField.text = text;
        textField.maxVisibleCharacters = 0;

        int visibleCharactersIndex = 0;

        for (int i = 0; i < text.Length; i++)
        {
            var delay = baseDelay;

            if (text[i] == '<')
            {
                while (i < text.Length && text[i] != '>')
                    i++;

            }

            visibleCharactersIndex++;
            textField.maxVisibleCharacters = visibleCharactersIndex;

            if (text[i] == '\n')
                delay = newLineDelay;

            yield return new WaitForSeconds(delay);
        }

        typingRoutine = null;
        OnTypingComplete?.Invoke();
    }
    
    public void RevealInstant()
    {
        if (typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
            OnTypingComplete?.Invoke();
        }

        textField.maxVisibleCharacters = textField.text.Length;
        typingRoutine = null;
    }
}