using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Zp
{
    public static class SettingsHelpers
    {
        public static void AddOrUpdateAppSetting<T>(T value, string sectionPathKey)
        {
            try
            {
                var settingFiles = new List<string> { "appsettings.json" };
                foreach (var item in settingFiles)
                {
                    var filePath = Path.Combine(AppContext.BaseDirectory, item);
                    string json = File.ReadAllText(filePath);
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                    SetValueRecursively(jsonObj, value);

                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(filePath, output);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing app settings | {ex.Message}", ex);
            }
        }
        private static void SetValueRecursively<T>(dynamic jsonObj, T value)
        {
            var properties = value.GetType().GetProperties();
            foreach (var property in properties)
            {
                var currentValue = property.GetValue(value);
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string) || property.PropertyType == typeof(decimal))
                {
                    if (currentValue == null) continue;
                    try
                    {
                        jsonObj[property.Name].Value = currentValue;

                    }
                    catch (RuntimeBinderException)
                    {
                        jsonObj[property.Name] = new JValue(currentValue);


                    }
                    continue;
                }
                try
                {
                    if (jsonObj[property.Name] == null)
                    {
                        jsonObj[property.Name] = new JObject();
                    }

                }
                catch (RuntimeBinderException)
                {
                    jsonObj[property.Name] = new JObject(new JProperty(property.Name));

                }
                SetValueRecursively(jsonObj[property.Name], currentValue);
            }


        }
        public static void SetAppSettingValue(string key, string sKey, string value, string appSettingsJsonFilePath = null)
        {
            if (appSettingsJsonFilePath == null)
            {
                appSettingsJsonFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "appsettings.json");
            }

            var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

            jsonObj[key][sKey] = value;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(appSettingsJsonFilePath, output);
        }
    }
}