using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TeamProject
{
    /// <summary>
    /// 1)убрать хардкод 2 переменных здесь, 
    /// 2)сделать "справку" в xaml,
    /// 3)поправить xaml чтоб было по симпатичнее
    /// </summary>
    public class AlcoholManager
    {
        public List<Alcohol> alcohols;
        public User user;
        public List<Conditions> conditions;
        public List<UserConditions> userConditions; //лист для DataGrid
        private const double _B60 = 0.15; //это не изменяющийся параметр, показывающий кол-во промилле выводящееся за час из организма
        private double _density = 0.7893; //плотность этанола для перевода массы алкоголя в граммы

        public AlcoholManager()
        {
            LoadData();
        }

        public void Volumes(Alcohol alcohol, User user)
        {

            foreach (var item in conditions)
            {
                var c = item.Concentration;
                var k = alcohol.AlcoholicBeverage;
                var A = (c * user.Weight * user.WidmarK * 100) / (k * _density * 0.7);
                var hours = c / _B60;
                var step1 = userConditions.Find(i => i.Name == item.Name);
                step1.Volume = A;
                step1.Hours = hours;
                
            }

        }

        private const string AlcoholFileName = "../../../../TeamProject/Data/Alcohol.json";
        private const string ConditionsFileName = "../../../../TeamProject/Data/Conditions.json";
        private const string UserCondsFileName = "../../../../TeamProject/Data/UserConds.json";

        private T Deserialize<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jsonReader);
                }
            }
        }

        public void Serialize<T>(string fileName, T data)
        {
            using (var sw = new StreamWriter(fileName))
            {
                using (var jsonWriter = new JsonTextWriter(sw))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }

        private void LoadData()
        {
            alcohols = Deserialize<List<Alcohol>>(AlcoholFileName);
            conditions = Deserialize<List<Conditions>>(ConditionsFileName);
            userConditions = Deserialize<List<UserConditions>>(UserCondsFileName);
        }
        
    }
            
}
