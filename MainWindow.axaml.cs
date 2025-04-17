using Avalonia.Automation.Peers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaProject.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaProject;

public partial class MainWindow : Window
{
    public DbuniversityContext context;
    public List<Student> DataStudents { get; set; }
    public List<Examresult> DataExams { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        context = new DbuniversityContext();
        showdata();
    }
    
    public void showdata()
    {
        DataExams = context.Examresults
                        .Include(x => x.Student)
                        .Include(q => q.Exam.Examsname)
                        .Include(w => w.Result)
                        .ToList();
        listStudents.ItemsSource = DataExams;
    }

    


    private void btnF1(object sender, RoutedEventArgs e)
    {
        var textg = this.FindControl<TextBox>("tbgr");
        try
        {
            var DataStudents = context.Examresults.
                            Where(q => q.Student.Sname == textg.Text)
                            .Include(x => x.Exam.Examsname)
                            .Include(w => w.Result)
                            .ToList();
            listStudents.ItemsSource = DataStudents;

            var kolvostr = 0;
            var kolvoysp = 0;
            
            foreach (var u in DataStudents)
            {
                kolvostr++;
                if(u.Result.Namestatys == "Не зачет" || u.Result.Namestatys == "2")
                {
                    kolvoysp++;
                }
            }
            var t1 = kolvostr-kolvoysp;
            decimal t2 = (decimal)t1/(decimal)kolvostr*(decimal)100;
            text2.Text = kolvoysp.ToString();
            text1.Text = t2.ToString("0.00") + "%";
        }
        catch
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Hide();
        }
    }

    private void btnDataAdd(object sender, RoutedEventArgs e)
    {
        try
        {
            //создание нового объекта автопроката, т.к. он новый, то все его "столбцы" еще не заполнены
            var newExamResult = new Examresult();
            //в БД добавляем новое пустое Бронирование
            context.Examresults.Add(newExamResult);
            //создаем объект класса DataAdd, передаем ему текущее подключение к БД и незаполненное бронирование
            //простыми словами - создаем окно для его последующего открытия
            DataStudentYsp dataAddWindow = new DataStudentYsp(this.context, newExamResult);
            //открываем окно
            dataAddWindow.ShowDialog(this);
        }
        catch
        {
            MainWindow frm = new MainWindow();
            frm.Show();
            this.Hide();
        }
    }


    private void btnDel(object sender, RoutedEventArgs e)
    {
        var vardel = listStudents.SelectedItem as Examresult;

        if (vardel == null)
            return;
        try
        {
            context.Examresults.Remove(vardel);
            context.SaveChanges();
        }
        catch (System.Exception)
        {
            Console.Write("Error"); 
        }
        
        MainWindow frm = new MainWindow();
        frm.Show();
        this.Hide();
    }


    private void btnup(object sender, RoutedEventArgs e)
    {
        MainWindow frm = new MainWindow();
        frm.Show();
        this.Hide();
    }
    private void btnc1(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}