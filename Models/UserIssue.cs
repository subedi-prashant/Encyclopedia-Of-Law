using System;
using System.Collections.Generic;

#nullable disable

namespace Encyclopedia_Of_Laws.Models
{
    public partial class UserIssue
    {
        public int IssueId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime? IssueDate { get; set; }
        public string Status { get; set; }
    }
}
