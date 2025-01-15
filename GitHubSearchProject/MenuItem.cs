using Newtonsoft.Json.Linq;

namespace GitHubSearchAPI
{
    public class MenuItem
    {
       public string Code { get; set; }
       public string Name { get; set; }
        public MenuItem(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
