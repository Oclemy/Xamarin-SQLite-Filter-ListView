using System;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite_Filter_ListView.mCode.mDataBase;
using SQLite_Filter_ListView.mCode.mDataObject;

namespace SQLite_Filter_ListView
{
    [Activity(Label = "SQLite_Filter_ListView", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListView lv;
        private SearchView sv;
        private EditText nameEditText;
        private Button saveBtn, retrieveBtn;
        JavaList<SpaceCraft> spaceCrafts=new JavaList<SpaceCraft>();
        private ArrayAdapter adapter;
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //INITIALIZE
            this.InitializeUI();

            //ADAPTER
            adapter=new ArrayAdapter(this,Android.Resource.Layout.SimpleListItem1,spaceCrafts);
            lv.Adapter = adapter;

            //EVENTS
            saveBtn.Click += saveBtn_Click;
            retrieveBtn.Click += retrieveBtn_Click;
            sv.QueryTextChange += sv_QueryTextChange;
            lv.ItemClick += lv_ItemClick;
        }

        void lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this,spaceCrafts[e.Position].ToString(),ToastLength.Short).Show();
        }

        void sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            String searchTerm = e.NewText;
            this.GetSpaceCrafts(searchTerm);
        }

        void retrieveBtn_Click(object sender, EventArgs e)
        {
            GetSpaceCrafts(null);
        }

        void saveBtn_Click(object sender, EventArgs e)
        {
            Save(nameEditText.Text);
        }

        private void InitializeUI()
        {
            lv = FindViewById<ListView>(Resource.Id.lv);
            sv = FindViewById<SearchView>(Resource.Id.sv);
            nameEditText = FindViewById<EditText>(Resource.Id.nameEditTxt);
            saveBtn = FindViewById<Button>(Resource.Id.saveBtn);
            retrieveBtn = FindViewById<Button>(Resource.Id.retrieveBtn);

          
        }

        //SAVE
        private void Save(string name)
        {
            DBAdapter db=new DBAdapter(this);
            db.OpenDB();
            bool saved = db.Add(name);
            db.CloseDB();

            if (saved)
            {
                nameEditText.Text = "";
            }
            else
            {
                Toast.MakeText(this,"Unable to save",ToastLength.Short).Show();
            }

            //RELOAD
            this.GetSpaceCrafts(null);
        }

        //RETRIEVE
        private void GetSpaceCrafts(string searchTerm)
        {
            spaceCrafts.Clear();
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            ICursor c = db.Retrieve(searchTerm);
            SpaceCraft s = null;

            if (c != null)
            {
                while (c.MoveToNext())
                {
                    int id = c.GetInt(0);
                    string name = c.GetString(1);

                    s=new SpaceCraft();
                    s.Id = id;
                    s.Name=name;

                    spaceCrafts.Add(s);
                }
            }

            db.CloseDB();

            lv.Adapter = adapter;
        }
        

    }
}

