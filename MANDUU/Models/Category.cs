using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANDUU.Models
{
    public class Category
    {
        public Category(short id, string name, string image, short parentId)
        {
            Id = id;
            Name = name;
            Image = image;
            ParentId = parentId;
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public short ParentId { get; set; }

        public bool IsMainCategory => ParentId == 0;
    }
       
}
