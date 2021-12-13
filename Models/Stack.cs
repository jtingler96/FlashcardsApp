﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcardsApp.Models
{
     class Stack
    {
        public int Id { get; set; }
        public string StackName { get; set; }
        public virtual ICollection<Flashcard> Flashcards { get; set; }
    }
}
