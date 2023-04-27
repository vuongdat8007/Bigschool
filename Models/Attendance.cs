using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bigschool_TH_11.Models
{
    
    public class Attendance
{
    [Key, Column(Order = 1), ForeignKey("Course")]
    public int CourseId { get; set; }

    public virtual Course Course { get; set; }

    [Key, Column(Order = 2), ForeignKey("Attendee")]
    public string AttendeeId { get; set; }

    public virtual ApplicationUser Attendee { get; set; }
}
}