using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewFeatureDevelopmentAPI.Models
{
    public class SearchParameters
    {
        public string FileSize { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
    }
}
