using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTactoe
{
    /// <summary>
    /// Initialize every singleton such that they can run in correct way.
    /// </summary>
    public class Initializer : MonoBehaviour
    {
        public Board board;
        public UI_Manager uiManager;
        public GameManager gameManager;
        public Recorder recorder;
        public bool destoryAfterInitializeComplete = true;

        private void Start()
        {
            gameManager.Init();
            uiManager.Init();
            board.Init();
            recorder.Init();           
            if (destoryAfterInitializeComplete)
                Destroy(transform.gameObject);
        }
    }
}
