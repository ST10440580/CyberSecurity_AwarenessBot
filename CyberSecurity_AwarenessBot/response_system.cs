using System;
using System.Collections;
using System.Collections.Generic;


namespace CyberSecurity_AwarenessBot 
{ 

public class response_system
{
    private readonly sentiment_handler sentimentAnalyzer = new sentiment_handler();
    private readonly memory_management memory = new memory_management();

    private readonly Dictionary<string, List<string>> keywordResponses = new()
        {
             {"phishing", new List<string>{
                "Phishing is a type of cyberattack where attackers try to trick people into giving away sensitive information like passwords, credit card numbers, or personal details—usually by pretending to be a trustworthy source via email or messages.",
                "Phishing typically involves fake emails that look like they're from legitimate companies (like your bank or Netflix), asking you to click a link and \"verify your account\"—but the link leads to a malicious site designed to steal your information.",
                "It's a form of social engineering where cybercriminals pose as someone you trust—like a coworker, manager, or company—to get you to send them confidential data or make payments.",
                " Phishing often creates a sense of urgency (e.g., “Your account will be locked in 24 hours”) to pressure victims into clicking links or downloading attachments without thinking critically.",
                "While most phishing happens through email, it can also occur via SMS (\"smishing\"), phone calls (\"vishing\"), or even on social media platforms through fake messages or ads. ",
                "Phishing emails may look professional and convincing, often copying logos, language, and formatting from legitimate companies to seem authentic.",
                "Phishing is the digital equivalent of baiting a hook, where attackers “fish” for victims by sending out large volumes of fake messages hoping someone will bite.",
                "Phishing is a social engineering tactic used to manipulate people into performing actions, such as clicking malicious links or revealing confidential info.",
                "Phishing involves impersonation, where an attacker pretends to be a legitimate entity (like a bank or employer) to gain access to private information."
            }},
            {"password", new List<string>{
                "A password is a secret combination of characters that users create to prove their identity and gain access to systems, accounts, or devices.",
                "Passwords act as a first line of defense against unauthorized access. Without the correct password, attackers are blocked from entering your accounts or devices.",
                "Strong passwords usually include a mix of uppercase and lowercase letters, numbers, and special characters to make them harder for hackers to guess or crack." ,
                "They are one of the most common authentication methods used in both personal and professional settings to confirm that you are who you say you are." ,
                "Weak passwords—like \"123456\" or \"password\"—are easy for hackers to guess. That’s why using complex, unique passwords for every account is important.",
                "It’s important to avoid using easily guessed personal information, such as your name, birthdate, favorite sports team, or pet’s name, in passwords. These details are often discoverable on social media and can be exploited in targeted attacks known as social engineering.",
                "Reusing the same password across multiple accounts is risky. If one account gets compromised, attackers may try that password on other services you use. Always use a different, unique password for each online account to limit the impact of any single breach."
            }},
            {"privacy", new List<string>{
                "Privacy is the right to control your personal information and how it’s collected, used, or shared—whether online or offline.",
                "Online privacy means protecting your data—like browsing history, location, and social media activity—from being tracked or misused by websites, companies, or hackers.",
                "A key principle of privacy is only sharing the minimum amount of information necessary. The less you share, the lower the risk of that data being stolen or misused.",
                "Privacy includes managing the permissions you grant to apps on your phone or computer—such as access to your camera, microphone, or contacts.",
                "Social media platforms are a major source of privacy exposure, as users often overshare personal details like locations, birthdays, and relationship status. This information can be used by advertisers, data brokers, or even cybercriminals for profiling or identity theft.",
                "Third-party cookies and trackers follow your activity across multiple websites, building a detailed profile of your interests, behavior, and online habits. These are often used for targeted advertising, but they also raise significant privacy concerns due to the lack of transparency and control.",
                "The more apps you install, the more you expose your personal data, especially when those apps request unnecessary permissions like access to your camera, contacts, or location. Always review permissions and consider deleting apps you no longer use or trust."
            }},
            {"safe browsing" ,new List<string> {
                "Safe browsing is the practice of using the internet in a way that protects your personal data, devices, and privacy from online threats like malware, scams, or malicious websites.",
                "When visiting websites, always check for “HTTPS” in the URL. The \"S\" stands for \"secure\" and means the site encrypts your data, helping prevent it from being intercepted by attackers.",
                "Never click on links or download files from unknown sources. Even if they appear in emails or messages from people you know, they could be part of a phishing scam.",
                "Browsers release frequent updates to patch security flaws. Enabling automatic updates ensures you have the latest protections against known vulnerabilities." ,
                "Consider using browser security extensions that enhance privacy and security, such as ad blockers, anti-tracking tools, and script blockers. These add-ons help reduce exposure to harmful content and prevent unauthorized tracking.",
                "Avoid clicking on suspicious pop-ups or banner ads, especially those that claim you’ve won a prize or that your device is infected. These are classic phishing techniques designed to trick users into downloading malware or sharing personal data.",
                "Never download software or files from untrusted or unknown sources. Malicious websites can disguise malware as useful tools or media files. Stick to official websites and verified app stores when installing software.",
                "Teach children and less tech-savvy users in your household about safe browsing. This includes avoiding shady websites, not clicking on unknown links, and understanding the risks of downloading suspicious files or chatting with strangers online."
            }},
              {"browsing" ,new List<string> {
                "Safe browsing is the practice of using the internet in a way that protects your personal data, devices, and privacy from online threats like malware, scams, or malicious websites.",
                "When visiting websites, always check for “HTTPS” in the URL. The \"S\" stands for \"secure\" and means the site encrypts your data, helping prevent it from being intercepted by attackers.",
                "Never click on links or download files from unknown sources. Even if they appear in emails or messages from people you know, they could be part of a phishing scam.",
                "Browsers release frequent updates to patch security flaws. Enabling automatic updates ensures you have the latest protections against known vulnerabilities." ,
                "Consider using browser security extensions that enhance privacy and security, such as ad blockers, anti-tracking tools, and script blockers. These add-ons help reduce exposure to harmful content and prevent unauthorized tracking.",
                "Avoid clicking on suspicious pop-ups or banner ads, especially those that claim you’ve won a prize or that your device is infected. These are classic phishing techniques designed to trick users into downloading malware or sharing personal data.",
                "Never download software or files from untrusted or unknown sources. Malicious websites can disguise malware as useful tools or media files. Stick to official websites and verified app stores when installing software.",
                "Teach children and less tech-savvy users in your household about safe browsing. This includes avoiding shady websites, not clicking on unknown links, and understanding the risks of downloading suspicious files or chatting with strangers online."
            }},
                   {"scam" ,new List<string> {
                "Online scams can be convincing ,A scam is a dishonest scheme or trick used to cheat someone out of their money, personal information, or access to their accounts. ",
                "Online scams often come through emails, fake websites, or social media, where scammers pretend to be legitimate companies or individuals to fool you into giving up sensitive info.",
                "Many scams offer something that sounds amazing—like a free prize, inheritance, or miracle investment—but these are bait to lure victims into giving money or data.",
                "Phishing is one common form of scam where attackers impersonate trusted sources to steal credentials, financial info, or infect your device with malware."
        }} };


