﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
      [SerializeField] float levelLoadWaitDelay = 1f;
      [SerializeField] float levelExitSlowMoFactor = 0.2f;
      private void OnTriggerEnter2D(Collider2D other)
      {
            StartCoroutine(LoadNextLevel());
      }

      IEnumerator LoadNextLevel()
      {
            Time.timeScale = levelExitSlowMoFactor;
            yield return new WaitForSeconds(levelLoadWaitDelay);
            Time.timeScale = 1f;
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);

      }
}
