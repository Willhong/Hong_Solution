
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hong_Solution
{
    public class HongTools
    {
        #region Custom MessageBox
        public static string ShowDialog(string text, string caption)
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

        #region openFileDialog
        public static string OpenFile(string FileType)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*."+ FileType.ToUpper() + "|*."+ FileType.ToLower();
            ofd.Title = "Open "+FileType;
            ofd.ShowDialog();
            return ofd.FileName;
        }

        #endregion

        #region Json
        public static void SaveJson(object classname)
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

        public static T LoadJson<T>(string filename,string section) where T: new()
        {
            return JsonConvert.DeserializeObject<T>((JObject.Parse(File.ReadAllText(filename))[section]).ToString());

            //string json = File.ReadAllText(filename);
            //JObject body = JObject.Parse(json);
            //JObject tmp = (JObject)body[section];
            //return JsonConvert.DeserializeObject<T>(tmp.ToString());

        }
        #endregion

        #region ImageLoad
        public static void LoadPictureboxImage(PictureBox pb,string dir)
        {
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            using (FileStream fileStream = new FileStream(dir, FileMode.Open))
            {
                pb.Image = new Bitmap(fileStream);
            }
        }
        #endregion

        #region Math

        public static T Smaller<T>(dynamic a, dynamic b)
        {
            return a > b ? b : a;
        }

        #endregion

        #region String
        public static string CompareOrder(string a, string b)
        {
            int compare = string.Compare(a, b);
            switch (compare)
            {
                case 1:
                    return a;
                case -1:
                    return b;
                default:
                    return a;
            }
        }


        #endregion

        #region Array
        public static T[,] deepcopyArray<T>(T[,] target, int size) // 배열 깊은복사
        {
            T[,] tmp = new T[size, size];
            Array.Copy(target, tmp, target.Length);
            return tmp;
        }

        #endregion

        #region List

        public static List<T> deepcopyList<T>(List<T> target)  //리스트 깊은복사
        {
            return target.ConvertAll(x => x);
        }

        public static List<T> OrderList<T>(List<T> origin)   //리스트 정렬
        {
            return origin.OrderBy(x => x).ToList();
        }

        public static List<T> DistinctList<T>(List<T> origin) //리스트 중복 제거
        {
            return origin.Select(x => x).Distinct().ToList();
        }
        public static void ListAdd<T>(List<T> origin, List<T> ListToAdd)  //리스트 뒤에 리스트 추가
        {
            origin.AddRange(ListToAdd);
        }



        #endregion

        #region Misc




        //public void EventHandler()
        //{
        //    foreach (Control a in this.Controls)
        //    {
        //        if (a.GetType().ToString().Contains("TextBox"))
        //        {
        //            a.Click += ClickOpenKeyboard;
        //            
        //        }
        //    }
        //}
        //public void ClickOpenKeyboard(object sender, EventArgs e)
        //{
        //    TextBox tb = (TextBox)sender;
        //    tb.Text = "1";
        //}
        public static string RunCMD(string cmd)
        {
            System.Diagnostics.ProcessStartInfo proInfo = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            proInfo.FileName = @"cmd";
            proInfo.CreateNoWindow = true;
            proInfo.UseShellExecute = false;
            proInfo.RedirectStandardOutput = true;
            proInfo.RedirectStandardInput = true;
            proInfo.RedirectStandardError = true;
            pro.StartInfo = proInfo;
            pro.Start();
            pro.StandardInput.Write(cmd);
            pro.StandardInput.Close();
            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();
            return resultValue;
        }
        #endregion
    }
}
