//BeXide 2022-12-12
//by MurakamiKazuki

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ScenarioTable : TreeView
    {
        static readonly string k_sortedColumnIndexStateKey = $"{PlayerSettings.productGUID}_sortedColumnIndex";

        internal ScenarioTable(TreeViewState state, MultiColumnHeader multiColumnHeader)
            : base(state, multiColumnHeader)
        {
            rowHeight = 32;
            showAlternatingRowBackgrounds = true;
            multiColumnHeader.sortingChanged += SortItems;

            multiColumnHeader.ResizeToFit();
            Reload();
            multiColumnHeader.sortedColumnIndex = SessionState.GetInt(k_sortedColumnIndexStateKey, -1);
        }

        internal bool AnyItems()
        {
            return rootItem.hasChildren;
        }

        void SortItems(MultiColumnHeader multiColumnHeader)
        {
            SessionState.SetInt(k_sortedColumnIndexStateKey, multiColumnHeader.sortedColumnIndex);
            int columnIndex = multiColumnHeader.sortedColumnIndex;
            var column = ScenarioTableHeader.GetColumns()[columnIndex];
            bool ascending = multiColumnHeader.IsSortedAscending(multiColumnHeader.sortedColumnIndex);

            var items = rootItem
                .children
                .Cast<ScenarioTableItem>();

            IOrderedEnumerable<ScenarioTableItem> orderedEnumerable = null;
            switch (column)
            {
            case NameColumn:
                orderedEnumerable = items.OrderBy(item => item.element.Name);
                break;
            case PathColumn:
                orderedEnumerable = items.OrderBy(item => item.element.Path);
                break;
            case BytesColumn:
                orderedEnumerable = items.OrderBy(item => item.element.Bytes);
                break;
            case LastTimeColumn:
                orderedEnumerable = items.OrderBy(item => item.element.LastWriteTime);
                break;
            case OpenButtonColumn:
                break;
            case ValidateColumn:
                orderedEnumerable = items.OrderBy(item => item.element.ValidateCount);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
            }

            if(orderedEnumerable != null)
            {
                items = orderedEnumerable.AsEnumerable();
                if (!ascending)
                {
                    items = items.Reverse();
                }   
            }
            rootItem.children = items.Cast<TreeViewItem>().ToList();
            BuildRows(rootItem);
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem
            {
                depth = -1
            };

            var paths = AssetDatabase.FindAssets($"t:{nameof(ScenarioData)}")
                .Select(AssetDatabase.GUIDToAssetPath);
            if (paths.Any())
            {
                int id = 1;
                foreach (string path in paths)
                {
                    root.AddChild(new ScenarioTableItem(id++, path));
                }
            }
            else
            {
                root.children = new List<TreeViewItem>();
            }

            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = (ScenarioTableItem)args.item;

            for(int i=0; i<args.GetNumVisibleColumns(); i++)
            {
                var rect = args.GetCellRect(i);
                int columnIndex = args.GetColumn(i);
                var column = ScenarioTableHeader.GetColumns()[columnIndex];

                var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;

                switch (column)
                {
                case NameColumn:
                    EditorGUI.LabelField(rect, item.element.Name, labelStyle);
                    break;
                case PathColumn:
                    EditorGUI.LabelField(rect, item.element.Path, labelStyle);
                    break;
                case BytesColumn:
                    EditorGUI.LabelField(rect, item.element.BytesToString, labelStyle);
                    break;
                case LastTimeColumn:
                    EditorGUI.LabelField(rect, $"{item.element.LastWriteTime:yyyy/MM/dd HH:mm:ss}");
                    break;
                case OpenButtonColumn:
                    if(GUI.Button(rect, "OPEN"))
                    {
                        ScenarioEditFlowWindow.CreateWindow(item.element.ScenarioAsset);
                        Debug.Log(item.element.Path);
                    }
                    break;
                case ValidateColumn:
                    int    count     = item.element.ValidateCount;
                    bool   validate  = count == 0;
                    string label     = $"{(validate ? "✔" : "✕")} : {count}";
                    using (new ContentColorScope(validate ? Color.green : Color.red))
                    {
                        EditorGUI.LabelField(rect, label, labelStyle);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, null);
                }            
            }
        }

        protected override bool DoesItemMatchSearch(TreeViewItem item, string search)
        {
            var scenarioTableItem = (ScenarioTableItem)item;

            search = search.ToLower();
            string name = scenarioTableItem.element.Name.ToLower();

            if (name.Contains(search))
            {
                return true;
            }
            return false;
        }

        protected override void SingleClickedItem(int id)
        {
            var item = (ScenarioTableItem)FindItem(id, rootItem);
            if (item != null)
            {
                EditorGUIUtility.PingObject(item.element.ScenarioAsset);
            }
        }

        protected override void DoubleClickedItem(int id)
        {
            var item = (ScenarioTableItem)FindItem(id, rootItem);
            if(item != null)
            {
                ScenarioEditFlowWindow.CreateWindow(item.element.ScenarioAsset);
            }
        }
    }
}