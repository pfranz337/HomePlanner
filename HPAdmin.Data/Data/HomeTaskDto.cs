using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPAdmin.Data.Data
{
    public class DtoDataBase(Guid id)
    {
        public Guid Id
        {
            get;
            set;
        } = id;

        protected DtoDataBase() : this(Guid.NewGuid())
        {
        }
    }

    public class HomeTaskDto : DtoDataBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
