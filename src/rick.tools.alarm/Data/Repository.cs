using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Media.AppBroadcasting;

namespace rick.tools.alarm.Data
{
    public class Repository<T> where T : class
    {
        private string _dataFile;
        private static object _lock = new object();

        public Repository() 
        {
            var path = $"{AppContext.BaseDirectory}\\Data";
            CreateDirectory(path);
            _dataFile = $"{path}\\{typeof(T).Name}.json";
            //CreateDataFile();
        }

        #region 辅助方法
        /// <summary>
        /// 创建数据文件
        /// </summary>
        private void CreateDataFile()
        {
            if (!File.Exists(_dataFile))
            {
                lock (_lock)
                {
                    if (!File.Exists(_dataFile))
                    {
                        File.Create(_dataFile);
                    }
                }
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                lock (_lock)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<T> Query()
        {
            if (!File.Exists(_dataFile))
            {
                return null;
            }
            string content = File.ReadAllText(_dataFile);
            if(string.IsNullOrEmpty(content))
            {
                return null;
            }

            return JsonSerializer.Deserialize<List<T>>(content);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public void Save(List<T> values)
        {
            var content = string.Empty;
            if(values != null)
            {
                content = JsonSerializer.Serialize(values);
            }

            File.WriteAllText(_dataFile, content);
        }
    }
}
