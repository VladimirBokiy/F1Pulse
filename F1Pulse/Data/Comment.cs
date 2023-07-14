using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace F1Pulse.Data;

public class Comment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual ApplicationUser? ApplicationUser { get; set; }
    [Required]
    public string NewsId { get; set; }

    [Required]
    public DateTime CommentDate { get; set; }
    [Required]
    public String Content { get; set; }
}