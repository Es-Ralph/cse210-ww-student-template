using System;
using System.Collections.Generic;

public class Comment
{
    private string _name;
    private string _text;

    public Comment(string name, string text)
    {
        _name = name;
        _text = text;
    }

    public string Name => _name;
    public string Text => _text;
}

public class Video
{
    private string _title;
    private string _author;
    private int _length; // length in seconds
    private List<Comment> _comments;

    public Video(string title, string author, int length)
    {
        _title = title;
        _author = author;
        _length = length;
        _comments = new List<Comment>();
    }

    public string Title => _title;
    public string Author => _author;
    public int Length => _length;

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Learning C#", "Alice", 600);
        video1.AddComment(new Comment("John", "Great explanation!"));
        video1.AddComment(new Comment("Mary", "Very helpful, thanks."));
        video1.AddComment(new Comment("Sam", "Clear and concise."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Object-Oriented Programming", "Bob", 900);
        video2.AddComment(new Comment("Anna", "Loved the examples."));
        video2.AddComment(new Comment("Tom", "Can you explain inheritance next?"));
        video2.AddComment(new Comment("Lucy", "This made OOP easy to understand."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Database Basics", "Charlie", 1200);
        video3.AddComment(new Comment("Mike", "SQL part was excellent."));
        video3.AddComment(new Comment("Sophia", "Very detailed explanation."));
        video3.AddComment(new Comment("David", "Helped me with my assignment."));
        videos.Add(video3);

        // Display details
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.Name}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}