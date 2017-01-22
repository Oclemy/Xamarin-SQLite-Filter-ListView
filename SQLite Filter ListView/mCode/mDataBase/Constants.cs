using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SQLite_Filter_ListView.mCode.mDataBase
{
    class Constants
    {
        //COLUMNS
        public static string ROW_ID = "id";
        public static string NAME = "name";

        //DB PROPS
        public static string DB_NAME = "c_DB";
        public static string TB_NAME = "c_TB";
        public static int DB_VERSION = 1;

        //create tb stmt
        public static String CREATE_TB = "CREATE TABLE c_TB(id INTEGER PRIMARY KEY AUTOINCREMENT,"
            + "name TEXT NOT NULL);";
       
        //drop tb stmt
        public static String DROP_TB = "DROP TABLE IF EXISTS " + TB_NAME;



    }
}