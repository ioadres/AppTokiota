using System;
namespace AppTokiota.Attributes
{
    public class TagAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public TagAttribute(String tag)
        {
            Tag = tag;
        }

        // Keep a variable internally ...
        public readonly String Tag;
    }

}
