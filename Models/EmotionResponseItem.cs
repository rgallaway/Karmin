using System.Collections.Generic;

namespace Karmin.Models
{
    public class EmotionResponseItem
    {
        public EmotionStatusItem Status { get; set; }
        public EmotionContentItem Content { get; set; }
    }

    public class EmotionStatusItem
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public double ElapsedTime { get; set; }
        public string ElapsedTimeFormatted { get; set; } 
    }

    public class EmotionContentItem
    {
        public string Text { get; set; }
        public int TextLength { get; set; }
        public SentencePropertiesItem SentenceProperties { get; set; }
        public ConfidenceIndicatorsItem ConfidenceIndicators { get; set; }
        public EmotionsItem Emotions { get; set; }
    }

    public class SentencePropertiesItem
    {
        public int TotalWordCount { get; set; }
        public bool AccentedPunctuation { get; set; }
        public bool Questioning { get; set; }
        public bool Exclamation { get; set; }
    }

    public class ConfidenceIndicatorsItem
    {
        public int ConfidenceRate { get; set; }
        public int SentenceNumber { get; set; }
        public int TotalKnownWord { get; set; }
        public int PerformanceRate { get; set; }
    }

    public class EmotionsItem
    {
        public int Eindex { get; set; }
        public int Happiness { get; set; }
        public int Surprise { get; set; }
        public int Calm { get; set; }
        public int Fear { get; set; }
        public int Sadness { get; set; }
        public int Anger { get; set; }
        public int Disgust { get; set; }
        public int EmotionalIntensityRate { get; set; }
        public List<string> Sentences { get; set; }
        public List<List<object>> StrongestEmotionBySentence { get; set; }
    }
}