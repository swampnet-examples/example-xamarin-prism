using Prism.Commands;
using Prism.Mvvm;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrismApp.ViewModels
{
    public class SqliteTestPageViewModel : BindableBase
    {
        private readonly SQLiteAsyncConnection _db;

        public SqliteTestPageViewModel()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.sqlite");
            _db = new SQLiteAsyncConnection(path);
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if(SetProperty(ref _isRefreshing, value))
                {
                    RaisePropertyChanged("CanRefresh");
                }
            }
        }

        public bool CanRefresh => !IsRefreshing;

        private ObservableCollection<Department> _departments;

        public ObservableCollection<Department> Departments
        {
            get { return _departments; }
            set { SetProperty(ref _departments, value); }
        }

        private DelegateCommand<Department> _moreCommand;
        public DelegateCommand<Department> MoreCommand => _moreCommand ?? (_moreCommand = new DelegateCommand<Department>(More));

        private void More(Department department)
        {

        }

        private DelegateCommand<Department> _deleteCommand;
        public DelegateCommand<Department> DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand<Department>(Delete));

        private async void Delete(Department department)
        {
            await _db.DeleteAsync(department);
            Departments.Remove(department);
        }


        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new DelegateCommand(Refresh).ObservesCanExecute(() => CanRefresh));

        private async void Refresh()
        {
            try
            {
                IsRefreshing = true;

                Departments?.Clear();

                await _db.CreateTableAsync<Department>();
                if (await _db.Table<Department>().CountAsync() == 0)
                {
                    // Seed
                    for (int i = 0; i < 100; i++)
                    {
                        await _db.InsertAsync(new Department()
                        {
                            Name = $"Department {i}"
                        });
                    }
                }

                await Task.Delay(2000);

                var d = await _db.Table<Department>().ToArrayAsync();
                Departments = new ObservableCollection<Department>(d);
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }

    [Table("Department")]
    public class Department
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
