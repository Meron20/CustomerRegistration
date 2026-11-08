using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomerRegistration.Application.Dtos.Requests;
using CustomerRegistration.Application.Service;
using Microsoft.UI.Xaml.Controls;
using System;

namespace CustomerRegistration.Presentation.ViewModels;

public partial class CreateCustomerViewModel(ICustomerService customerService) : ObservableObject
{
    private Guid _id;

    [ObservableProperty]
    public partial string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string EmailAddress { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string PhoneNumber { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string OrganizationNumber { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Password { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;

    [ObservableProperty]
    public partial bool IsMessageOpen { get; set; }

    [ObservableProperty]
    public partial string MessageTitle { get; set; } = string.Empty;

    [ObservableProperty]
    public partial InfoBarSeverity MessageType { get; set; } = InfoBarSeverity.Informational;

    [ObservableProperty]
    public partial string PageTitle { get; set; } = "Create customer account";


    public void ClearForm()
    {
        _id = Guid.Empty;
        Name = string.Empty;
        EmailAddress = string.Empty;
        PhoneNumber = string.Empty;
        OrganizationNumber = string.Empty;
        Password = string.Empty;
        Message = string.Empty;
        PageTitle = string.Empty;
    }

    [RelayCommand]
    private void CreateAccount()
    {

        {
            var request = new CreateCustomerRequest(
                   Name,
                   EmailAddress,
                   PhoneNumber,
                   OrganizationNumber,
                   Password);

            var result = customerService.CreateCustomer(request);

            if (result.Succeeded)
            {
                ClearForm();
                MessageType = InfoBarSeverity.Success;
                MessageTitle = "Account created";
                Message = "The customer account was created successfully.";
            }
            else
            {
                MessageType = InfoBarSeverity.Error;
                MessageTitle = "Could not create account";
                Message = result.ErrorMessage ?? "Something went wrong.";
            }
              
            IsMessageOpen = true;

        }
    }



}