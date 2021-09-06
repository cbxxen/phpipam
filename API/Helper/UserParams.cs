//Pagination
//defines Page Item Size
namespace API.Helper
{
    public class UserParams
    {
        //Max Page Size that is possible
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        //default Page Size at beginning
        private int _pageSize=10;

        public int PageSize{
            get => _pageSize;
            //set PageSize, check if it is bigger than MaxPage Size, set it to MaxPageSize, else the value of _pageSize
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        ///For Filtering
        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;
    }
}