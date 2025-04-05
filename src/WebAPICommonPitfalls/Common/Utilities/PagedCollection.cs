using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPICommonPitfalls.Common.Utilities
{
    public class PagedCollection<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public IEnumerable<T> Items { get; set; }
        private readonly string _baseUri;

        public PagedCollection(IEnumerable<T> items, int page, int pageSize, int totalCount, string baseUri)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            _baseUri = baseUri;
            FirstPage = CreatePageUri(1, pageSize);
            LastPage = CreatePageUri((int)Math.Ceiling((double)totalCount / pageSize), pageSize);
            NextPage = HasNextPage ? CreatePageUri(page + 1, pageSize) : null;
            PreviousPage = HasPreviousPage ? CreatePageUri(page - 1, pageSize) : null;
        }

        private Uri CreatePageUri(int pageNumber, int pageSize)
        {
            var uriBuilder = new UriBuilder(_baseUri)
            {
                Query = $"pageNumber={pageNumber}&pageSize={pageSize}"
            };
            return uriBuilder.Uri;
        }

        public static async Task<PagedCollection<T>> CreateAsync(
            IQueryable<T> query,
            int page,
            int pageSize,
            string baseUri)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedCollection<T>(items, page, pageSize, totalCount, baseUri);
        }
    }
}