        private readonly Dictionary<string, string> userMemory = new();
    private int responseCount = 0;

    public void SetUserName(string name) => userMemory["name"] = name;

        public string GetResponse(string userInput)
        {
            string name = userMemory.ContainsKey("name") ? userMemory["name"] : "User";
            memory.AddToMemory($"{name},{userInput}");

        
          

        string sentiment = sentimentAnalyzer.DetectSentiment(userInput);
        string sentimentMsg = HandleSentiment(sentiment, name);

        if (userInput.ToLower().Contains("interested in"))
        {
            foreach (var topic in keywordResponses.Keys)
            {
                if (userInput.ToLower().Contains(topic))
                {
                    userMemory["favoriteTopic"] = topic;
                    return $"Great! I'll remember that you're interested in {topic}.";
                }
            }
        }

        foreach (var keyword in keywordResponses.Keys)
        {
            if (userInput.ToLower().Contains(keyword))
            {
                var responses = keywordResponses[keyword];
                var reply = responses[new Random().Next(responses.Count)];

                responseCount++;
                if (userMemory.ContainsKey("favoriteTopic") && responseCount % 3 == 0)
                    return $"{sentimentMsg}\nSince you're interested in {userMemory["favoriteTopic"]}, here's something to keep in mind: {reply}";

                return $"{sentimentMsg}\n{reply}";
            }
        }

        if (userInput.ToLower().Contains("understand") || userInput.ToLower().Contains("confused") || userInput.ToLower().Contains("more details"))
            return $"{sentimentMsg}\nNo worries, {name}. I can clarify that. Could you tell me what part you're unsure about?";

        return $"{sentimentMsg}\nSorry {name}, I didn't quite get that. Please ask about passwords, phishing, or safe browsing.";
    }

    private string HandleSentiment(string sentiment, string name) => sentiment switch
    {
        "negative" => $"I'm sorry you're feeling that way, {name}. Cybersecurity can feel overwhelming—but you're not alone.",
        "positive" => $"Glad to hear that, {name}! Let's keep that momentum going.",
        _ => ""
    };
}
}