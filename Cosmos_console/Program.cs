using Cosmos_console.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Cosmo.Crud.Helper.Implementation;

try
{
    var cosmosClient = new CosmosClient("Your Connection String");
    var database = cosmosClient.GetDatabase("Your DataBase Name");
    Container _container = database.GetContainer("Your Container Name");
    CosmoRepository<Conversation> conversation = new CosmoRepository<Conversation>(_container);
    CosmoRepository<Message> message = new CosmoRepository<Message>(_container);

    var exit = false;
    while (!exit)
    {
        Console.WriteLine("Enter 1. To GetAlldata");
        Console.WriteLine("Enter 2. To create a conversation ");
        Console.WriteLine("Enter 3. To Delete Conversation");
        Console.WriteLine("Enter 4. To update conversation");
        Console.WriteLine("Enter 5. To GetAllMessage");
        Console.WriteLine("Enter 6. To getMessagebyid");
        Console.WriteLine("Enter 7. To getconversationbyuserid");
        Console.WriteLine("Enter 8. To Exit");
        int cases = Convert.ToInt32(Console.ReadLine());
        switch (cases)
        {
            case 1:

                List<Conversation> List = await conversation.GetAllItemsAsync();
                var i = 0;
                foreach (var item in List)
                {
                    Console.WriteLine($"item no {i++}");
                    Console.WriteLine(item.id);
                    Console.WriteLine(item.userId);
                    Console.WriteLine(item.createdAt);
                    Console.WriteLine(item.updatedAt);
                    Console.WriteLine(item.title);
                    Console.WriteLine(item.type);
                }

                break;
            case 2:
                Console.WriteLine("Enter Userid");
                var userid = Console.ReadLine();
                Console.WriteLine("Enter title");
                var title = Console.ReadLine();
                var conv = new Conversation
                {
                    id = Guid.NewGuid().ToString(),
                    type = "conversation",
                    createdAt = DateTime.UtcNow.ToString("o"),
                    updatedAt = DateTime.UtcNow.ToString("o"),
                    userId = userid,
                    title = title
                };
                var response = await conversation.CreateItemAsync(conv, userid);
                if (!string.IsNullOrEmpty(response.id))
                {

                    Console.WriteLine(response.id);


                    Console.WriteLine(response.title);
                    Console.WriteLine(response.userId);
                    Console.WriteLine(response.type);
                    Console.WriteLine(response.updatedAt);
                    Console.WriteLine(response.createdAt);
                }
                break;
            case 3:
                Console.WriteLine("Enter id of conversation");
                var id = Console.ReadLine();
                Console.WriteLine("Enter Partionkey / user id");
                var pkey = Console.ReadLine();
                await conversation.DeleteItemAsync(id, pkey);
                break;
            case 4:
                Console.WriteLine("Please Enter the convid");
                var convid = Console.ReadLine();
                Console.WriteLine("please Enter Userid");
                var usid = Console.ReadLine();
                var conve = await conversation.GetByIdAsync(convid, usid);
                Console.WriteLine("Enter Title ");
                var titles=Console.ReadLine();
                conve.title = titles;
                var conveupdate=await conversation.UpdateItemAsync(convid,conve,usid);
                Console.WriteLine(conveupdate.id);
                Console.WriteLine(conveupdate.title);
                Console.WriteLine(conveupdate.userId);
                Console.WriteLine(conveupdate.type);
                Console.WriteLine(conveupdate.updatedAt);
                Console.WriteLine(conveupdate.createdAt);

                break;
            case 5:

                IEnumerable<Message> Listmessage = await message.GetItemsAsync(x => x.type == "message", "cf04c210-de21-4d01-b4e9-6aa272e9fa9c");
                var j = 0;
                foreach (var item in Listmessage.ToList())
                {
                    Console.WriteLine($"item no {j++}");
                    Console.WriteLine(item.id);
                    Console.WriteLine(item.userId);
                    Console.WriteLine(item.createdAt);
                    Console.WriteLine(item.updatedAt);
                    Console.WriteLine(item.feedback);
                    Console.WriteLine(item.type);
                }

                break;
            case 6:
                Console.WriteLine("Please Enter the messageid");
                var messageid = Console.ReadLine();
                Console.WriteLine("please Enter Userid");
                var userId = Console.ReadLine();
                var messages = await message.GetByIdAsync(messageid, userId);
                Console.WriteLine(messages.id);
                Console.WriteLine(messages.feedback);
                Console.WriteLine(messages.userId);
                Console.WriteLine(messages.content);
                Console.WriteLine(messages.content);
                Console.WriteLine(messages.conversationId);
                Console.WriteLine(messages.createdAt);
                break;
            case 7:
                Console.WriteLine("Please Enter the userid");
                var useriid = Console.ReadLine();

                var conversationss = await conversation.GetItemsAsync(x => x.userId == useriid && x.type == "conversation", useriid);
                foreach (var items in conversationss)
                {

                    Console.WriteLine(items.id);
                    Console.WriteLine(items.title);
                    Console.WriteLine(items.userId);
                    Console.WriteLine(items.type);
                    Console.WriteLine(items.updatedAt);
                    Console.WriteLine(items.createdAt);

                }
                break;
            case 8:
                exit = true;
                break;


        }
}
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
    
}


