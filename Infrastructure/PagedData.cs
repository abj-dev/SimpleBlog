using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleBlog.Infrastructure
{
    public class PagedData<T> : IEnumerable<T>
    {
        // Fields
        private readonly IEnumerable<T> _currentItems;

        // Properties
        public int TotalCountOfItems { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public int NextPage
        {
            get
            {
                if(!HasNextPage)
                    throw new InvalidOperationException();

                return CurrentPage + 1;
            }
        }
        public int PreviousPage
        {
            get
            {
                if(!HasPreviousPage)
                    throw new InvalidOperationException();

                return CurrentPage - 1;
            }
        }

        // Constructors
        public PagedData(IEnumerable<T> currentItems, int totalCountOfItems,int currentPage, int itemsPerPage)
        {
            _currentItems = currentItems;

            TotalCountOfItems = totalCountOfItems;

            CurrentPage = currentPage;

            ItemsPerPage = itemsPerPage;

            TotalPages = (int) Math.Ceiling((float) TotalCountOfItems/ItemsPerPage);

            HasNextPage = CurrentPage < TotalPages;

            HasPreviousPage = CurrentPage > 1;
        }

        // IEnumerable Methods
        public IEnumerator<T> GetEnumerator()
        {
            return _currentItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}