using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public Animator animator;
    public GameObject dialogue;
    public GameObject characterIcon;
    public GameObject textBox;
    private Queue<string> sentences;
    private Queue<Sprite> icons;
    Text dialText;
    public Dialog dialog;
    public GameObject miniPortal;
    public Dialog dialog2;
    public Dialog dialog3;
    public GameObject transition;

    bool stopTyping = false;
    bool busy = false;
    bool canSkip = false;
    void Start()
    {
        sentences = new Queue<string>();
        icons = new Queue<Sprite>();
        dialText = textBox.transform.Find("Text").GetComponent<Text>();
        StartCoroutine(tutorialStart());
    }

    IEnumerator tutorialStart()
    {
        print("intro anim..");
        yield return new WaitForSeconds(1.5f);
        StartDialog();
    }

    public void StartDialog()
    {
        animator.SetBool("IsOpen", true);
        print("Starting conversation");
        sentences.Clear();
        foreach(string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach(Sprite icon in dialog.images)
        {
            icons.Enqueue(icon);
        }
        DisplayNextSentence();
        canSkip = true;
        print("we've been here");
    }

    private void Update()
    {
        if (canSkip)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextSentence();
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (!busy)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
            }
            else
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Sounds/capTalk" + Random.Range(1, 4)));
                Sprite icon = icons.Dequeue();
                string sentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(typeSentence(sentence, icon));
            }
        }
        else
        {
            print("urghghh");
            stopTyping = true ;
        }
        
    }

    IEnumerator typeSentence(string text, Sprite icon)
    {
        dialText.text = "";
        characterIcon.GetComponent<Image>().sprite = icon;
        busy = true;
        foreach (char letter in text.ToCharArray())
        {
            dialText.text += letter;
            if (stopTyping) continue;
            yield return new WaitForSeconds(0.025f);
        }
        busy = false;
        stopTyping = false;
    }

    bool ballSpawned = false;
    public void EndDialogue()
    {
        if (!ballSpawned)
        {
            StartCoroutine(spawnSomeBalls());
            ballSpawned = true;
        }
        else
        {
            transition.GetComponent<TransitionScript>().transOut();
            IEnumerator waitNquit()
            {
                yield return new WaitForSeconds(1.2f);
                SceneManager.LoadScene("Game");
            }
            StartCoroutine(waitNquit());
        }
        animator.SetBool("IsOpen", false);
        canSkip = false;
    }

    void SetNews()
    {
        dialog = dialog2;
        StartDialog();
    }

    IEnumerator spawnSomeBalls()
    {
        for(int i = 0;i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            float RandDeg = Random.Range(0f, 360f);
            Vector3 pos = new Vector3(10f / 1.2f * Mathf.Cos(RandDeg), 10f / 1.2f * Mathf.Sin(RandDeg), 10);
            Vector3 offset = new Vector3(0, 0, 10);
             Instantiate(miniPortal, pos + offset, Quaternion.identity);
        }
        SetNews();

    }
}
