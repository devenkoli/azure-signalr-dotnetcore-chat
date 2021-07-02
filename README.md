# azure-signalr-dotnetcore-chat
A aimple asp.net core application to demonstrate multiple chat rooms with azure signalr service

For this example to work with Azure Signlar service, you need to create a Signalr resource with service mode settings as Default.

- Add a file D:\messages.json and D:\rooms.json  in your file directory or you can locate it at desired path for check on localhost.
- Please remember to change the path in ChatHub.cs file.
- For production use, create a desired backend resource.

In Startup.cs file
mention the connection string in the following code.
- services.AddSignalR().AddAzureSignalR("azure-signalr-connection-string");
 
or you can simply use for Local Hub integration.
- services.AddSignalR();
