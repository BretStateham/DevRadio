<a name="Top" />
# 001 - WAMS Introduction #

This demo introduces the viewer to Windows Azure Mobile Services.  We focus on the core of Windows Azure Services, independent of the client side SDK used.  To do that, we first create an Azure Mobile Service using the portal, then interact with it using the REST API from Fiddler.  

<a name="pre-requisites" />
## Pre-requisites ##
Developers wishing to follow the demo steps need:

- A Windows Azure Subscription
- Fiddler (http://fiddler2.com)

<a name="steps" />
## Steps ##

- [Create a Windows Azure Mobile Service](CreateWAMS)
- [Create a table with open permissions](CreateTable)
- [Use fiddler to demo the REST API](UseRest)
- [Use fiddler to demo the REST API with the Application Key](UseRestWithKey)

<a name="CreateWAMS" />
### Create a Windows Azure Mobile Service ###

- Open the [Windows Azure Management Portal](https://manage.windowsazure.com) (https://manage.windowsazure.com)
- Create a new Windows Azure Mobile Service

	![10 New Azure Mobile Service](images/10-new-azure-mobile-service.png?raw=true "New Azure Mobile Service")

- Give the Azure Mobile Service:
  - Use a Unique URL
  - Either use an Existing SQL Database, or choose to create a new one
  - Pick an appropriate region

	![20 Create a Mobile Service](images/20-create-a-mobile-service.png?raw=true "Create a Mobile Service")

- Create a new database on the selected server.  If you chose to create a new server provide appropriate administrative credentials and a region for the new Server.  Click the "Check Mark" button in the lower right corner to create the service.  

	![30 - Mobile Service Database](images/30---mobile-service-database.png?raw=true "Mobile Service Database")

- Wait for the new mobile service to be created.  It shouldn't take more than one or two minutes.  

	![40 New Mobile Service Ready](images/40-new-mobile-service-ready.png?raw=true "New Mobile Service Ready")


<a name="CreateTable" />
### Create a table with open permissions ###

<a name="UseRest" />
### Use fiddler to demo the REST API ###

<a name="UseRestWithKey" />
### Use fiddler to demo the REST API with the Application Key ###
