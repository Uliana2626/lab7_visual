using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using lab7_visual.Models;

namespace lab7_visual.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        List<string> items;
        List<Student> students;
        List<AverageScore> itemsAverage = new List<AverageScore>() { new AverageScore(), new AverageScore(), new AverageScore() };

        ObservableCollection<Student> studentList;
        ObservableCollection<AverageScore> itemsAverageList;

        public MainWindowViewModel()
        {
            items = new List<string>() { "Info", "Visual", "OOP" };
            students = new List<Student>() { };
            studentList = new ObservableCollection<Student>(students);
            itemsAverageList = new ObservableCollection<AverageScore>(itemsAverage);
            Add = ReactiveCommand.Create(() => add());
            RemoveSelected = ReactiveCommand.Create(() => Remove());
        }

        public void add()
        {
            students.Add(new Student(items));
            StudentList = new ObservableCollection<Student>(students);
            RefreshAverageList();
        }

        public void Remove()
        {
            List<Student> removeList = new List<Student>();
            foreach (var item in StudentList)
            {
                if (item.IsSelected)
                    removeList.Add(item);
            }
            foreach (var item in removeList)
            {
                students.Remove(item);
            }
            StudentList = new ObservableCollection<Student>(students);
            RefreshAverageList();
        }

        public void RefreshAverageList()
        {
            try
            {
                foreach (AverageScore average in itemsAverage)
                {
                    average.Score = "0";
                }

                foreach (Student student in studentList)
                {
                    for (int i = 0; i < student.ItemList.Count(); i++)
                    {
                        itemsAverageList[i].Score = Convert.ToString(Convert.ToDouble(itemsAverageList[i].Score)
                            + Convert.ToDouble(student.ItemList[i].Score) / (double)studentList.Count());
                    }
                }
            }
            catch
            {
            }
        }

        public void RefreshStudentAverage()
        {
            foreach (var student in studentList)
            {
                student.RefreshAverage();
            }
        }

        public ReactiveCommand<Unit, Unit> Add { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelected { get; }
        public ObservableCollection<Student> StudentList
        {
            get => studentList;
            set
            {
                this.RaiseAndSetIfChanged(ref studentList, value);
            }
        }

        public ObservableCollection<AverageScore> ItemsAverageList
        {
            get => itemsAverageList;
        }

        public List<Student> Students
        {
            get => students;
        }

        public void SaveFile(string path)
        {
            FIleInOut.WriteFile(path, students);
        }

        public void LoadFile(string path)
        {
            students = FIleInOut.ReadFile(path, items.Count());
            StudentList = new ObservableCollection<Student>(students);
            RefreshAverageList();
            RefreshStudentAverage();
        }
    }
}
