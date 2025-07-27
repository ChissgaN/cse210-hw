using System;
using System.Collections.Generic;

class Comment
{
    private string _commenterName;
    private string _commentText;

    public Comment(string commenterName, string commentText)
    {
        _commenterName = commenterName;
        _commentText = commentText;
    }

    public string GetCommenterName() => _commenterName;
    public string GetCommentText() => _commentText;
}

class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthInSeconds} seconds");
        Console.WriteLine($"Comments ({GetNumberOfComments()}):");
        foreach (Comment comment in _comments)
        {
            Console.WriteLine($"- {comment.GetCommenterName()}: {comment.GetCommentText()}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Video video1 = new Video("Learning C#", "Code Academy", 600);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Carol", "Can you make a video on interfaces?"));

        Video video2 = new Video("Top 10 Travel Destinations", "Travel Vibes", 420);
        video2.AddComment(new Comment("Dave", "I want to go to Bali now!"));
        video2.AddComment(new Comment("Emma", "I’ve been to 3 of these!"));
        video2.AddComment(new Comment("Frank", "Love this list!"));

        Video video3 = new Video("How to Bake a Cake", "Chef Laura", 360);
        video3.AddComment(new Comment("Grace", "Mine turned out great!"));
        video3.AddComment(new Comment("Henry", "Can I use almond flour instead?"));
        video3.AddComment(new Comment("Ivy", "Thanks for the step-by-step guide."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display all video info
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
