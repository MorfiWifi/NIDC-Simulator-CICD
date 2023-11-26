using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.List
{
    public class PagingList<T> : IPagingList<T>
    {

        public PagingList() { }

        public PagingList(IList<T> data, int totalRecords, int totalPages, int perPage, int page)
        {
            Data = data;
            TotalRecords = totalRecords;
            TotalPages = totalPages;
            PerPage = perPage;
            Page = page;
        }

        public static IPagingList<T> Create(IList<T> list, int perPage, int page)
        {
            var count = list.Count;
            var skip = (page - 1) * perPage;
            var take = perPage;
            var pages = (int)Math.Ceiling((decimal)count / take);
            var data = list.Skip(skip).Take(take).ToList();
            return new PagingList<T>(data, count, pages, perPage, page);
        }

        public static async Task<IPagingList<T>> Create(IQueryable<T> query, int perPage, int page)
        {
            var count = await query.CountAsync();
            var skip = (page - 1) * perPage;
            var take = perPage;
            var pages = (int)Math.Ceiling((decimal)count / take);
            var data = await query.Skip(skip).Take(take).ToListAsync();
            return new PagingList<T>(data, count, pages, perPage, page);
        }

        public IList<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; }

    }
}
