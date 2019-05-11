using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

      [SerializeField] TextMeshProUGUI livesText;
      [SerializeField] TextMeshProUGUI scoreText;
      [SerializeField] int playerLives = 3;
      [SerializeField] int playerScore = 0;
      private void Awake()
      {
            int numGameSession = FindObjectsOfType<GameSession>().Length;
            if (numGameSession > 1)
            {
                  Destroy(gameObject);
            }
            else
            {
                  DontDestroyOnLoad(gameObject);
            }
      }
      void Start()
      {
            livesText.text = playerLives.ToString();
            scoreText.text = playerScore.ToString();
      }

      public void AddToScore(int pointsToAdd)
      {
            playerScore += pointsToAdd;
            scoreText.text = playerScore.ToString();
      }
      public void ProcessPlayerDeath()
      {
            if (playerLives > 1)
            {
                  TakeLife();
            }
            else
            {
                  ResetGameSession();
            }
      }

      private void TakeLife()
      {
            playerLives--;
            livesText.text = playerLives.ToString();
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
      }

      private void ResetGameSession()
      {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
      }

}
