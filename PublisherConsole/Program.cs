using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PublisherData;
using PublisherDomain;
using System.Diagnostics;

namespace PublisherConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SortAscending();
            Console.WriteLine("------------------------------------------");
            //SortDescending();
            RetriveDataFromArtist();

        }

        static void RetriveDataFromArtist()
        {
            using var context= new PubContext();
            var artist = context.Artists.Find(1);
            artist.Covers.Add(context.Covers.Find(1));
            context.SaveChanges();
            //var aritstcovers = context.ArtistCovers.Include(x => x.Artist).Include(x=>x.Cover).AsSplitQuery().FirstOrDefault(x=>x.ArtistId == 3);
            //Console.WriteLine($"{aritstcovers?.ArtistId}, {aritstcovers?.Artist.FirstName}");
            //Console.WriteLine($"{aritstcovers?.CoverId}, {aritstcovers?.Cover.DesignIdea}");
        }
        static void DeleteAuthor()
        {
            using var context = new PubContext();
            var author = context.Authors.Find(-4);
            context.Authors.Remove(author);
            context.SaveChanges();
        }
        static void updateAuthor()
        {
            using var context = new PubContext();
            var author = context.Authors.Find(1);
            author.FirstName = "Ismail";
            context.Authors.Update(author);
            context.SaveChanges();
            Console.WriteLine(author);
        }
        static void EagerLoading()
        {
            using var context = new PubContext();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var authors = context.Authors.Include(e => e.Books).Where(e => EF.Functions.Like(e.FirstName, "%a%")).ToList();
            timer.Stop();
            Console.WriteLine($"Time Without using AsSplitQuery: {timer.ElapsedMilliseconds}ms");
            Console.WriteLine($"Count Of List {authors.Count()}");

            timer.Reset();

            timer.Start();
            var authorsAsSplitQuery = context.Authors.Include(e => e.Books).Where(e => EF.Functions.Like(e.FirstName, "%a%")).AsSplitQuery().ToList();
            timer.Stop();

            Console.WriteLine($"Time using AsSplitQuery: {timer.ElapsedMilliseconds}ms");
            Console.WriteLine($"Count Of List {authorsAsSplitQuery.Count()}");
        }

        static void ExplicitLoading()
        {
            using var context = new PubContext();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var author = context.Authors.Find(1);
            var books = context.Entry(author).Collection(e => e.Books).Query().Where(e => e.Title.Contains('C')).ToList();
            timer.Stop();
            Console.WriteLine($"Time using Query: {timer.ElapsedMilliseconds}");
            
        }
        static void Pagination()
        {
            const int PAGE_SIZE = 2;
            using var context = new PubContext();
            var authors = context.Authors.AsNoTracking();
            List<Author> authorsPages = new List<Author>();
            int i = 0;
            do
            {
                authorsPages = authors.Skip(i*PAGE_SIZE).Take(PAGE_SIZE).ToList();
                if(authorsPages.Count() != 0)
                Console.WriteLine($"------------ Page {i+1} ---------------");
                foreach (var author in authorsPages)
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName} {author.Id}");
                }
                i++;
            } while (authorsPages.Count() != 0);
        }

        static void AddAuthor(Author author)
        {
            using (var context = new PubContext())
            {
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }

        static void GetAuthorsWithBooks()
        {
            using var context = new PubContext();
            var authors = context.Authors.Where(e => EF.Functions.Like(e.FirstName, "%me%")).ToList();
            foreach(var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName} {author.Id}");
            }
        }
        
        static void AddSomeAuthors()
        {
            AddAuthor(new Author() { FirstName = "Amr", LastName = "Selynee" });
            AddAuthor(new Author() { FirstName = "Badr", LastName = "Saeed" });
            AddAuthor(new Author() { FirstName = "Beltagy", LastName = "dede" });
            AddAuthor(new Author() { FirstName = "Figa", LastName = "figo" });
            AddAuthor(new Author() { FirstName = "Soob", LastName = "hesbor" });
        }
        
        static void SortAscending()
        {
            using var context = new PubContext();
            var list = context.Authors.OrderBy(a => a.FirstName).ThenBy(a => a.LastName).ToList();
            foreach (var author in list)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName} {author.Id}");
            }
        }
        static void SortDescending()
        {
            using var context = new PubContext();
            var list = context.Authors.OrderByDescending(a => a.FirstName).ThenByDescending(a => a.LastName).ToList();
            foreach (var author in list)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName} {author.Id}");
            }
        }


    }
}