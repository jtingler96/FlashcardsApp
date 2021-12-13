using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcardsApp.Models
{
    class Flashcard
    {
        public int Id { get; set; }
        public string CardFront { get; set; }
        public string CardBack { get; set; }
        public int StackId { get; set; }
        public virtual Stack Stack { get; set; }
    }
}
