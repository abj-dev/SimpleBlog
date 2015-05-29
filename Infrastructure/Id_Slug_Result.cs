using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Infrastructure
{
    public class Id_Slug_Result
    {
        public int Id { get; set; }
        public string Slug { get; set; }

        public Id_Slug_Result(int id, string slug)
        {
            this.Id = id;
            this.Slug = slug;
        }
    }
}
