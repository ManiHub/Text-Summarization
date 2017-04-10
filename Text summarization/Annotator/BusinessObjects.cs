using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP_WPF
{
    public class Articles
    {
        public List<Article> articles { get; set; }
    }

    public class Article
    {
        public string url { get; set; }
        public string content { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
    }
}
