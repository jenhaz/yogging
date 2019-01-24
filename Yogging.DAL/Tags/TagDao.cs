using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yogging.DAL.Tags
{
    [Table("Tags")]
    public class TagDao
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
    }
}
