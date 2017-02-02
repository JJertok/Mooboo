using System;
using MooBoo.Infrastructure.Interfaces.DataLayer;

namespace MooBoo.Model.DataLayer
{
    public class LogItem : DataObject
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string FileName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public override DataObject Clone()
        {
            return new LogItem
            {
                CategoryId = CategoryId,
                Category = null,
                FileName = FileName,
                Start = Start,
                Stop = Stop,
                Id = Id
            };
        }
    }
}
