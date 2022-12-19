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
        internal ScenarioData ScenarioAsset { get; }
        internal string Name { get; }
        internal string Path { get; }
        internal long Bytes { get; }
        internal DateTimeOffset LastWriteTime { get; }

        internal ScenarioElement(string path)
        {
            var data = AssetDatabase.LoadAssetAtPath<ScenarioData>(path);
            var info = new FileInfo(path);

            ScenarioAsset = data;
            Name     = data.name;
            Path     = path;
            Bytes    = info.Length;
            LastWriteTime = info.LastWriteTime.ToLocalTime();
        }

        internal string DownloadSizeToString()
        {
            long downloadSize = Bytes;

            double kiloBytes = (double)downloadSize / 1024;
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
                return $"{downloadSize}B";
            }
        }

        internal (bool validate, int count) GetValidateCount()
        {
            int count = 0;
            foreach(var cmd in ScenarioAsset.Commands)
            {
                if(!CommandValidator.Validate(cmd, out _))
                {
                    count++;
                }
            }

            bool validate = count == 0;
            return (validate, count);
        }
    }
}