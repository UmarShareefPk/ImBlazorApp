namespace ImBlazorApp.Helper
{
    public class Common : ICommon
    {
        public string GetStatusNameByCode(string code)
        {
            switch (code)
            {
                case "N":
                    return "New";
                case "C":
                    return "Closed";
                case "A":
                    return "Approved";
                case "I":
                    return "In Progress";
                default:
                    return code;
            }
        }
    }
}
