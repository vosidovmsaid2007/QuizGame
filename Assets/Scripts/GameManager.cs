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
    private float timeBetweenQuestions = 1f;


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
    }

    IEnumerator TransitionToNextQuestion(){
        unansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectTrue(){
        if(currentQuestion.isTrue){
            Debug.Log("Correct");
        }
        else{
            Debug.Log("Wrong");
        }
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse(){
        if(!currentQuestion.isTrue){
            Debug.Log("Correct");
        }
        else{
            Debug.Log("Wrong");
        }

        StartCoroutine(TransitionToNextQuestion());
    }
}
