namespace WaveTrumpet.UI.Themes
{
    public class Rule
    {
        public Rule(string targetKey, string sourceKey)
        {
            TargetKey = targetKey;
            SourceKey = sourceKey;
        }

        public string TargetKey { get; private set; }

        public string SourceKey { get; private set; }
    }
}
