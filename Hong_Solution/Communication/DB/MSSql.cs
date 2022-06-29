using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hong_Solution
{
    public class MSSql
    {
        public bool bIsConnected = false;

        public SqlConnectionStringBuilder builder;
        private SqlConnection connection;

        public MSSql()
        {
            builder = new SqlConnectionStringBuilder();
            InitializeData();
            ConnectSQL();
        }
        public bool InitializeData()
        {
            builder.DataSource = "123.123.123.123:1234";   //서버 IP, 포트
            builder.UserID = "";                           //ID 변경
            builder.Password = "";                         //PW 변경
            builder.InitialCatalog = "";                   //시작 위치
            return true;
        }
        public bool ConnectSQL()
        {
            try
            {
                connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                bIsConnected = true;
                return true;

            }
            catch (Exception e)
            {
                bIsConnected = false;
                return false;
            }
        }

        public void DisconnectSQL()
        {
            try
            {
                connection.Close();
                connection.Dispose();
                connection = null;
                bIsConnected = false;
            }
            catch (Exception e)
            {
            }
        }
        public string ReadData(int nType, string[] data)
        {
            string query = "select * from dbo.fqc_visual_insp";
            string sReturnValue = "";
            try
            {


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Connection = connection;
                    command.CommandText = query;
                    StringBuilder sbValue = new StringBuilder();
                    SqlDataReader dataReader = command.ExecuteReader();
                    {
                        Console.WriteLine("Name \t | Age \t | Grade");
                        while (dataReader.Read())     // 한줄 한줄 불러오기
                        {
                            foreach (var value in dataReader) {
                                sbValue.Append (value.ToString());
                            }
                                
                        }
                    }
                    sReturnValue= sbValue.ToString();
                    connection.Close();
                }

                bIsConnected = true;
                return sReturnValue;
            }
            catch (Exception e)
            {
                bIsConnected = false;
                return "";
            }
        }
        public void InsertData(int nType, string[] data)
        {
            string sType = nType == 0 ? "Front" : "Back";
            string query= "insert into dbo.fqc_visual_insp (EQP_ID,MODULE_ID,JUDGE,NG_CODE,NG_TYPE,NG_POSITION,INSP_TIME," +
                "NG_X_POSITION,NG_Y_POSITION,SOURCE_IMAGE,NG_IMAGE,MAIN_IMAGE,NG_SIZE) values(@EQP_ID,@MODULE_ID,@JUDGE,@NG_CODE," +
                "@NG_TYPE,@NG_POSITION,@INSP_TIME,@NG_X_POSITION,@NG_Y_POSITiON,@SOURCE_IMAGE,@NG_IMAGE,@MAIN_IMAGE,@NG_SIZE)";
            try
            {
                DateTime dt = DateTime.Now;
                string sqlFormattedDate = dt.ToString("yyyy-MM-dd HH:mm:ss.fff");


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EQP_ID", "");
                    command.Parameters.AddWithValue("@MODULE_ID", "");
                    command.Parameters.AddWithValue("@JUDGE", "NG");
                    command.Parameters.AddWithValue("@NG_TYPE", "");//, struMESData.sNGType);
                    command.Parameters.AddWithValue("@INSP_TIME", sqlFormattedDate);

                    command.ExecuteNonQuery();
                }

                
                bIsConnected = true;
            }
            catch (Exception e)
            {
                bIsConnected = false;
                
            }
        }
    }
}
