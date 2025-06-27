using System.Collections.Generic;

namespace CyberSecurity_AwarenessBot
{
    public class sentiment_handler
    {
        public string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("hate") || input.Contains("angry") || input.Contains("frustrated") || input.Contains("confused"))
                return "negative";
            else if (input.Contains("love") || input.Contains("great") || input.Contains("happy") || input.Contains("excited"))
                return "positive";
            else
                return "neutral";
        }
    }
}