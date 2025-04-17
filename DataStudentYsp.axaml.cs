using Avalonia.Automation.Peers;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaProject.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvaloniaProject;

public partial class DataStudentYsp : Window
{
    public Examresult Ysp {get; set;}
    public DbuniversityContext Context {get; set;}

    public DataStudentYsp(DbuniversityContext context, Examresult ysp)
    {
        InitializeComponent();
        this.Context = context;
        this.Ysp = ysp;
        studdata();
        examdata();
        resultdata();
    }
    
    public void studdata()
    {
        var fiovar = Context.Students.ToList(); 
        var fiolist = this.FindControl<ComboBox>("fiobox");
        fiolist.ItemsSource = fiovar;
    }

    
    public void examdata()
    {
        var examvar = Context.Exams
                                .Include(x => x.Examsname)
                                .ToList(); 
        var examlist = this.FindControl<ComboBox>("exambox");
        examlist.ItemsSource = examvar;
    }

    public void resultdata()
    {
        var resultvar = Context.Statysresults.ToList(); 
        var resultlist = this.FindControl<ComboBox>("resultbox");
        resultlist.ItemsSource = resultvar;
    }



    private void btnSave_Click(object sender, RoutedEventArgs e) 
    { 
        var fiovar = Context.Students.ToList(); 
        var fiolist = this.FindControl<ComboBox>("fiobox");
        fiolist.ItemsSource = fiovar;

        var examvar = Context.Exams
                            .Include(x => x.Examsname)
                            .ToList(); 
        var examlist = this.FindControl<ComboBox>("exambox");
        examlist.ItemsSource = examvar;

        var resultvar = Context.Statysresults.ToList(); 
        var resultlist = this.FindControl<ComboBox>("resultbox");
        resultlist.ItemsSource = resultvar;
        
        var fio = fiolist.SelectedItem as Student;
        var exam = examlist.SelectedItem as Exam;
        var result = resultlist.SelectedItem as Statysresult;
            
            try
            {
                this.Ysp.Studentid = fio.Id;
                this.Ysp.Examid = exam.Id;
                this.Ysp.ResultId = result.Id;
                
                Context.SaveChanges();
            }
            catch (System.Exception)
            {
                Console.Write("Error");
            }
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e) 
    { 
        this.Close();
    }
}