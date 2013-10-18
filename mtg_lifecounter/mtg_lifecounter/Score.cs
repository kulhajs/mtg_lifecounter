using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace mtg_lifecounter
{
    [Table]
    public class Score : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int id;

        [Column(IsPrimaryKey=true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return id; }
            set
            {
                if(id != value)
                {
                    NotifyPropertyChanging("Id");
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        private int winnerId;

        [Column]
        public int WinnerId
        {
            get { return winnerId; }
            set
            {
                if(winnerId != value)
                {
                    NotifyPropertyChanging("WinnerId");
                    winnerId = value;
                    NotifyPropertyChanged("WinnerId");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        private void NotifyPropertyChanging(string propertyName)
        {
            if(PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ScoreDataContext : DataContext
    {
        public static string DBCoonnectionString = "Data Source=isostore:/Score.sdf";

        public ScoreDataContext(string connectionString) : base (connectionString) { }

        public Table<Score> ScoreTable;
    }
}
