# components_umbraco_page_navigation
Page Navigation

Installation steps:
1. Copy all files to the folder 'PageNavigation' to ~\App_Plugins\
2. Setup PageNavigationModel in a controller
```c#
using PageNavigation;

public class SomeSurfaceController : SurfaceController
{
	public ActionResult Index(int page = 1)
	{
		...
		// GET ITEMS
		var totalItemsCount = 10;
		...
		var model = new SomeModel();

		var pageSize = 5;
		var currentPage = page;
		var pagesCount = (int)Math.Ceiling((decimal)totalItemsCount / pageSize);
		var pageModel = new PageNavigationModel(currentPage, pagesCount, totalItemsCount, pageSize);

		model.PageNavigationModel = pageModel;

		return View("Index.cshtml", model);
	}
}
```

3. Add Partial to a view:
```c#
@Html.Partial("/App_Plugins/PageNavigation/Views/PageNavigation.cshtml", Model.PageNavigationModel)
```
