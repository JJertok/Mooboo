using System;

namespace MooBoo.Model.DataLayer
{
    public class LogItem : DataObject
    {
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}
