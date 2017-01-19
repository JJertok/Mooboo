using System;

namespace MooBoo.Model.DataLayer
{
    public class LogItem
    {
        public Category Category { get; set; }
        public string FileName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}
