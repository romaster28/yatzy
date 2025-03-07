using System;
using System.Linq;
using System.Threading.Tasks;
using Dan.Main;
using Dan.Models;
using UnityEngine;

namespace Sources
{
    public class LeaderBoardsTester : MonoBehaviour
    {
        [SerializeField] private string _publicKey;
        
        private async void Start()
        {
            await Task.Delay(1000);
            
            Debug.Log("Test");
            
            LeaderboardCreator.GetLeaderboard(_publicKey, Callback, ErrorCallback);
        }

        private void ErrorCallback(string error)
        {
            Debug.LogError(error);
        }

        private void Callback(Entry[] entries)
        {
            Debug.Log(entries.Length);
            
            foreach (var entry in entries.OrderByDescending(x => x.Score))
            {
                Debug.Log($"{entry.Username}: {entry.Score}");
            }
        }
    }
}
