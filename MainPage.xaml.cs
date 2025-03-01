using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.UI.Xaml;
using PolicyManager.Pages;
using PolicyManager.Utils;

namespace PolicyManager;

public class GoPageParameter
{
    public MainPageModel FrameModel { get; init; }
    public Dictionary<string, object> Parameters { get; init; }
}

public partial class MainPageModel(MainPage mainPage) : INotifyPropertyChanged
{
    private string _activePageName;
    
    public bool CanGoBack => mainPage.MainFrame.CanGoBack;

    public bool CantGoBack => !CanGoBack;

    public string ActivePageName
    {
        get => _activePageName;
        set
        {
            if (value == _activePageName) return;
            _activePageName = value;
            OnPropertyChanged(nameof(ActivePageName));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void GoBack()
    {
        if (!CanGoBack) return;

        mainPage.MainFrame.GoBack();

        OnRouteUpdated();
    }

    public void GoPage(Type pageType, Dictionary<string, object> parameters)
    {
        mainPage.MainFrame.Navigate(pageType, new GoPageParameter
        {
            FrameModel = this,
            Parameters = parameters
        });

        OnRouteUpdated();
    }

    public void GoPage(Type pageType)
    {
        GoPage(pageType, new Dictionary<string, object>());
    }

    private void OnRouteUpdated()
    {
        ActivePageName = ResourceUtil.GetString($"{mainPage.MainFrame.Content.GetType().Name}/Name");
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(CantGoBack));
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public sealed partial class MainPage
{
    private readonly MainPageModel _dataContext;

    public MainPage()
    {
        InitializeComponent();

        _dataContext = new MainPageModel(this);

        _dataContext.GoPage(typeof(WelcomePage));
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        _dataContext.GoBack();
    }
}