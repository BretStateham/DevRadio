<a name="Top" />
# 001 - WAMS Introduction #

This demo introduces the viewer to Windows Azure Mobile Services.  We focus on the core of Windows Azure Services, independent of the client side SDK used.  To do that, we first create an Azure Mobile Service using the portal, then interact with it using the REST API from Fiddler.  

<a name="prerequisites" />
## Prerequisites ##
Developers wishing to follow the demo steps need:

- A Windows Azure Subscription - [Sign Up for a Free Trial](http://aka.ms/az30)
- Visual Studio 2013 (Express for Windows 8 should work) - [Download](http://www.visualstudio.com/downloads/download-visual-studio-vs)
- Fiddler 4 - [Download from fiddler2.com](http://fiddler2.com)
- Optional: SQL Server Management Studio Express.  Download [ENU\x64\SQLManagementStudio_x64_ENU.exe](http://aka.ms/sqlexp12)

<a name="resources" />
## Resources ##

- [Bret's DevRadio Demos on GitHub](http://github.com/BretStateham/DevRadio)
- [Azure Mobile Services Documentation](http://msdn.microsoft.com/en-us/library/windowsazure/jj554228.aspx)
- [Windows Azure Mobile Services REST API Reference](http://msdn.microsoft.com/en-us/library/windowsazure/jj710108.aspx)
- [Operations on a table in Mobile Services](http://msdn.microsoft.com/en-us/library/windowsazure/jj710104.aspx)
- [Bret's Blog](http://BretStateham.com)(http://BretStateham.com)
- [Bret's YouTube Channel](http://youtube.com/BStateham)
- A file with the Fiddler Scratchpad contents (**"Fiddler Scratchpad.txt"**) can be found in the same directory as this file.  Make sure to replace the **"YOUR_MOBILE_SERVICE_NAME"**, **"BRETS_RECORD_ID_GUID"**, and **"PASTE_YOUR_KEY_HERE"** placeholders with appropriate values.  


<a name="steps" />
## Steps ##

- [Create a Windows Azure Mobile Service](CreateWAMS)
- [Review Mobile Service in the Portal](ReviewInPortal)
- [Review the Azure SQL Database](Review the Database)
- [Create a table with open permissions](CreateTable)
- [Use fiddler to read data using the REST API](RESTGets)
- [Insert (POST) Data with REST](RESTPosts)
- [Update (PATCH) Data with REST](RESTPatches)
- [Delete (DELETE) Data with REST](RESTDeletes)
- [Use the REST API with the Application Key](UseRestWithKey)

---
<a name="CreateWAMS" />
### Create a Windows Azure Mobile Service ###

- Open the [Windows Azure Management Portal](https://manage.windowsazure.com) (https://manage.windowsazure.com)
- Create a new Windows Azure Mobile Service

	![New Windows Azure Mobile Service](images/010-new-windows-azure-mobile-service.png?raw=true "New Windows Azure Mobile Service")

- Give the Azure Mobile Service:
  - Use a Unique URL
  - Either use an Existing SQL Database, or choose to create a new one
  - Pick an appropriate region

	![Create a Mobile Service](images/020-create-a-mobile-service.png?raw=true "Create a Mobile Service")

- Create a new database on the selected server.  If you chose to create a new server provide appropriate administrative credentials and a region for the new Server.  Click the "Check Mark" button in the lower right corner to create the service.  

	![Specify Database Settings](images/030-specify-database-settings.png?raw=true "Sepcify Database Settings")

- Wait for the new mobile service to be created.  It shouldn't take more than one or two minutes.  

	![Mobile Service Ready](images/040-mobile-service-ready.png?raw=true "Mobile Service Ready")

---
<a name="ReviewInPortal" />
### Review Mobile Service in the Portal ###

- Click on the new mobile service to open it's dashboard. The portal starts you out on the **"Quickstart"** page (the cloud/lighting bolt icon ![Quickstart Page Icon](images/quickstart-page-icon.png?raw=true "Quickstart Page Icon")) for the mobile service.  The quickstart page has links to great tutorials for integrating your new Windows Azure Mobile Service with :
	- Windows Store Apps
	- Windows Phone Apps
	- iOS Native Apps
	- Android Native Apps
	- HTML/Javascript Apps
	- iOS, Xamarin and other apps with Xamarin!

	![Quickstart Page](images/50-quickstart-page.png?raw=true "Quickstart Page")

- Switch to the **"DASHBOARD"** page.  The dashboard provides tools for monitoring and configuring your Mobile Service. From here you can:
	- Monitor the mobile serivce status and usage
	- Configure source control with git
	- Configure autoscale (Basic usage tier only)
	- more...

	![Dashboard Page](images/60-dashboard-page.png?raw=true "Dashboard Page")

- We'll see other pages in the steps that follow.

---
<a name="Review the Database" />
### Review the Azure SQL Database ###

- Switch to the **"DATA"** page for the mobile service, and notice that by default there are no tables:

	![Data Page - No Tables](images/70-data-page---no-tables.png?raw=true "Data Page - No Tables")

- The database that was created along with the Mobile Service is just an Azure SQL Database.  That's cool because it means that you can connect to it, and use it just like a regular SQL Datbase.  To see this, in the Portal, change to the "SQL Databases" page. You should see the database for your mobile service listed on the page.  CLick on the name of the database to open it in the portal:

	![Mobile Service SQL Database](images/80-mobile-service-sql-database.png?raw=true "Mobile Service SQL Database")

- Scroll to the bottom of the quickstart page (again, the quickstart page is the page you see by default, or when you click on the cloud/lightning bolt icon - ![quickstart-page-icon](images/quickstart-page-icon.png?raw=true "Quickstart Icon")).  At the bottom of the page, you can see the fully qualified host name for the sql server. It will be in the form of  **_&lt;servername&gt;_.database.windows.net**. Copy the fully qualified name (without the ",1433" port info) to the clipboard:

	![SQL Server Fully Qualified Host Name](images/90-sql-server-fully-qualified-host-name.png?raw=true "SQL Server Fully Qualified Host Name")

- Open Visual Studio 2013.  From the menu bar, select **"VIEW"** | **"SQL Server Object Explorer"** (note, you can also do the same thing with SQL Server Management Studio).

	![VS SQL Object Explorer Menu Item](images/100-vs-sql-object-explorer-menu-item.png?raw=true "VS SQL Object Explorer Menu Item")

- In the **"SQL Server Object Explorer"** window, **right-click** on **"SQL Server"** and select **"Ad SQL Server..."** from the pop-up menu:

	![Add SQL Server](images/110-add-sql-server.png?raw=true "Add SQL Server")

- In the **"Connect to Server"** window:
	- Paste the server name that you copied to the clipboard above into the **"Server name:"** box.  
	- Set the the **"Authentication:""* method to **"SQL Server Authentication"**
	- Enter the **"Login:"** and **"Password:"** values that you supplied for the SQL Server when you created the mobile service above.  
	- Click **"Connect"**

	![Connect to Server](images/120-connect-to-server.png?raw=true "Connect to Server")

- **You should receive an error!  Yay! This is a good thing.**  That means that even though your new database got created, it isn't open for the whole world to hack into and mess with.  Azure SQL Server's have a firewall that prevents external connections by default!  Click **"OK"** to clear the error dialog, but leave the connection dialog open.  We'll retry it in a few steps.  

	![SQL Server Connection Error](images/130-sql-server-connection-error.png?raw=true "SQL Server Connection Error")

- Return to the web browser with Windows Azure Management Portal open to the Azure SQL Database's quickstart page.  Go to the **"DASHBOARD"** page for the database, and scroll down and click the **"Manage allowed IP addresses"** link:

	![Azure Database Allowed IP Addr Link](images/140-azure-database-allowed-ip-addr-link.png?raw=true "Azure Database Allowed IP Addr Link")

- You will actually be taken to the **Azure SQL Server**'s **"CONFIGURE"** page in the portal.  From here, you can modify the Azure SQL Server instances firewall rules to allow connections from your IP Address.

- You can create any number of firewall rules here, but we will focus on just allowing your development workstation to connect.  The portal makes it easy to do by showing you the IP address you are connecting FROM, and provides you an easy way to create a firewall rule for your client IP address.  Click the "right arrow" (![Right Arrow Icon](images/right-arrow-icon.png?raw=true "Right Arrow Icon")) button next to "ADD TO THE ALLOWED IP ADDRESSES." text next to your client IP Address: 

	![Add Client IP Firewall Rule](images/150-add-client-ip-firewall-rule.png?raw=true "Add Client IP Firewall Rule")

- You should see a new entry for your client IP Address.  Click the "SAVE" button along the bottom to save the new rule:

	![Save New Firewall Rule](images/160-save-new-firewall-rule.png?raw=true "Save New Firewall Rule")

	> **Note:** We are doing this only as a demo of how to connect to the Azure SQL Database from a development workstation.  You DO NOT need to open any firewall rules on the database for normal Azure Mobile Services functionality.  Azure Mobile Services provides a REST api for client applications to access and manipulate the data in the tables using good old HTTP. 

- Return to Visual Studio's SQL Server Object Explorer (or SSMS if you are using that instead) and retry the connection to the SQL Sever:

	![Retry SQL Server Connection](images/170-retry-sql-server-connection.png?raw=true "Retry SQL Server Connection")

- The SQL Server Object Explorer should now successfully connect to the server (if not, confirm that you saved the firewall rule, that you have the correct server name, and that you are using the correct credentials).

- In the **SQL Server Object Explorer** window, expand  **"_&lt;servername&gt;_.database.windows.net"** | **"Databases"** | **"_&lt;your_db_name&gt;_"** | **"Tables"** and verify that there are no user tables in the database:

	![Database with No Tables](images/180-database-with-no-tables.png?raw=true "Database with No Tables")

- Ok, so what did that prove?  We'll it proved that:
	- With the appropriate firewall rules, we can connect to the Azure SQL Database provided with our Azure Mobile Service
	- The database is just a regular Azure SQL Database.  That means that we can connect to it like most any other SQL Database for querying, reporting, updating, etc. 
	- By default, there are no tables in the new database

---
<a name="CreateTable" />
### Create a table with open permissions ###

- Let's create a table for our Azure Mobile Service.  Return to the **Windows Azure Management Portal** in the browser, and open the **"DATA"** page for the Azure Mobile Serivce we created earlier.  Click the **"CREATE"** button along the bottom to create a new table:

	![Create a New Table](images/190-create-a-new-table.png?raw=true "Create a New Table")

- In the **"Create New Table"** window, create a new table named **"DemoTable"** and give **Insert, Update, Delete and Read Permissions** to **"Everyone"**.  Click the "Check Mark" icon in the lower right corner to create the table:

	![Create DemoTable with Open Permissions](images/200-create-demotable-with-open-permissions.png?raw=true "Create DemoTable with Open Permissions")

- You should see the new table listed on the **"DATA"** page in the portal.  Click on the name of the table to open it in the portal:

	![DemoTable on DATA Page](images/210-demotable-on-data-page.png?raw=true "DemoTable on DATA Page")

- Look at the various pages for the table.  Note the list of columns on the **"COLUMNS"** page:

	![DemoTable Default Columns](images/220-demotable-default-columns.png?raw=true "DemoTable Default Columns")

- Return to the **Visual Studio** (or SSMS) **SQL Server Object Explorer**.  **Right-Click" the **"Tables"** node and select **"Refresh"** from the pop-up menu:

	![Refresh Tables](images/230-refresh-tables.png?raw=true "Refresh Tables")

- Notice that the new table is in a schema with the same name as your Windows Azure Mobile Service.  Expand the table and it's columns to see the same columns that were shown in the portal:

	![Default Columns in Object Explorer](images/240-default-columns-in-object-explorer.png?raw=true "Default Columns in Object Explorer")

- Lastly, return to the **Windows Azure Management Portal** in the browser, and open your Azure Mobile Service's **"CONFIGURE"** page.  Notice that by default, the **"ENABLE DYNAMIC SCHEMA"** option is **"ON"**:

	![Dynamic Schema on by Default](images/250-dynamic-schema-on-by-default.png?raw=true "Dynamic Schema on by Default")

	> **Note:** Dynamic Schema makes it possible for new columns to be created on the fly.  If a client application submits a JSON object to the REST API via a POST or a PATCH update, the any members on the JSON object that don't have a corresponding column will cause the mobile service to create a new column in the Azure SQL Database, with a data type compatible with the JSON value.  This only works for new columns though.  It won't delete columns, or alter their datatype on the fly.  Once the column exists, you must drop or alter it manually using SQL statements.  Pretty cool though.  This gives the SQL Databse a kind of "No-SQL" feel in terms of just throwing objects at the database and having it store them.  

---
<a name="RESTGets" />
### Use fiddler to read data using the REST API ###

Ok, so we have this mobile service and a table.  No what?  Well, even though we opened a firewall rule for ***US*** to connect directly to the SQL database from our development workstation, we wouldn't want to do that for general use.  It's just not safe, plus connecting directly to SQL from mobile clients is nearly impossible because there are native client database apis and drivers for those platforms.  Rather, we'll let our client applications work with data in the tables using a REST api that is provided by the Azure Mobile Service.  

Each table created for your Azure Mobile Service will be exposed as a REST endpoint with the following URL:

**http://_&lt;your_mobile_service_name&gt;_.azure-mobile.net/tables/_&lt;table_Name&gt;_**

You can then use regular **HTTP** verbs like **GET**, **POST**, **PATCH**, and **DELETE** to perform Read, Insert, Update and Delete (correspondingly) operations against the table! Awesome! 

Read the documentation to learn more about the [Windows Azure Mobile Services REST API](http://msdn.microsoft.com/en-us/library/windowsazure/jj710108.aspx) including how to perform [Operations on a Table](http://msdn.microsoft.com/en-us/library/windowsazure/jj710104.aspx), 

You could test the REST API using any HTTP client, but you can only create GET request easily in the browser.  Do create POST, PATCH, DELETE request and to edit the HTTP headers, we need something a little more low level. A great to tool to do this with on Windows is Fiddler.  You can download fiddler from [fiddler2.com](http://fiddler2.com)

- Open Fiddler.  To make it easy to watch JUST the Azure Mobile Services traffic, we'll create a filter in Fiddler to capture only requests to **"*.azure-mobile.net"** sites.  In Fiddler:
	- Switch to the **"Filters"** tab.  
	- Turn on the **"Use Filters"** checkbox.
	- Select **"Show only the following hosts"** from the hosts drop down
	- Type **"*.azure-mobile.net"** into the "Hosts" text box
	- Click the **"Actions"** button.
	- Select **"Run Filterset now** to enable the filters

	![Fiddler Filters](images/260-fiddler-filters.png?raw=true "Fiddler Filters")

- Next, to make it easier to view the result of each request, switch to the **"Composer"** tab, and within there, select the **"Options"** tab.  Under **"Request Options"** turn on the **"Inspect Session"** checkbox, then click the "Tear off" button to open the composer in it's own window:

	![Composer Options](images/270-composer-options.png?raw=true "Composer Options")

- Arrange the **"Composer"** and **"Fiddler Web Debugger"** windows so that you can see them both at the same time.  

	![Arrange Fiddler Windows](images/280-arrange-fiddler-windows.png?raw=true "Arrange Fiddler Windows")

- Try a DemoTable GET request using the REST API. 
	- In the Fiddler **"Composer"** window:
		- Switch to the **"Parsed"** tab
		- Select the **"GET"** HTTP method
		- Enter your the URL to the DemoTable REST endpoint: **http://_&lt;your_service_name&gt;_.azure-mobile.net/tables/DemoTable**
		- Leave the http protocol version as **"HTTP/1.1"**
		- Make note of the default **Request Headers**.  We won't change them for now. 
		- Click the **"Execute"** button to submit the get request.
	- In the main **"Fiddler Web Debugger"** window
		- In the "Session" list to the right, notice the **200** status code. The screen shot doesn't show the entire session, it's been sized down to fit.  
		- On the **"Inspectors"** tab, click the **"TextView"** tab for the response
		- Notice that the response is an pair of empty square brackets, **"[]"**.  That means no data was returned.  We don't have any records in the table, so we didn't get any back!

	![Empty Result](images/290-empty-result.png?raw=true "Empty Result")

	> **Note:** The request succeeded because we enabled the "Everyone" permissions for Insert, Update, Delete and Read on the DemoTable table.  That means we don't need to supply an application key, or have previously authenticated to interact with the tables data.  Later, we'll change the permissions to require the application key, and see that we then need to include the key in the HTTP headers for the request to succeed.  

- Another handy way to use the fiddler **Composer** to submit request is via the **"Scratchpad"** ([Read More](http://fiddler2.com/documentation/Generate-Traffic/Tasks/ResendRequest)):
	- In the **"Composer"** window, switch to the **"Scratchpad"** tab.
	- **Drag the session from the "Sessions" list** in the main "Fiddler Web Debugger" window over **to the "Scratchpad"**  
	- You can then **select the text for the request** in the Scratch pad and **click the "Execute" button**.  That is a handy way to copy request, easily modify them, and execute them.  
	- We'll use this method for the rest of the API demos to simplify the instructions:

	![Drag a Session to the Scratch Pad](images/300-drag-a-session-to-the-scratch-pad.png?raw=true "Drag a Session to the Scratch Pad")

---
<a name="RESTPosts" />
### Insert (POST) Data with REST ###

Ok, we need to add some data to our table to make it more interesting. 

- **Paste the following** into the Fiddler **Scratchpad** to execute a POST request to the DemoTable REST API and insert a new record in the table.  Select the entire requets (you can tripple-click with the most in the request text to select the entire requets), then click the "Execute" button:

````HTML
==========================================================

POST http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

{
  "firstName": "Bret",
  "lastName": "Stateham"
}
````

- In the **"Fiddler Web Debugger"** window, Change to the **JSON** tab in for both the request and the response to view the result.  Notice that the object returned in the result has a GUID supplied for it's ID.  That's cool! The response from the POST includes the updated id field:

	![Post Result](images/310-post-result.png?raw=true "Post Result")

	> **Note:** Remember that **Dynamic Schema** was enabled on the Mobile Service by default.  That means that columns for the **firstName**, and **lastName** values we submitted should have been created on the fly.  

- Run another GET request to verify that the new record is returned.  Paste the following GET request into the scratchpad and execute it:

````HTML
==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json
````
- Here's what it should look like:

	![GET Initial Record](images/315-get-initial-record.png?raw=true "GET Initial Record")

- You can verify the new record and table columns using the portal. 

- Click on the "DemoTable" name on the "DATA" page for your Mobile Service in the Windows Azure Management Portal.  On the "BROWSE" page for your table, you should see the record we just inserted:

	![New Record in Portal](images/320-new-record-in-portal.png?raw=true "New Record in Portal")

- You should also see the new columns on the "COLUMNS" page:

	![New Columns in Portal](images/330-new-columns-in-portal.png?raw=true "New Columns in Portal")

	> **Note:** Remember that once created, columns can NOT be changed dynamically.  If I want to drop or alter the columns (say change the data type), I'll need to do that from a lower level SQL client like Visual Studio, SQL Server Management Studio, sqlcmd, etc.

- Finally, notice that you can see the new columns and records from a SQL Client as well.  Back in Visual Studio, you can refresh the columns for the DemoTable table, as well as query data in the table (**"TOOLS"** | **"SQL Server"** | **"New Query"**, then connect to the database on the Azure SQL Server instance):

	![New Record and Columns in Visual Studio](images/340-new-record-and-columns-in-visual-studio.png?raw=true "New Record and Columns in Visual Studio")

- Here's the SQL I used for the query.  Replace the "your_service_schema_name" with the appropriate value for your service :

````HTML
SELECT 
  id, 
  firstName, 
  lastName,
  __createdAt,
  __updatedAt, 
  __version
FROM your_service_schema_name.DemoTable;
````

- **COOL!**, let's insert another bunch of records:  Copy and paste ALL of the following requests into the Scratchpad, select them one by one, and execute them each individually:

````HTML
==========================================================

POST http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

{
  "firstName": "Daniel",
  "lastName": "Egan"
}

==========================================================

POST http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

{
  "firstName": "Adam",
  "lastName": "Tuliper"
}

==========================================================

POST http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

{
  "firstName": "Bruno",
  "lastName": "Terkaly"
}

==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json


````

- With the final GET request, you should get all the records back:
	
![Multiple Records in GET](images/350-multiple-records-in-get.png?raw=true "Multiple Records in GET")

- The get's we have performed so far have been basically equivilent to "SELECT * FROM ..." SQL statements.  You can be more selective in your queries.  Read all about how to [Query records using the REST API in the documentation](http://msdn.microsoft.com/en-us/library/windowsazure/jj677199.aspx). 

Paste and Excute the following request on the Fiddler Composer Scratchpad to use the **$filter** query operator to return just the record with the firstName='Bret':  (Note the use of single quotes for the string literal).

````HTML
==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable?$filter=(firstName%20eq%20'Bret') HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json
````

- And verify the result in the Inspector:

	![Filtered Result in the Inspector](images/360-filtered-result-in-the-inspector.png?raw=true "Filtered Result in the Inspector")

---
<a name="RESTPatches" />
### Update (PATCH) Data with REST ###

- Allright, cool stuff, We've seen reads and inserts. What about updates?  For updates the Mobile Services REST API leverages the PATCH method (as opposed to PUT).  The benefit of PATCH is that it allows Partial updates to the record.  You don't need to supply all the values for the record to modify.  

- To use the PATCH method, use a URL that includes the records' ID.  For example, the URL for a patch method on our DemoTable would look like:<br/><br/>
**http:&#47;&#47;wamsdemo01.azure-mobile.net/tables/DemoTable/_&lt;id guid here&gt;_**

- Use the following **PATCH** and **GET** requests from the Fiddler Composer's **Scratchpad** to **update "Bret"'s first name to "Brett"** and verify the result:<br/><br/> **!!!!! WARNING- YOU NEED TO CHANGE THE URL FOR BOTH REQUESTS WITH THE PROPER ID FOR BRET'S RECORD !!!!!**

````HTML
PATCH http://wamsdemo01.azure-mobile.net/tables/DemoTable/A0308978-1655-4207-B0E7-A1412E710B2F HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

{
  "firstName": "Brett"
}

==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable/A0308978-1655-4207-B0E7-A1412E710B2F HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

````

- Here's what the result of the GET should look like:


	![PATCH Result](images/370-patch-result.png?raw=true "PATCH Result")

---
<a name="RESTDeletes" />
### Delete (DELETE) Data with REST ###

- So, based on the PATCH excample above, you can probably guess how to do a delete.  Basically use the DELETE verb, with the URL that includes the ID of the record to delete!  No request body is needed.  Easy-peasy!

- Try these request from Scratch pad.  Notice that the HTTP Status Code returned for the DELETE requets is **"204 - No Content"**.  The DELETE operation doesn't return any data.  Also, notice that after the DELETE, the GET returns a Status Code of **"404 - Not Found"**  because there are no longer any records with that specific ID:<br/><br/> **!!!!! WARNING- YOU NEED TO CHANGE THE URL FOR BOTH REQUESTS WITH THE PROPER ID FOR BRET'S RECORD !!!!!**

````HTML
==========================================================

DELETE http://wamsdemo01.azure-mobile.net/tables/DemoTable/A0308978-1655-4207-B0E7-A1412E710B2F HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable/A0308978-1655-4207-B0E7-A1412E710B2F HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

````

- Here's what it looks like:

	![DELETE Results](images/380-delete-results.png?raw=true "DELETE Results")

- Lastly, try a **GET** request that returns all records in the table:

````HTML
==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json

````

- And again, here's what it looks like: 

	![Get All Records after Delete](images/390-get-all-records-after-delete.png?raw=true "Get All Records after Delete")

---
<a name="UseRestWithKey" />
### Use the REST API with the Application Key ###

So far, we have been accessing the DemoTable data anonymously, and with no restrictions.  In Mobile Services, the first line of defense (albeit an extremely weak one) is the **"Application Key"**. The "Application Key" is created, along with another key called the "Master Key" when your mobile service is created. You can change the keys if you feel they have been discovered.  But I'll tell you that you can pretty much guarantee that your "Application Key" will be discovered if you use it.  That is because it will likely need to be embedded in the client code (and thus discoverable using de-compilers, or just viewing the source if it's accessible).  Because of that, it isn't really a great choice for security.  

The "Master Key" should never by used by (or at least embedded into) client applications.  The Master Key is used for certain operations that only "administrators" of the service should perform, or only by code that should perform those actions.  We'll reserve discussion of the Master Key for a later tutorial.  

- First, let's restrict our table permissions to only allowing operations from client requests that inclue the application key.
- In the **"Windows Azure Management Portal"**, open the your mobile service's **"DATA"** page, then open the **"DemoTable"** table, and go to the **"Permissions"** tab. Change the permissions for all operations to be **"Anybody with the Application Key"**, and click **"Save"** to save the changes.

	![Require Application Key](images/400-require-application-key.png?raw=true "Require Application Key")

- Retry the last GET in the Fiddler Scratchpad, and notice that now we get a Status code of **"401 - Unauthorized" as a result: 

	![Unauthorized Access](images/410-unauthorized-access.png?raw=true "Unauthorized Access")

- To fix that problem, we need to include the **"X-ZUMO-APPLICATION"** HTTP header in our REST API request, along with the actual application key value.  Past the following GET Request into the Fiddler Composer's Scratchpad:

````HTML
==========================================================

GET http://wamsdemo01.azure-mobile.net/tables/DemoTable HTTP/1.1
User-Agent: Fiddler
Host: wamsdemo01.azure-mobile.net
Accept: application/json
X-ZUMO-APPLICATION: PASTE_YOUR_KEY_HERE

````

- Next, we need to get the **"Application Key"** value for our mobile service. In the management portal, change the the "DASHBOARD" page for your mobile service, and click the "Manage Keys" button along the bottom:

	![Manage Keys](images/420-manage-keys.png?raw=true "Manage Keys")

- In the "Manage Access Keys" window, click they "copy" icon (![Copy Icon](images/copy-icon.png?raw=true "Copy Icon")) next to the **APPLICATION KEY** to copy the key to the clipboard. If prompted by IE to allow access to the clipboard click "Allow access", then close the "Manage Access Keys" window:

	![Copy the Application Key to the Clipboard](images/425-copy-the-application-key-to-the-clipboard.png?raw=true "Copy the Application Key to the Clipboard")

- Return to the fiddler **Scratchpad** and **replace** **"PASTE_YOUR_KEY_HERE"** with the **key you just copied to the clipboard**. Then execute the request.  You should get records returned now:

	![GET Request with Application Key](images/430-get-request-with-application-key.png?raw=true "GET Request with Application Key")

<a name="Summary" />
## Summary ##

Hopefully this demo has shown you how easy it is to create an Azure Mobile Service, and how easily we can create tables to store data in our Azure Mobile Service.  

This demo focused on the REST API through which client applications can access the mobile service by using a low level HTTP client (Fiddler).  We alos used regular HTTP requests, not HTTPS (SSL) requests.  Normally, you wouldn't do either of those. 

The normal way to interact with your Azure Mobile Service is from a client application that runs on Windows 8, Windows Phone, iOS, Android, in the browser, or on all of them.  You would then use a client SDK that hides the low level HTTP calls from you and provides a simplified programming model, and that transfers data security using HTTPS.  

In coming demo's we'll look at using Windows Azure Mobile Services with client side SDKs on a variety of platforms. 

