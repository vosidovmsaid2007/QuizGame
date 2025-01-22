using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private float timeBetweenQuestions = 1f;

    [SerializeField]
    private GameObject TrueButton, FalseButton;


    void Start(){
        if(unansweredQuestions == null || unansweredQuestions.Count == 0){
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    void SetCurrentQuestion(){
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        
        factText.text = currentQuestion.fact;

        if(currentQuestion.isTrue){
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        }
        else{
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion(){
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue(){
        FalseButton.SetActive(false);
        trueAnswerText.gameObject.SetActive(true);

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse(){
        TrueButton.SetActive(false);
        falseAnswerText.gameObject.SetActive(true);

        StartCoroutine(TransitionToNextQuestion());
    }
}
