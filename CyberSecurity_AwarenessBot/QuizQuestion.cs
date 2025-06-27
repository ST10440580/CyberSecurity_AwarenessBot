using System.Collections.Generic;

namespace CyberSecurity_AwarenessBot
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public string CorrectChoice { get; set; }
        public List<string> Choices { get; set; }
    }
}
