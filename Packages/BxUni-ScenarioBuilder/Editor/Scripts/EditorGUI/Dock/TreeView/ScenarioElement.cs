//BeXide 2022-12-12
//by MurakamiKazuki

using System;
using System.IO;
using UnityEditor;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    [Serializable]
    internal class ScenarioElement
    {
        internal ScenarioData   ScenarioAsset { get; }
        internal string         Name { get; }
        internal string         Path { get; }
        internal long           Bytes { get; }
        internal string         BytesToString { get; }
        internal DateTimeOffset LastWriteTime { get; }
        internal int            ValidateCount { get; }

        internal ScenarioElement(string path)
        {
            var data = AssetDatabase.LoadAssetAtPath<ScenarioData>(path);
            var info = new FileInfo(path);

            ScenarioAsset = data;
            Name          = data.name;
            Path          = path;
            Bytes         = info.Length;
            BytesToString = DownloadSizeToString(info.Length);
            LastWriteTime = info.LastWriteTime.ToLocalTime();
            ValidateCount = GetValidateCount();
        }

        string DownloadSizeToString(long bytes)
        {
            double kiloBytes = (double)bytes / 1024;
            double megaBytes = kiloBytes / 1024;

            if (megaBytes >= 1)
            {
                megaBytes = Math.Min(9999, Math.Ceiling(megaBytes));
                return $"約{megaBytes:#,###}MB";
            }
            else if (megaBytes >= 0.1)
            {
                megaBytes = Math.Ceiling(megaBytes * 10) / 10;
                return $"約{megaBytes:0.#}MB";
            }
            else if (kiloBytes >= 0.1)
            {
                kiloBytes = Math.Ceiling(kiloBytes * 10) / 10;
                return $"約{kiloBytes:0.#}KB";
            }
            else
            {
                return $"{bytes}B";
            }
        }

        int GetValidateCount()
        {
            int count = 0;
            foreach(var cmd in ScenarioAsset.Commands)
            {
                if(!CommandValidator.Validate(cmd, out _))
                {
                    count++;
                }
            }
            return count;
        }
    }
}