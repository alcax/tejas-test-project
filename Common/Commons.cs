using SampleProject.Common;
namespace SampleProject.Models
{
    public static class DefaultPagination
    {
        public const int PageNo = 1;
        public const int RecordsPerPage = 20;
    }
    public class ActionOutputBase
    {
        public ActionStatus Status { get; set; }
        public String Message { get; set; }
        public List<String> Results { get; set; }
    }
    public class ActionOutput : ActionOutputBase
    {
    }

    public class PagingResult<T>
    {
        public List<T> List { get; set; } = new List<T>();
        public int TotalCount { get; set; } = 0;
        public ActionStatus Status { get; set; } = ActionStatus.Successfull;
        public string Message { get; set; } = "List Fetched Successfully";
    }
    public class PagingModel
    {
        public string TimeZone { get; set; }
        public Guid UserId { get; set; }
        private int pageno { get; set; }
        public int PageNo
        {
            get { return pageno <= 0 ? 1 : pageno; }
            set { pageno = value; }
        }

        private int recordsperpage; // field
        public int RecordsPerPage
        {
            get { return recordsperpage <= 0 ? 10 : recordsperpage; }
            set { recordsperpage = value; }  // set method
        }
        private string search = "";
        public string Search
        {
            get { return string.IsNullOrEmpty(search) ? search : search.Trim().ToLower(); }
            set { search = value; }  // set method
        }
        public PagingModel()
        {
            if (PageNo <= 1)
            {
                PageNo = 1;
            }
            if (RecordsPerPage <= 0)
            {
                RecordsPerPage = 10;
            }
        }

        public string SortBy { get; set; }
        public string SortOrder { get; set; }


        public static PagingModel DefaultModel(string sortBy = "CreatedOn", string sortOder = "Asc")
        {
            return new PagingModel { PageNo = 1, RecordsPerPage = 10, SortBy = sortBy, SortOrder = sortOder };
        }
    }

 


 
}