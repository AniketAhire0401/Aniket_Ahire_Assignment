using System;
using System.Collections.Generic;

namespace Aniket_Ahire_Assignment_2
{
    // Item class with ID, Name, Price, and Quantity properties
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Item(int id, string name, double price, int quantity)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }
    }

    // Inventory class to manage a collection of items
    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
        }

        // Add a new item to the inventory
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        // Display all items in the inventory
        public void DisplayItems()
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        // Find an item by ID
        public Item FindItemById(int id)
        {
            return items.Find(item => item.ID == id);
        }

        // Update an item's information
        public bool UpdateItem(int id)
        {
            Item item = FindItemById(id);
            if (item != null)
            {
                Console.WriteLine("What do you want to update?");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Price");
                Console.WriteLine("3. Quantity");
                Console.Write("Choose an option: ");
                int updateChoice = Convert.ToInt32(Console.ReadLine());

                switch (updateChoice)
                {
                    case 1:
                        Console.Write("Enter new Item Name: ");
                        string newName = Console.ReadLine();
                        item.Name = newName;
                        break;

                    case 2:
                        Console.Write("Enter new Item Price: ");
                        double newPrice = Convert.ToDouble(Console.ReadLine());
                        item.Price = newPrice;
                        break;

                    case 3:
                        Console.Write("Enter new Item Quantity: ");
                        int newQuantity = Convert.ToInt32(Console.ReadLine());
                        item.Quantity = newQuantity;
                        break;

                    default:
                        Console.WriteLine("Invalid option. No updates made.");
                        return false;
                }

                Console.WriteLine("Item updated successfully.");
                return true;
            }

            return false;
        }

        // Delete item from the inventory
        public bool DeleteItem(int id)
        {
            Item item = FindItemById(id);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            while (true)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Display All Items");
                Console.WriteLine("3. Find Item by ID");
                Console.WriteLine("4. Update Item");
                Console.WriteLine("5. Delete Item");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Item ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Item Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Item Price: ");
                        double price = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Item Quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        Item newItem = new Item(id, name, price, quantity);
                        inventory.AddItem(newItem);
                        break;

                    case 2:
                        inventory.DisplayItems();
                        break;

                    case 3:
                        Console.Write("Enter Item ID to find: ");
                        int findId = Convert.ToInt32(Console.ReadLine());
                        Item foundItem = inventory.FindItemById(findId);
                        if (foundItem != null)
                        {
                            Console.WriteLine(foundItem);
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Item ID to update: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());
                        bool updated = inventory.UpdateItem(updateId);
                        if (!updated)
                        {
                            Console.WriteLine("Item not found or no updates made.");
                        }
                        break;

                    case 5:
                        Console.Write("Enter Item ID to delete: ");
                        int deleteId = Convert.ToInt32(Console.ReadLine());
                        bool deleted = inventory.DeleteItem(deleteId);
                        if (deleted)
                        {
                            Console.WriteLine("Item deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Item not found.");
                        }
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
