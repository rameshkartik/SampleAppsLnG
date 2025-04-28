using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSlnFilter
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root
    {
        public Solution solution { get; set; }
    }

    public class Solution
    {
        public string path { get; set; }
        public List<string> projects { get; set; }
    }


}
