using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SQLite_Filter_ListView.mCode.mDataBase
{
    class DBAdapter
    {
        private Context c;
        private SQLiteDatabase db;
        private DBHelper helper;

        public DBAdapter(Context c)
        {
            this.c = c;
            helper=new DBHelper(c);
        }

        //OPEN DB
        public void OpenDB()
        {
            try
            {
                db = helper.WritableDatabase;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        //close db
        public void CloseDB()
        {
            try
            {
                helper.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //INSERT DATA
        public bool Add(String name)
        {
            try
            {
                ContentValues cv=new ContentValues();
                cv.Put(Constants.NAME,name);

                db.Insert(Constants.TB_NAME, Constants.ROW_ID, cv);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        //RETRIEVE/FILTER
        public ICursor Retrieve(String searchTerm)
        {
            string[] columns = {Constants.ROW_ID, Constants.NAME};
            ICursor c = null;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                string sql = "SELECT * FROM " + Constants.TB_NAME + " WHERE " + Constants.NAME + " LIKE '%" + searchTerm + "%'";
                c=db.RawQuery(sql, null);
            }
            else
            {
                c = db.Query(Constants.TB_NAME, columns, null, null, null, null, null);
            }
            return c;
        }
    }
}