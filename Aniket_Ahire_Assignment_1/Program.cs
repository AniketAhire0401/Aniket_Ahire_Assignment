using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aniket_Ahire_Assignment_1
{
    class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    internal class Program
    {
        static List<Task> tasks = new List<Task>();
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n-----------------------------------------------------");
                Console.WriteLine("Task List Application");
                Console.WriteLine("1. Create a task");
                Console.WriteLine("2. Read tasks");
                Console.WriteLine("3. Update a task");
                Console.WriteLine("4. Delete a task");
                Console.WriteLine("5. Exit");
                Console.WriteLine("-----------------------------------------------------");
                Console.Write("\nEnter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateTask();
                        break;
                    case 2:
                        ReadTasks();
                        break;
                    case 3:
                        UpdateTask();
                        break;
                    case 4:
                        DeleteTask();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CreateTask()
        {
            Console.Write("\nEnter task title: ");
            string title = Console.ReadLine();
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Task newTask = new Task { Title = title, Description = description };
            tasks.Add(newTask);
            Console.WriteLine("\nTask created successfully.");
        }

        static void ReadTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("\nNo tasks available.");
            }
            else
            {
                Console.WriteLine("\nTask List:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"Task {i + 1}: {tasks[i].Title} - {tasks[i].Description}");
                }
            }
        }

        static void UpdateTask()
        {
            Console.Write("\nEnter the index of the task to update: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > tasks.Count)
            {
                Console.WriteLine("\nInvalid task index.");
                return;
            }

            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Enter new description: ");
            string newDescription = Console.ReadLine();

            tasks[index - 1].Title = newTitle;
            tasks[index - 1].Description = newDescription;
            Console.WriteLine("\nTask updated successfully.");
        }

        static void DeleteTask()
        {
            Console.Write("\nEnter the index of the task to delete: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > tasks.Count)
            {
                Console.WriteLine("Invalid task index.");
                return;
            }

            tasks.RemoveAt(index - 1);
            Console.WriteLine("\nTask deleted successfully.");
        }
    }
}
