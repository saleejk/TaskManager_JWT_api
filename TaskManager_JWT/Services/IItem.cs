using TaskManager_JWT.Models;

namespace TaskManager_JWT.Services
{
    public interface IItem
    {
        public void createTask(Item items);
        public List<Item> getAllItem();
        public Item getById(int id);
        public void updateItem(int id,Item item);
        public void deleteItem(int id);
    }
    public class ItemService : IItem
    {
        public static List<Item> items = new List<Item>() { new Item() { Id = 1, Title = "saleej", Description = "thats a good tasks", Status = "doing" },
            new Item (){Id=2,Title="babi",Description="avengers End Game",Status="Now watching" } };
        public void createTask(Item item)
        {
            int idd = items.LastOrDefault().Id + 1;
            item.Id= idd;
             items.Add(item);
        }
        public List<Item> getAllItem()
        {
            return items;
        }
        public Item getById(int id)
        {
            return items.FirstOrDefault(e => e.Id == id);
        }
        public void updateItem(int id,Item item) {
        var updateItem=items.FirstOrDefault(e => e.Id == id);
            updateItem.Title = item.Title;
            updateItem.Description = item.Description;
            updateItem.Status= item.Status;

        }
        public void deleteItem(int id)
        {
            var deleteItem=items.FirstOrDefault(e=>e.Id == id);
            items.Remove(deleteItem);
        }
    }
}
