using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

namespace TicTactoe
{
    /// <summary>
    /// Use recorder for save/load game data.
    /// </summary>
    public class Recorder : MonoBehaviour
    {
        public struct Move
        {
            public Player player;
            public int row;
            public int col;
        }

        public static Recorder Instance { get; private set; }
        public string testLoadDataFilename = "Sample";
        private List<Move> steps;

        public void Init()
        {
            if (Instance != null)
                return;
            Instance = this;
            steps = new List<Move>();

        }

        public void Clear()
        {
            steps.Clear();
        }

        public List<Move> Get()
        {
            return steps;
        }

        public void SetStep(int round, Player _player, int _row, int _col)
        {
            if(_player != null)
                steps.Add(new Move { player = _player, row = _row, col = _col });
        }

        public void Save()
        {
            string currTime = "SaveData_"  + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
            string filePath = Application.dataPath + @"/Resources/" + currTime + ".json";
            FileInfo file = new FileInfo(filePath);          
            string json = JsonMapper.ToJson(steps);
            StreamWriter sw = file.CreateText();
            sw.WriteLine(json); 
            sw.Close();
            sw.Dispose();
        }

        public Move[] Load(string filename)
        {
            TextAsset asset = Resources.Load(filename) as TextAsset;
            if (!asset)
            {
                return null;
            }

            string strData = asset.text;
            JsonData jsData = JsonMapper.ToObject(strData);
            Move[] loadedData = new Move[jsData.Count];
            for (int i = 0; i < jsData.Count; i++)
            {
                loadedData[i].player = new Player(jsData[i]["player"]["Name"].ToString(), 
                    (int)jsData[i]["player"]["Index"],
                    (PlayerSymbol)(int)jsData[i]["player"]["PlayerSymbol"]);
                loadedData[i].row = (int)jsData[i]["row"];
                loadedData[i].col = (int)jsData[i]["col"];
            }
            return loadedData;
        }

        public void TestLoadData()
        {
            Move[] moves = Recorder.Instance.Load(testLoadDataFilename);
            int i = 0;
            foreach (Move move in moves)
                print(string.Format("At round {0}, Player ID {1} set a chess at row:{2} col:3", i++, move.player.Index, move.row, move.col));
        }

    }
}
