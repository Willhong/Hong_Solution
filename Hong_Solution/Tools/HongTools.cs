﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hong_Solution.Tools
{
    public class HongTools
    {
        #region Custom MessageBox
        public string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 260,
                Height = 170,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label()
            {
                Left = 30,
                Top = 20,
                Text = text
            };
            TextBox textBox = new TextBox()
            {
                Left = 30,
                Top = 50,
                Height = 30,
                Width = 185,
                PasswordChar = (char)42
            };
            Button confirmation = new Button()
            {
                Text = "OK",
                Left = 115,
                Width = 100,
                Top = 80,
                Height = 30,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        #endregion

        #region Json
        public void SaveJson(object classname)
        {

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string dirsave = HongDef.CONFIG_FOLDER;
            string filename = dirsave + "\\Config.json";
            
            if (Directory.Exists(dirsave))
            {
                File.WriteAllText(filename,JsonConvert.SerializeObject(classname, Formatting.Indented));
            }
            else
            {
                Directory.CreateDirectory(dirsave);
                File.WriteAllText(filename, JsonConvert.SerializeObject(classname, Formatting.Indented));
            }
        }

        public T LoadJson<T>(string filename,string section) where T: new()
        {
            return JsonConvert.DeserializeObject<T>((JObject.Parse(File.ReadAllText(filename))[section]).ToString());

            //string json = File.ReadAllText(filename);
            //JObject body = JObject.Parse(json);
            //JObject tmp = (JObject)body[section];
            //return JsonConvert.DeserializeObject<T>(tmp.ToString());


        }
        #endregion
    }
}