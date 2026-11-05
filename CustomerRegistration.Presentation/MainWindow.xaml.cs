using CustomerRegistration.Presentation.ViewModels;
using Microsoft.UI.Xaml;

namespace CustomerRegistration.Presentation;
public sealed partial class MainWindow : Window
{
    public CreateCustomerViewModel ViewModel { get; }

    public MainWindow(CreateCustomerViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}
