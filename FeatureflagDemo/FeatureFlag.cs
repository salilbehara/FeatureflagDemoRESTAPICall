using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FeatureflagDemo
{
    public class FeatureSettings
    {
        public string name { get; set; }
        public bool qa { get; set; }
        public bool prod { get; set; }
        public string application { get; set; }
        public bool dev { get; set; }
    }

    public class Item
    {
        public FeatureSettings FeatureSettings { get; set; }
        public string appName { get; set; }
        public string Feature_ID { get; set; }
    }

    public class AppFeatureFlagData
    {
        public List<Item> Items { get; set; }
        public int Count { get; set; }
        public int ScannedCount { get; set; }
    }

    public class FeatureFlagResult
    {
        public Item Item { get; set; }
    }

}
