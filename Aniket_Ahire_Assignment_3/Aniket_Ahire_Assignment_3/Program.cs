using System;
using System.Collections.Generic;
using System.Linq;
public class Book
{
    public string UId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublishedDate { get; set; }
    public string ISBN { get; set; }
    public bool IsIssued { get; set; }
}

public class Member
{
    public string UId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
}

public class Issue
{
    public string UId { get; set; }
    public string BookId { get; set; }
    public string MemberId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; }
}





public class LibraryRepository
{
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();
    private List<Issue> issues = new List<Issue>();

    // Book operations
    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public Book GetBookById(string uid)
    {
        return books.FirstOrDefault(b => b.UId == uid);
    }

    public Book GetBookByName(string name)
    {
        return books.FirstOrDefault(b => b.Title.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public List<Book> GetAvailableBooks()
    {
        return books.Where(b => !b.IsIssued).ToList();
    }

    public List<Book> GetIssuedBooks()
    {
        return books.Where(b => b.IsIssued).ToList();
    }

    public void UpdateBook(Book updatedBook)
    {
        var book = GetBookById(updatedBook.UId);
        if (book != null)
        {
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishedDate = updatedBook.PublishedDate;
            book.ISBN = updatedBook.ISBN;
            book.IsIssued = updatedBook.IsIssued;
        }
    }

    // Member operations
    public void AddMember(Member member)
    {
        members.Add(member);
    }

    public Member GetMemberById(string uid)
    {
        return members.FirstOrDefault(m => m.UId == uid);
    }

    public List<Member> GetAllMembers()
    {
        return members;
    }

    public void UpdateMember(Member updatedMember)
    {
        var member = GetMemberById(updatedMember.UId);
        if (member != null)
        {
            member.Name = updatedMember.Name;
            member.DateOfBirth = updatedMember.DateOfBirth;
            member.Email = updatedMember.Email;
        }
    }

    // Issue operations
    public void AddIssue(Issue issue)
    {
        var book = GetBookById(issue.BookId);
        if (book != null && !book.IsIssued)
        {
            issues.Add(issue);
            book.IsIssued = true;
        }
    }

    public Issue GetIssueById(string uid)
    {
        return issues.FirstOrDefault(i => i.UId == uid);
    }

    public void UpdateIssue(Issue updatedIssue)
    {
        var issue = GetIssueById(updatedIssue.UId);
        if (issue != null)
        {
            issue.BookId = updatedIssue.BookId;
            issue.MemberId = updatedIssue.MemberId;
            issue.IssueDate = updatedIssue.IssueDate;
            issue.ReturnDate = updatedIssue.ReturnDate;
            issue.IsReturned = updatedIssue.IsReturned;

            var book = GetBookById(updatedIssue.BookId);
            if (book != null)
            {
                book.IsIssued = !updatedIssue.IsReturned;
            }
        }
    }
}
class Program
{
    static void Main()
    {
        var repository = new LibraryRepository();
        bool running = true;

        while (running)
        {

            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Get Book by ID");
            Console.WriteLine("3. Get Book by Name");
            Console.WriteLine("4. Get All Books");
            Console.WriteLine("5. Get Available Books");
            Console.WriteLine("6. Get Issued Books");
            Console.WriteLine("7. Update Book");
            Console.WriteLine("8. Add Member");
            Console.WriteLine("9. Get Member by ID");
            Console.WriteLine("10. Get All Members");
            Console.WriteLine("11. Update Member");
            Console.WriteLine("12. Issue Book");
            Console.WriteLine("13. Get Issue by ID");
            Console.WriteLine("14. Update Issue");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddBook(repository);
                    break;
                case "2":
                    GetBookById(repository);
                    break;
                case "3":
                    GetBookByName(repository);
                    break;
                case "4":
                    GetAllBooks(repository);
                    break;
                case "5":
                    GetAvailableBooks(repository);
                    break;
                case "6":
                    GetIssuedBooks(repository);
                    break;
                case "7":
                    UpdateBook(repository);
                    break;
                case "8":
                    AddMember(repository);
                    break;
                case "9":
                    GetMemberById(repository);
                    break;
                case "10":
                    GetAllMembers(repository);
                    break;
                case "11":
                    UpdateMember(repository);
                    break;
                case "12":
                    IssueBook(repository);
                    break;
                case "13":
                    GetIssueById(repository);
                    break;
                case "14":
                    UpdateIssue(repository);
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

    static void AddBook(LibraryRepository repository)
    {
        var book = new Book();
        Console.Write("Enter UId: ");
        book.UId = Console.ReadLine();
        Console.Write("Enter Title: ");
        book.Title = Console.ReadLine();
        Console.Write("Enter Author: ");
        book.Author = Console.ReadLine();
        Console.Write("Enter Published Date (yyyy-mm-dd): ");
        book.PublishedDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter ISBN: ");
        book.ISBN = Console.ReadLine();
        book.IsIssued = false;

        repository.AddBook(book);
        Console.WriteLine("Book added successfully.");
    }

    static void GetBookById(LibraryRepository repository)
    {
        Console.Write("Enter Book UId: ");
        var book = repository.GetBookById(Console.ReadLine());
        if (book != null)
        {
            Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Published Date: {book.PublishedDate}, IsIssued: {book.IsIssued}");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void GetBookByName(LibraryRepository repository)
    {
        Console.Write("Enter Book Title: ");
        var book = repository.GetBookByName(Console.ReadLine());
        if (book != null)
        {
            Console.WriteLine($"UId: {book.UId}, Author: {book.Author}, ISBN: {book.ISBN}, Published Date: {book.PublishedDate}, IsIssued: {book.IsIssued}");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void GetAllBooks(LibraryRepository repository)
    {
        var books = repository.GetAllBooks();
        foreach (var book in books)
        {
            Console.WriteLine($"UId: {book.UId}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Published Date: {book.PublishedDate}, IsIssued: {book.IsIssued}");
        }
    }

    static void GetAvailableBooks(LibraryRepository repository)
    {
        var books = repository.GetAvailableBooks();
        foreach (var book in books)
        {
            Console.WriteLine($"UId: {book.UId}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Published Date: {book.PublishedDate}, IsIssued: {book.IsIssued}");
        }
    }

    static void GetIssuedBooks(LibraryRepository repository)
    {
        var books = repository.GetIssuedBooks();
        foreach (var book in books)
        {
            Console.WriteLine($"UId: {book.UId}, Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Published Date: {book.PublishedDate}, IsIssued: {book.IsIssued}");
        }
    }

    static void UpdateBook(LibraryRepository repository)
    {
        var book = new Book();
        Console.Write("Enter Book UId to Update: ");
        book.UId = Console.ReadLine();
        Console.Write("Enter Title: ");
        book.Title = Console.ReadLine();
        Console.Write("Enter Author: ");
        book.Author = Console.ReadLine();
        Console.Write("Enter Published Date (yyyy-mm-dd): ");
        book.PublishedDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter ISBN: ");
        book.ISBN = Console.ReadLine();
        Console.Write("Is Issued (true/false): ");
        book.IsIssued = bool.Parse(Console.ReadLine());

        repository.UpdateBook(book);
        Console.WriteLine("Book updated successfully.");
    }

    static void AddMember(LibraryRepository repository)
    {
        var member = new Member();
        Console.Write("Enter UId: ");
        member.UId = Console.ReadLine();
        Console.Write("Enter Name: ");
        member.Name = Console.ReadLine();
        Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
        member.DateOfBirth = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Email: ");
        member.Email = Console.ReadLine();

        repository.AddMember(member);
        Console.WriteLine("Member added successfully.");
    }

    static void GetMemberById(LibraryRepository repository)
    {
        Console.Write("Enter Member UId: ");
        var member = repository.GetMemberById(Console.ReadLine());
        if (member != null)
        {
            Console.WriteLine($"Name: {member.Name}, Date of Birth: {member.DateOfBirth}, Email: {member.Email}");
        }
        else
        {
            Console.WriteLine("Member not found.");
        }
    }

    static void GetAllMembers(LibraryRepository repository)
    {
        var members = repository.GetAllMembers();
        foreach (var member in members)
        {
            Console.WriteLine($"UId: {member.UId}, Name: {member.Name}, Date of Birth: {member.DateOfBirth}, Email: {member.Email}");
        }
    }

    static void UpdateMember(LibraryRepository repository)
    {
        var member = new Member();
        Console.Write("Enter Member UId to Update: ");
        member.UId = Console.ReadLine();
        Console.Write("Enter Name: ");
        member.Name = Console.ReadLine();
        Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
        member.DateOfBirth = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Email: ");
        member.Email = Console.ReadLine();

        repository.UpdateMember(member);
        Console.WriteLine("Member updated successfully.");
    }

    static void IssueBook(LibraryRepository repository)
    {
        var issue = new Issue();
        Console.Write("Enter Issue UId: ");
        issue.UId = Console.ReadLine();
        Console.Write("Enter Book UId: ");
        issue.BookId = Console.ReadLine();
        Console.Write("Enter Member UId: ");
        issue.MemberId = Console.ReadLine();
        issue.IssueDate = DateTime.Now;
        issue.IsReturned = false;

        repository.AddIssue(issue);
        Console.WriteLine("Book issued successfully.");
    }

    static void GetIssueById(LibraryRepository repository)
    {
        Console.Write("Enter Issue UId: ");
        var issue = repository.GetIssueById(Console.ReadLine());
        if (issue != null)
        {
            Console.WriteLine($"BookId: {issue.BookId}, MemberId: {issue.MemberId}, IssueDate: {issue.IssueDate}, ReturnDate: {issue.ReturnDate}, IsReturned: {issue.IsReturned}");
        }
        else
        {
            Console.WriteLine("Issue not found.");
        }
    }

    static void UpdateIssue(LibraryRepository repository)
    {
        var issue = new Issue();
        Console.Write("Enter Issue UId to Update: ");
        issue.UId = Console.ReadLine();
        Console.Write("Enter Book UId: ");
        issue.BookId = Console.ReadLine();
        Console.Write("Enter Member UId: ");
        issue.MemberId = Console.ReadLine();
        Console.Write("Enter Issue Date (yyyy-mm-dd): ");
        issue.IssueDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter Return Date (yyyy-mm-dd) or leave empty if not returned: ");
        var returnDate = Console.ReadLine();
        issue.ReturnDate = string.IsNullOrEmpty(returnDate) ? (DateTime?)null : DateTime.Parse(returnDate);
        issue.IsReturned = issue.ReturnDate.HasValue;

        repository.UpdateIssue(issue);
        Console.WriteLine("Issue updated successfully.");
    }
}
