using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graph.Components.PageNavigation
{
	public class PageNavigationModel
	{
		public int CurrentPage { get; }
		public int ItemsAmount { get; }
		public int PageSize { get; }
		public bool HasNextPage { get; }
		public bool HasPrevPage { get; }
		public IEnumerable<int> Pages { get; }

		public PageNavigationModel(int currentPage, int pages, int itemsAmount, int pageSize = 0)
		{
			if (pageSize == 0)
			{
				pageSize = PageNavigationConfig.DefaultPageSize;
			}
			CurrentPage = currentPage;
			ItemsAmount = itemsAmount;
			PageSize = pageSize;
			Pages = GetPageRange(currentPage, pages);
			HasNextPage = false;
			HasPrevPage = false;
			if (currentPage < Pages.Count())
			{
				HasNextPage = true;
			}
			if (currentPage > 1)
			{
				HasPrevPage = true;
			}
		}

		private static IEnumerable<int> GetPageRange(int currentPage, int pageCount)
		{
			var pageRange = new List<int>();
			if (pageCount <= 8)
			{
				return Enumerable.Range(1, pageCount);
			}

			if (currentPage <= 5)
			{
				pageRange.AddRange(Enumerable.Range(1, currentPage < 5 ? 5 : currentPage + 1));
				pageRange.Add(0);
				pageRange.Add(pageCount);

			}
			else if (pageCount - currentPage < 6)
			{
				pageRange.Add(1);
				pageRange.Add(0);
				var startPageId = currentPage == pageCount - 5 ? currentPage - 1 : pageCount - 5;
				pageRange.AddRange(Enumerable.Range(startPageId, pageCount - startPageId + 1));
			}
			else
			{
				pageRange.Add(1);
				pageRange.Add(0);
				pageRange.AddRange(Enumerable.Range(currentPage - 2, currentPage + 2));
				pageRange.Add(0);
				pageRange.Add(pageCount);
			}

			return pageRange;
		}

		public string NextPageUrl => GetUrl(CurrentPage + 1);

		public string PrevPageUrl => GetUrl(CurrentPage - 1);

		public static string UrlToPage(int page)
		{
			return GetUrl(page);
		}

		private static string GetUrl(int page)
		{
			var queryString = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
			queryString["page"] = page.ToString();
			return $"?{queryString}";
		}
	}
}
