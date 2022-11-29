//BeXide 2022-11-14
//by MurakamiKazuki

using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace BX.CommandToolKit.EditorInternal
{
    internal class MonoScriptSelectDropdown : AdvancedDropdown
    {
        internal event System.Action<MonoScript> onSelectScript;

        internal MonoScriptSelectDropdown(System.Action<MonoScript> callback)
            : base(new AdvancedDropdownState())
        {
            onSelectScript += callback;

            var minimumSize = this.minimumSize;
            minimumSize.y = 15 * EditorGUIUtility.singleLineHeight;
            this.minimumSize = minimumSize;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("New Command");

            var scripts = AssetDatabase.FindAssets($"t:{nameof(MonoScript)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<MonoScript>)
                .Where(script =>
                {
                    var cls = script?.GetClass();
                    return cls != null && cls.IsSubclassOf(typeof(BaseCommand));
                })
                .OrderBy(script => script.GetClass().FullName)
                .ToArray();

            foreach(var script in scripts)
            {
                var spritStrings = script.GetClass().FullName.Split('.');
                var parent       = root;
                var lastString   = spritStrings.LastOrDefault();

                foreach(string str in spritStrings)
                {
                    var foundChildItem = parent.children
                        .FirstOrDefault(item => item.name == str);
                    if(foundChildItem != null)
                    {
                        parent = foundChildItem;
                        continue;
                    }

                    if(str == lastString)
                    {
                        var child = new MonoScriptSelectDropdownItem(script);
                        parent.AddChild(child);
                        parent = child;
                    }
                    else
                    {
                        var child = new AdvancedDropdownItem(str);
                        parent.AddChild(child);
                        parent = child;
                    }

                }
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            if(item is MonoScriptSelectDropdownItem monoSelect)
            {
                onSelectScript?.Invoke(monoSelect.Script);
            }
            else
            {
                base.ItemSelected(item);
            }
        }
    }
}