using System;
using System.Windows.Media;

namespace CyberSecurity_AwarenessBot
{
    public class voice_greeting
    {
        private MediaPlayer audioGreet;

        public voice_greeting()
        {
            audioGreet = new MediaPlayer();

            // Get the base path
            string fullPath = AppDomain.CurrentDomain.BaseDirectory;

            // Replace build-specific path
            string replaced = fullPath.Replace("\\bin\\Debug\\net8.0-windows", "");

            // Combine with audio file name
            string combinedPath = System.IO.Path.Combine(replaced, "voicegreeting.wav");

            // Load and play the audio
            audioGreet.Open(new Uri(combinedPath, UriKind.Absolute));
            audioGreet.Play();
        }
    }
}
