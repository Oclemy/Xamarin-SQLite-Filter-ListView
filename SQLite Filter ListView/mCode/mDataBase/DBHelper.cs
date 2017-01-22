using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SQLite_Filter_ListView.mCode.mDataBase
{
    class DBHelper : SQLiteOpenHelper
    {
        public DBHelper(Context context) : base(context, Constants.DB_NAME, null, Constants.DB_VERSION)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            try
            {
                db.ExecSQL(Constants.CREATE_TB);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(Constants.DROP_TB);
            OnCreate(db);
        }
    }
}