using System;
using System.Collections.Generic;
using System.Linq;
using Dan.Main;
using Dan.Models;
using Sources.Config;
using Sources.Misc;

namespace Sources.Model.Leaders.Decorators
{
    public class LeaderBoardsServerDecorator : ILeaderBoards
    {
        private readonly ILeaderBoards _leaderBoards;

        private readonly LeaderboardServerConfig _config;

        private readonly AsyncProcessor _asyncProcessor;

        public LeaderBoardsServerDecorator(ILeaderBoards leaderBoards, LeaderboardServerConfig config, AsyncProcessor asyncProcessor)
        {
            _leaderBoards = leaderBoards ?? throw new ArgumentNullException(nameof(leaderBoards));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _asyncProcessor = asyncProcessor ? asyncProcessor : throw new ArgumentNullException(nameof(asyncProcessor));
        }

        public void Write(LeaderInfo info)
        {
            _leaderBoards.Write(info);
        }

        public void Read(Action<IEnumerable<LeaderInfo>> received)
        {
            _leaderBoards.Read(delegate(IEnumerable<LeaderInfo> infos)
            {
                List<LeaderInfo> list = infos.ToList();
                
                LeaderboardCreator.GetLeaderboard(_config.PublicKey, delegate(Entry[] entries)
                {
                    foreach (Entry entry in entries) 
                        list.Add(new LeaderInfo(entry.Username, entry.Score));

                    received?.Invoke(list.OrderByDescending(x => x.Score));
                }, delegate(string s)
                {
                    received?.Invoke(list);
                });
            });
        }
    }
}