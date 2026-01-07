using PensiuneApp2.Data;
using System.IO;

namespace PensiuneApp2
{
    public partial class App : Application
    {
        static PensiuneDataBase database;

        public static PensiuneDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PensiuneDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pensiune.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}