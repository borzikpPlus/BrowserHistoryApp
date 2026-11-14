using System;
using System.Collections.Generic;
using System.Text;

class BrowserHistory
{
    private LinkedList<string> history = new LinkedList<string>();
    private LinkedListNode<string> current;

    public BrowserHistory()
    {
    }

    public BrowserHistory(string homePage)
    {
        current = history.AddFirst(homePage);
    }

    public void Visits(string url)
    {
        if (current == null)
        {
            current = history.AddLast(url);
            return;
        }

        while (current.Next != null)
        {
            history.Remove(current.Next);
        }
        current = history.AddAfter(current, url);
    }

    public string Back(int steps)
    {
        while (steps > 0 && current != null && current.Previous != null)
        {
            current = current.Previous;
            steps--;
        }
        return GetCurrentPage();
    }

    public string Forward(int steps)
    {
        while (steps > 0 && current != null && current.Next != null)
        {
            current = current.Next;
            steps--;
        }
        return GetCurrentPage();
    }

    public string GetCurrentPage()
    {
        if (current == null)
        {
            return "Історія порожня";
        }
        return current.Value;
    }

    public void ShowHistory()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("Історія: порожня");
            return;
        }

        List<string> pages = new List<string>();
        for (LinkedListNode<string> node = history.First; node != null; node = node.Next)
        {
            if (node == current)
            {
                pages.Add($"[ {node.Value} ]");
            }
            else
            {
                pages.Add(node.Value);
            }
        }
        Console.WriteLine($"Історія: {string.Join(" -> ", pages)}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        BrowserHistory browser = new BrowserHistory("home.com");

        browser.Visits("google.com");
        browser.Visits("github.com");
        browser.ShowHistory();

        Console.WriteLine($"\nBack(1): {browser.Back(1)}");
        browser.ShowHistory();

        Console.WriteLine("\nVisits(\"stackoverflow.com\")");
        browser.Visits("stackoverflow.com");
        browser.ShowHistory();

        Console.WriteLine($"\nBack(2): {browser.Back(2)}");
        browser.ShowHistory();

        Console.WriteLine($"\nForward(1): {browser.Forward(1)}");
        browser.ShowHistory();

        Console.WriteLine($"\nForward(10): {browser.Forward(10)}");
        browser.ShowHistory();

        Console.WriteLine($"\nBack(10): {browser.Back(10)}");
        browser.ShowHistory();

        Console.WriteLine($"\nПоточна сторінка: {browser.GetCurrentPage()}");

        BrowserHistory empty = new BrowserHistory();
        Console.WriteLine($"\nНовий браузер: {empty.GetCurrentPage()}");
        empty.ShowHistory();
    }
}
