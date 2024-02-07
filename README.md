# Microsoft.Cosmo.Crud.Helper

![build](https://img.shields.io/badge/build-passing-d.svg)

`Microsoft.Cosmo.Crud.Helper` is a .NET library that simplifies working with Azure `Cosmos DB`, providing straightforward asynchronous operations to manage items within your `Cosmos containers`. This package abstracts complex queries and operations into easy-to-use methods for common tasks such as` creating`, `updating`, `deleting`, and `querying items`.

### Features

- CRUD Operations: Easily create, read, update, and delete items in your Cosmos DB container.
- Asynchronous Support: All operations are designed to be asynchronous, promoting efficient resource use in your applications.
- LINQ Query Support: Leverage LINQ queries to fetch items based on complex predicates without dealing directly with SQL queries.
- Partition Key Support: Direct support for partition keys to ensure optimal performance and scalability of your Cosmos DB operations.

## Prerequisites

.NET SDK (version depending on your project requirements)
`Azure Cosmos DB account` and setup completed

##### Usage

Below are examples of how to use the main features of `Microsoft.Cosmo.Crud.Helper`.

### Initializing

To start using the package, initialize your Cosmos client and container
csharp
Copy code

```c#
var cosmosClient = new CosmosClient("YourConnectionString");
 var database = cosmosClient.GetDatabase("YourDatabaseName");
var container = database.GetContainer("YourContainerName"");
CosmoRepository<T> Service = new CosmoRepository<T>(container);
```

Replace `T` with your data model class.

### Creating an Item

csharp
Copy code

```c#
var item = new YourModel { /* Initialize your model properties */ };
await yourService.CreateItemAsync(item, "yourPartitionKey");
```

### Getting an Item by ID

```c#
var item = await yourService.GetByIdAsync("yourItemId", "yourPartitionKey");
```

### Updating an Item

```c#
item.SomeProperty = "New Value";
await yourService.UpdateItemAsync("yourItemId", item, "yourPartitionKey");
```

### Deleting an Item

```c#
await yourService.DeleteItemAsync("yourItemId", "yourPartitionKey");
```

### Querying Items

```c#
var items = await yourService.GetItemsAsync(x => x.SomeProperty == "SomeValue", "yourPartitionKey");
```

### Getting All Items

Be cautious with GetAllItemsAsync in production environments, especially in large datasets, as it may impact performance.

```c#
var allItems = await yourService.GetAllItemsAsync();
```

### For Example

Console Application to check

- Change Start Project to `Cosmos_console` to run console App.
- Add `Your Connection String` , `Your Database Name` , `Your containerName` to `Program.cs` file.
- Run the console App.

```c#
var cosmosClient = new CosmosClient("Your Connection String");
    var database = cosmosClient.GetDatabase("Your DataBase Name");
    Container _container = database.GetContainer("Your Container Name");
```
