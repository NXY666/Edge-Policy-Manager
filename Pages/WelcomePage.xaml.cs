using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Win32;
using PolicyManager.Utils;

namespace PolicyManager.Pages;

public sealed partial class WelcomePage
{
    private MainPageModel _frameModel;

    public WelcomePage()
    {
        InitializeComponent();

        Name = ResourceUtil.GetString($"{GetType().Name}/Name");
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        if (e.Parameter is not GoPageParameter goPageParameter) return;

        _frameModel = goPageParameter.FrameModel;
    }

    private void EdgeButton_Click(object sender, RoutedEventArgs e)
    {
        _frameModel.GoPage(typeof(PolicyPage), new Dictionary<string, object>
        {
            { "policyType", "Edge" },
            { "policyRegistryPath", @"SOFTWARE\Policies\Microsoft\Edge" }
        });
    }

    private void EdgeUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        _frameModel.GoPage(typeof(PolicyPage), new Dictionary<string, object>
        {
            { "policyType", "EdgeUpdate" },
            { "policyRegistryPath", @"SOFTWARE\Policies\Microsoft\EdgeUpdate" }
        });
    }

    private void EdgeWebviewButton_Click(object sender, RoutedEventArgs e)
    {
        _frameModel.GoPage(typeof(PolicyPage), new Dictionary<string, object>
        {
            { "policyType", "EdgeWebview" },
            { "policyRegistryPath", @"SOFTWARE\Policies\Microsoft\Edge\WebView2" }
        });
    }

    private async void GithubButton_OnClick(object sender, RoutedEventArgs e)
    {
        // 打开 Github 仓库
        await Launcher.LaunchUriAsync(new Uri("https://github.com/NXY666/EdgePolicyManager"));
    }

    private async void DownloadButton_OnClick(object sender, RoutedEventArgs e)
    {
        // 打开 Github Release
        await Launcher.LaunchUriAsync(new Uri("https://github.com/NXY666/EdgePolicyManager/releases/latest"));
    }

    private async void FeedbackButton_OnClick(object sender, RoutedEventArgs e)
    {
        // 打开 Github Issues
        await Launcher.LaunchUriAsync(new Uri("https://github.com/NXY666/EdgePolicyManager/issues/new/choose"));
    }

    private static int CompareVersion(string v1, string v2)
    {
        var v1S = v1.Split('.');
        var v2S = v2.Split('.');
        var len = Math.Max(v1S.Length, v2S.Length);
        for (var i = 0; i < len; i++)
        {
            var v1I = i < v1S.Length ? int.Parse(v1S[i]) : 0;
            var v2I = i < v2S.Length ? int.Parse(v2S[i]) : 0;
            if (v1I > v2I)
            {
                return 1;
            }

            if (v1I < v2I)
            {
                return -1;
            }
        }

        return 0;
    }

    private async void VersionButton_OnClick(object sender, RoutedEventArgs e)
    {
        var senderButton = (Button)sender;
        senderButton.IsEnabled = false;

        var grid = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
        };

        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());
        grid.RowDefinitions.Add(new RowDefinition());

        grid.RowSpacing = 8;

        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());

        var titleColor = new SolidColorBrush(Colors.Black);
        var valueColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x64, 0x64));

        var title1 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/PolicyManagerTitle"), FontWeight = FontWeights.Bold };
        title1.SetValue(Grid.RowProperty, 0);
        title1.SetValue(Grid.ColumnProperty, 0);
        title1.SetValue(Grid.ColumnSpanProperty, 2);
        grid.Children.Add(title1);

        var key1 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/CurrentVersionKey"), Foreground = titleColor };
        key1.SetValue(Grid.RowProperty, 1);
        key1.SetValue(Grid.ColumnProperty, 0);
        grid.Children.Add(key1);

        var key2 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/LatestVersionKey"), Foreground = titleColor };
        key2.SetValue(Grid.RowProperty, 2);
        key2.SetValue(Grid.ColumnProperty, 0);
        grid.Children.Add(key2);

        var title2 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/PolicyCompatibilityTitle"), FontWeight = FontWeights.Bold };
        title2.SetValue(Grid.RowProperty, 3);
        title2.SetValue(Grid.ColumnProperty, 0);
        title2.SetValue(Grid.ColumnSpanProperty, 2);
        grid.Children.Add(title2);

        var key3 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/EdgeVersionKey"), Foreground = titleColor };
        key3.SetValue(Grid.RowProperty, 4);
        key3.SetValue(Grid.ColumnProperty, 0);
        grid.Children.Add(key3);

        var key4 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/CurrentCompatibleVersionKey"), Foreground = titleColor };
        key4.SetValue(Grid.RowProperty, 5);
        key4.SetValue(Grid.ColumnProperty, 0);
        grid.Children.Add(key4);

        var key5 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/LatestCompatibleVersionKey"), Foreground = titleColor };
        key5.SetValue(Grid.RowProperty, 6);
        key5.SetValue(Grid.ColumnProperty, 0);
        grid.Children.Add(key5);

        var value1 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/GettingValueText"), Foreground = valueColor };
        value1.SetValue(Grid.RowProperty, 1);
        value1.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(value1);

        var value2 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/GettingValueText"), Foreground = valueColor };
        value2.SetValue(Grid.RowProperty, 2);
        value2.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(value2);

        var value3 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/GettingValueText"), Foreground = valueColor };
        value3.SetValue(Grid.RowProperty, 4);
        value3.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(value3);

        var value4 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/GettingValueText"), Foreground = valueColor };
        value4.SetValue(Grid.RowProperty, 5);
        value4.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(value4);

        var value5 = new TextBlock { Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/GettingValueText"), Foreground = valueColor };
        value5.SetValue(Grid.RowProperty, 6);
        value5.SetValue(Grid.ColumnProperty, 1);
        grid.Children.Add(value5);

        var dialog = new ContentDialog
        {
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            XamlRoot = XamlRoot,
            Title = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/Title"),
            CloseButtonText = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/CloseButtonText"),
            Content = grid
        };

        var dialogTask = dialog.ShowAsync();

        // 当前工具版本
        try
        {
            value1.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        }
        catch (Exception)
        {
            value1.Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/UnknownText");
        }

        // 最新工具版本
        var task2 = Task.Run(() =>
        {
            using var client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Hello/1.0");

                var response = client.GetAsync("https://api.github.com/repos/NXY666/EdgePolicyManager/releases/latest").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                // 解析 JSON
                var jsonContent = response.Content.ReadAsStringAsync().Result;
                var jsonElement = JsonDocument.Parse(jsonContent).RootElement;
                return jsonElement.GetProperty("tag_name").GetString()?[1..];
            }
            catch (Exception)
            {
                return ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/UnknownText");
            }
        });

        // 获取 Edge 版本
        try
        {
            value3.Text = (string)RegistryUtil.GetRegistryValue(Registry.CurrentUser, @"Software\Microsoft\Edge\BLBeacon", "version").Value;
        }
        catch (Exception)
        {
            value3.Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/UnknownText");
        }

        // 获取当前兼容版本
        try
        {
            value4.Text = ResourceUtil.GetEmbeddedPlainText("StaticModels.Policy.SUPPORT_VERSION");
        }
        catch (Exception)
        {
            value4.Text = ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/UnknownText");
        }

        // 获取最新版本
        var task5 = Task.Run(() =>
        {
            using var client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Hello/1.0");

                var response = client.GetAsync("https://raw.githubusercontents.com/NXY666/EdgePolicyManager/master/StaticModels/Policy/SUPPORT_VERSION").Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception)
            {
                return ResourceUtil.GetString($"WelcomePage/VersionButton_OnClick/VersionInfoDialog/UnknownText");
            }
        });

        senderButton.IsEnabled = true;

        value2.Text = await task2;
        value5.Text = await task5;
        await dialogTask;
    }
}