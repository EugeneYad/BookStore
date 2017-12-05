using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using IndexBatch = Microsoft.Azure.Search.Models.IndexBatch;

namespace BookStore.Helpers
{
    public static class SearchHelper
    {
        private static ISearchIndexClient _bookIdxClient;
        private static ISearchIndexClient _playersIdxClient;
        private static ISearchIndexClient _studentsIdxClient;

        public static void Init()
        {
            var serviceClient = CreateSearchServiceClient();
            UploadBooks(serviceClient);
            UploadStudents(serviceClient);
            UploadPlayers(serviceClient);
        }

        public static Models.SearchResult Search(string text)
        {
            var books = _bookIdxClient.Documents.Search<BookEx>(text).Results.Select(el => el.Document);
            var players = _playersIdxClient.Documents.Search<PlayerEx>(text).Results.Select(el => el.Document);
            var students = _studentsIdxClient.Documents.Search<StudentEx>(text).Results.Select(el => el.Document);

            var searchResult = new Models.SearchResult
            {
                Books = books.ToList(),
                Players = players.ToList(),
                Students = students.ToList()
            };

            return searchResult;
        }

        public static void AddToIndex<T>(ICollection<T> items)
            where T : class 
        {
            if (items.Count == 0) return;
            
            if (typeof(T) == typeof(Book))
            {
                var books = items.Select(el => new BookEx(el as Book));
                Upload(_bookIdxClient, books);
            }
            if (typeof(T) == typeof(Player))
            {
                var players = items.Select(el => new PlayerEx(el as Player));
                Upload(_playersIdxClient, players);
            }
            if (typeof(T) == typeof(Student))
            {
                var students = items.Select(el => new StudentEx(el as Student));
                Upload(_studentsIdxClient, students);
            }
        }

        private static SearchServiceClient CreateSearchServiceClient()
        {
            string searchServiceName = @"demosearch-yy";
            string adminApiKey = "416AF6074F4D68A0CF4841FC13CB2746";

            return new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
        }

        private static void UploadPlayers(SearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists("playersidx"))
            {
                _playersIdxClient = serviceClient.Indexes.GetClient("playersidx");

                var db = new SoccerContex();
                if (db.Players.Any())
                {
                    var players = db.Players.ToList().Select(el => new PlayerEx(el));

                    Upload(_playersIdxClient, players);
                }
            }
        }

        private static void UploadStudents(SearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists("studentsidx"))
            {
                _studentsIdxClient = serviceClient.Indexes.GetClient("studentsidx");

                StudentsContext db = new StudentsContext();
                var students = db.Students.ToList().Select(el => new StudentEx(el));

                Upload(_studentsIdxClient, students);
            }
        }

        private static void UploadBooks(SearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists("bookstoreidx"))
            {
                _bookIdxClient = serviceClient.Indexes.GetClient("bookstoreidx");

                BookContext db = new BookContext();
                var booksEf = db.Books;
                var books = booksEf.ToList().Select(el => new BookEx(el));

                Upload(_bookIdxClient, books);
            }
        }

        private static void Upload<T>(ISearchIndexClient indexClient, IEnumerable<T> docs)
            where T : class
        {
            var batch = IndexBatch.MergeOrUpload(docs);

            try
            {
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException e)
            {
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }
        }
    }
}