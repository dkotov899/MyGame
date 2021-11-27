using System.Collections.Generic;

using MyGame.GameComponents.World.GameLevel;

namespace MyGame.GameComponents.World
{
    public class GameLevelManager
    {
        private static MainGame _gameRef;

        private static Level _currentLevel;

        // Game Levels
        private static Dictionary<int, Level> _gameLevels = 
            new Dictionary<int, Level>();

        public static MainGame GameRef
        {
            set { _gameRef = value; }
        }

        public static Level CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }

        public static Dictionary<int, Level> GameLevels
        {
            get { return _gameLevels; }
        }

        public GameLevelManager()
        {
        }

        public static void CreateLevels()
        {
            foreach (var levelData in DataLevelManager.LevelsData)
            {
                _gameLevels.Add(levelData.Key, new Level(_gameRef, levelData.Value));
            }
        }      

        public static void ResetLevel()
        {
            _currentLevel.Reset();
        }
    }
}
