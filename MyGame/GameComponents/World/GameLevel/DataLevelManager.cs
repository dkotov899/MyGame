using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyGame.GameComponents.World.GameLevel
{
    public static class DataLevelManager
    {
        // Levels Data
        private static Dictionary<int, LevelData> _levelsData =
            new Dictionary<int, LevelData>();

        public static Dictionary<int, LevelData> LevelsData
        {
            get { return _levelsData; }
        }

        static DataLevelManager()
        {
        }

        public static void ReadLevelData()
        {
            try
            {
                if (File.Exists(@"Data\LevelsData.txt") == false)
                {
                    CreateDataFile();
                }

                using (var stream = File.OpenRead(@"Data\LevelsData.txt"))
                {
                    var array = new byte[stream.Length];

                    stream.Read(array, 0, array.Length);

                    var root = JsonConvert.DeserializeObject<Root>
                        (
                            Encoding.Default.GetString(array)
                        );

                    SetLevelData(root);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void WriteLevelData()
        {
            try
            {
                using (var stream = new FileStream(@"Data\LevelsData.txt", FileMode.Create))
                {
                    var root = GetLevelData();

                    var array = Encoding.Default.GetBytes(JsonConvert.SerializeObject(root));

                    stream.Write(array, 0, array.Length);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void SetLevelData(Root root) //read data
        {
            var levels = root.PayLoad.LevelData;

            foreach (var level in levels)
            {
                _levelsData.Add(level.Key, level);
            }
        }

        private static Root GetLevelData() //write data
        {
            var levels = new List<LevelData>();

            foreach (var item in _levelsData)
            {
                levels.Add(new LevelData()
                {
                    Key = item.Key,
                    LevelName = item.Value.LevelName,
                    Status = item.Value.Status
                });
            }

            return new Root()
            {
                PayLoad = new PayLoad() 
                { 
                    LevelData = levels
                } 
            };
        }

        private static void CreateDataFile()
        {
            try
            {
                var listFiles = new List<LevelData>();

                var key = 1;

                foreach (var file in Directory.GetFiles(@"Content/World/Levels/", "*.tmx", SearchOption.TopDirectoryOnly))
                {
                    var fileName = file.Split('/');
                    var levelName = fileName[fileName.Length - 1].Split('.')[0];

                    listFiles.Add(new LevelData()
                    {
                        Key = key,
                        LevelName = levelName,
                        Status = false
                    });

                    key++;
                }

                using (var stream = new FileStream(@"Data\LevelsData.txt", FileMode.Create))
                {
                    var root = new Root()
                    {
                        PayLoad = new PayLoad()
                        {
                            LevelData = listFiles
                        }
                    };

                    var array = Encoding.Default.GetBytes(JsonConvert.SerializeObject(root));

                    stream.Write(array, 0, array.Length);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
