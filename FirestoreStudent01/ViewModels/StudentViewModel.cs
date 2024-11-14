using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FirestoreStudent01.Models;
using FirestoreStudent01.Services;

namespace FirestoreStudent01.ViewModels;

[AddINotifyPropertyChangedInterface]
public partial class SampleViewModel
{
    FirestoreService _firestoreService;

    public ObservableCollection<StudentModel> Samples { get; set; } = [];
    public StudentModel CurrentSample { get; set; }

    public ICommand Reset { get; set; }
    public ICommand AddOrUpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public StudentModel CurrentStudent { get; private set; }

    public SampleViewModel(FirestoreService firestoreService)
    {
        this._firestoreService = firestoreService;
        this.Refresh();
        Reset = new Command( async () =>
        {
            CurrentStudent = new StudentModel();
            await this.Refresh();
        }
        );
        AddOrUpdateCommand = new Command(async () =>
        {
            await this.Save();
            await this.Refresh();
        });
        DeleteCommand = new Command(async () =>
        {
            await this.Delete();
            await this.Refresh();
        });

    }

    public async Task GetAll()
    {
        Samples = [];
        var items = await _firestoreService.GetAllStudent();
        foreach (var item in items)
        {
            Samples.Add(item);
        }
    }

    public async Task Save()
    {
       if(string.IsNullOrEmpty(CurrentStudent.Id))
       {
            await _firestoreService.InsertStudent(this.CurrentStudent);
       }
       else{
            await _firestoreService.UpdateStudent(this.CurrentStudent);
       }
    }

    private async Task Refresh()
    {
        CurrentStudent = new StudentModel();
        await this.GetAll();
    }

    private async Task Delete()
    {
        await _firestoreService.DeleteStudent(this.CurrentStudent.Id);
    }

}

internal class AddINotifyPropertyChangedInterfaceAttribute : Attribute
{
}