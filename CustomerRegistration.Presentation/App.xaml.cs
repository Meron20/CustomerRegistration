
using CustomerRegistration.Application.Service;
using CustomerRegistration.Domain.Customers;
using CustomerRegistration.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using CustomerRegistration.Presentation.ViewModels;

namespace CustomerRegistration.Presentation;
public partial class App : Microsoft.UI.Xaml.Application
{
    private readonly IServiceProvider _services;
    private Window? _window;
    
    public App()
    {
        InitializeComponent();

        var services = new ServiceCollection();

        services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
        services.AddSingleton<ICustomerService, CustomerService>();

        services.AddTransient<CreateCustomerViewModel>();
        services.AddTransient<MainWindow>();


        _services = services.BuildServiceProvider();

    }


    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        _window = _services.GetRequiredService<MainWindow>();

        _window.Activate();
    }
}
