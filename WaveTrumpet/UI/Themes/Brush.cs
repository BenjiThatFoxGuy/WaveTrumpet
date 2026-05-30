namespace WaveTrumpet.UI.Themes
{
    public class Brush
    {
        public Brush(string resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; private set; }
    }
}
