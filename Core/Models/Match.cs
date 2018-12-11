using System;
using Core.Enums;

namespace Core.Models
{
    public class Match
    {
        public DateTime Date { get; set; }
        public string PlayerOneId { get; set; }
        public string PlayerTwoId { get; set; }
        public Result Result { get; set; }

       
    }
}