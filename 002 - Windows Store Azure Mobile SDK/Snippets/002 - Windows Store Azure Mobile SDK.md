<a name="Top" />
# 002 - Windows STore Azure Mobile SDK #

This demo introduces the viewer to Windows Azure Mobile Services SDK for Windows Store applications.  We'll use the Mobile Service, table, and data from the previous demo (001 - WAMS Intro), and consume it from a Windows Store C#/XAML applications.   

<a name="prerequisites" />
## Prerequisites ##
Developers wishing to follow the demo steps need:

- A Windows Azure Subscription - [Sign Up for a Free Trial](http://aka.ms/az30)
- Visual Studio 2013 (Express for Windows 8 should work) - [Download](http://www.visualstudio.com/downloads/download-visual-studio-vs)
- Mobile Service, DemoTable, and Data created in the "001 - WAMS Intro" demo.

<a name="resources" />
## Resources ##

- [Bret's DevRadio Demos on GitHub](http://github.com/BretStateham/DevRadio)
- [Azure Mobile Services Documentation](http://msdn.microsoft.com/en-us/library/windowsazure/jj554228.aspx)
- [Azure Mobile Services Client Library for .NET Documentation](http://msdn.microsoft.com/en-us/library/windowsazure/jj863454.aspx)
- [Bret's Blog](http://BretStateham.com)(http://BretStateham.com)
- [Bret's YouTube Channel](http://youtube.com/BStateham)

<a name="steps" />
## Steps ##

- [Review Mobile Service from Previous Demo](ReviewService)
- [Create Windows Store Project](CreateWinStoreProject)
- [Add App UI](AddAppUI)
- [Add Mobile Services SDK](AddSDK)
- [Implement App](ImplementApp)


---
<a name="ReviewService" />
### Review Mobile Service from Previous Demo ###

- In the [**"Windows Azure Management Portal"**](https://manage.windowsazure.com)(https://manage.windowsazure.com), open the **Mobile Service** that was created in the "001 - WAMS Intro" demo

- Review the **"DemoTable"** table, it's columns (**"Id"**, **"firstName"** and **"lastName"**, ...) and the existing data in it. 

- Review the permissions on the DemoTable (they should all be set to "Anybody with the Application Key"

---
<a name="CreateWinStoreProject" />
### Create Windows Store Project ###

- Open **Visual Studio 2013**
- Create a new C# Windows Store Project. Using the Following:
	- Template: **"Installed"** | **"Templates"** | **"Visual C#"** | **"Windows Store"** | **"Blank App (XAML)"**
	- Name: **WAMSClients.WinStoreCS**
	- Location: **"Before"** Folder for this demo
	- Solution name: **"WAMSClients"**

	![New Windows Store Project](images/010-new-windows-store-project.png?raw=true "New Windows Store Project")

---
<a name="AddAppUI" />
### Add App UI ###

- In the new Windows Store application, open MainPage.xaml, and replace the contents of the page (exclucing the **&lt;Page&gt;&lt;/Page&gt;** element itself to the following:

````XML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
  <Grid.ColumnDefinitions>
    <ColumnDefinition Width="140"/>
    <ColumnDefinition Width="*"/>
  </Grid.ColumnDefinitions>
  <Grid.RowDefinitions>
    <RowDefinition Height="120"/>
    <RowDefinition Height="*"/>
  </Grid.RowDefinitions>
  <TextBlock Grid.Column="1" Text="Windows Store WAMS Client Demo" VerticalAlignment="Center" HorizontalAlignment="Left" Margin="10,0,0,0" Style="{StaticResource HeaderTextBlockStyle}"/>
  <Grid Grid.Row="1" Grid.Column="1">
    <Grid.ColumnDefinitions>
      <ColumnDefinition />
      <ColumnDefinition />
    </Grid.ColumnDefinitions>

    <Grid x:Name="FormGrid" Grid.Column="0">
      <Grid.ColumnDefinitions>

        <ColumnDefinition Width="2*"/>
        <ColumnDefinition Width="3*"/>
      </Grid.ColumnDefinitions>
      <Grid.RowDefinitions>
        <RowDefinition Height="75" />
        <RowDefinition Height="75" />
        <RowDefinition Height="75" />
        <RowDefinition Height="*" />
      </Grid.RowDefinitions>

      <TextBlock Text="First Name:" FontSize="32" Grid.Row="0" Grid.Column="0" VerticalAlignment="Center" HorizontalAlignment="Left" />
      <TextBox x:Name="FirstNameTextBox" Text="" FontSize="32" Grid.Row="0" Grid.Column="1" TextAlignment="Left" Margin="4" />

      <TextBlock Text="Last Name:" FontSize="32" Grid.Row="1" Grid.Column="0" VerticalAlignment="Center" HorizontalAlignment="Left" />
      <TextBox x:Name="LastNameTextBox" Text="" FontSize="32" Grid.Row="1" Grid.Column="1" TextAlignment="Left" Margin="4" />

      <Button x:Name="InsertButton" Grid.Row="2" Grid.Column="1" Content="Insert" FontSize="32" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Margin="4" />

      <Button x:Name="RefreshButton" Grid.Row="3" Grid.Column="0" Grid.ColumnSpan="2" Content="RefreshData" FontSize="32" HorizontalAlignment="Stretch" Height="200" />

    </Grid>

    <ListView x:Name="DemoTableListView" Grid.Column="1" Margin="4">
      <ListView.ItemTemplate>
        <DataTemplate>
          <StackPanel Margin="10">
            <TextBlock Text="{Binding FirstName}" FontSize="18" Margin="4" />
            <TextBlock Text="{Binding LastName}" FontSize="18" Margin="4" />          </StackPanel>
        </DataTemplate>
      </ListView.ItemTemplate>
    </ListView>

      <ProgressRing x:Name="LoadingDataRing" Grid.Column="1" HorizontalAlignment="Center" VerticalAlignment="Center" Width="200" Height="200" />

  </Grid>
</Grid>
````

- The resulting UI for MainPage.xaml should look like this:

	![MainPage UI](images/20-mainpage-ui.png?raw=true "MainPage UI")

---
<a name="AddSDK" />
### Add Mobile Services SDK ###

- In the **"Visual Studio"** **"Solution Explorer"** window, **right-click** the **"WAMSClients.WinStoreCS"** project and select **"Manage NuGet Packages..."** from the pop-up menu:

	![Manage NuGet Packages](images/30-manage-nuget-packages.png?raw=true "Manage NuGet Packages")

- In the **"WAMSClients.WinStoreCS - Manage NuGet Packages"** window:
	- Select **"Online"** | **"nuget.org"** on the left side
	- Type **"Windows Azure Mobile Services"** into the search box in the top right corner
	- On the **"Windows Azure Mobile Services"** item in the search results, click the **"Install"** button

	![Install WAMS SDK](images/40-install-wams-sdk.png?raw=true "Install WAMS SDK")

- On the **"License Acceptance"** window, click **"I Accept"**:

	![License Acceptance](images/50-license-acceptance.png?raw=true "License Acceptance")

- When the installation is complete, click the **"Close"** button on the "WAMSClients.WinStoreCS Manage NuGet Packages" window:

	![Close Manage NuGet Packages Window](images/60-close-manage-nuget-packages-window.png?raw=true "Close Manage NuGet Packages Window")

---
<a name="ImplementApp" />
### Implement App ###

- First, we'll add a MobileServicesClient object to the App so we can use it throughout the application.  
- In the management portal:
	- Open the "Quickstart" page for your mobile service.  
	- Next to **"CHOOSE A PLATFORM"** click on **"Windows Store"** 
	- Under **"Connect your app"**, for **"LANGUAGE"** select **"C#"**
	- Click the **"Copy Icon"** to copy the code to the clipboard. 
	- If IE prompts you to allow access to the clipboard click **"Allow access"**

	![Copy Code for Mobile Service Client](images/70-copy-code-for-mobile-service-client.png?raw=true "Copy Code for Mobile Service Client")

- Notice that the copied code already has the propert URL for the mobile service api endpoint, as well as the Application Key for the mobile service.  

- Open the App.xaml.cs code file in Visual Studio.  Add the following using directive to the top of the code file:

````C#
using Microsoft.WindowsAzure.MobileServices;
````

- Then inside the App class definition, paste the code you copied from the mobile service quickstart page (If you copy the code below, you will need to fix the URL and application key):

````C#
public static MobileServiceClient MobileService = new MobileServiceClient(
  "https://YOUR_MOBILE_SERVICE_NAME.azure-mobile.net/",
  "YOUR_MOBILE_SERVICE_APPLICATION_KEY"
);
````
 
- In the Solution Explorer, right click the WAMSClients.WinStoreCS project.  Select "Add" | "Class..." from the pop-up menu. 

- Name the new class "DemoTable.cs"

- Paste the following code in for the class:

````C#
namespace WAMSClients.WinStoreCS
{
  public class DemoTable
  {
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
  }
}
````

- Back in MaingPage.xaml, create a Click event handler for the RefreshButton.  Add the following code to MainPage.xaml.cs for the event handler:

````C#
private void RefreshButton_Click(object sender, RoutedEventArgs e)
{
  RefreshData();
}

async private void RefreshData()
{
  LoadingDataRing.IsActive = true;
  var demoTable = App.MobileService.GetTable<DemoTable>();
  var query = demoTable.CreateQuery();
  DemoTableListView.ItemsSource = await query.ToListAsync();
  LoadingDataRing.IsActive = false;
}
````
- Next, add a click event handler to the InsertButton, and add the following code for it:

````C#
private async void InsertButton_Click(object sender, RoutedEventArgs e)
{
  DemoTable item = new DemoTable
  {
    FirstName = FirstNameTextBox.Text,
    LastName = LastNameTextBox.Text
  };

  LoadingDataRing.IsActive = true;

  await App.MobileService.GetTable<DemoTable>().InsertAsync(item);

  FirstNameTextBox.Text = "";
  LastNameTextBox.Text = "";

  RefreshData();
}
````



<a name="Summary" />
## Summary ##

