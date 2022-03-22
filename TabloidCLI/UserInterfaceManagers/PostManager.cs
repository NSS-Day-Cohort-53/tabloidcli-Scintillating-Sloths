using System;
using System.Collections.Generic;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Manager");
            Console.WriteLine(" 1) Add Post");
            Console.WriteLine(" 2) List Posts");
            Console.WriteLine(" 3) Delete Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Post Details");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "0":
                    return _parentUI;
                case "1":
                    Add();
                    return this;
                case "2":
                    List();
                    return this;
                case "3":
                    Delete();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Title} ({post.Url})");
            }    
        }
        public Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Publish Date: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            AuthorManager authMan = new AuthorManager(this, _connectionString);
            post.Author = authMan.Choose();

            BlogManager blogMan = new BlogManager(this, _connectionString);
            post.Blog = blogMan.Choose();

            _postRepository.Insert(post);
        }
        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New publish date (blank to leave unchanged: ");
            string stringDate = Console.ReadLine();
            
            if (!string.IsNullOrWhiteSpace(stringDate))
            {
                DateTime date = DateTime.Parse(stringDate);
                postToEdit.PublishDateTime = date;
            }
            AuthorManager authMan = new AuthorManager(this, _connectionString);
            Author chosenAuthor = authMan.Choose("New author (blank to leave unchanged: ");
            if (chosenAuthor != null)
            {
                postToEdit.Author = chosenAuthor;
            }
            BlogManager blogMan = new BlogManager(this, _connectionString);
            Blog chosenBlog = blogMan.Choose("New blog (blank to leave unchanged: ");
            if (chosenBlog != null)
            {
                postToEdit.Blog = chosenBlog;
            }

            _postRepository.Update(postToEdit);
        }
        private void Delete()
        {
            Post postBeingDeleted = Choose("Which post would you like to delete?");
            if (postBeingDeleted != null)
            {
                _postRepository.Delete(postBeingDeleted.Id);
            }
        }
    }
}
