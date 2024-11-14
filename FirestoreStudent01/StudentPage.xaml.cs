using FirestoreStudent01.Services;

namespace FirestoreStudent01;

public partial class StudentPage : ContentPage
{
	public StudentPage()
	{
		InitializeComponent();
		var firestoreService = new FirestoreService();
		BindingContext = new StudentViewModel(firestoreService);
	}

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}

internal class StudentViewModel
{
    private FirestoreService firestoreService;

    public StudentViewModel(FirestoreService firestoreService)
    {
        this.firestoreService = firestoreService;
    }
}