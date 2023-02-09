using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoAPI.Models
{
    public class TodoList
    {
        [Key]
        public int TodoNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public String? TodoName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public String? TodoDetail { get; set; }

    }
}
