using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System;

namespace BxUni.ScenarioBuilder.EditorInternal
{
    internal class ScenarioTable : TreeView
    {
        const string k_sortedColumnIndexStateKey = "ScenarioEditWindow_sortedColumnIndex";

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
            var index = (ColumnIndex)multiColumnHeader.sortedColumnIndex;
            bool ascending = multiColumnHeader.IsSortedAscending(multiColumnHeader.sortedColumnIndex);

            var items = rootItem
                .children
                .Cast<ScenarioTableItem>();

            IOrderedEnumerable<ScenarioTableItem> orderedEnumerable = null;
            switch (index)
            {
            case ColumnIndex.Name:
                orderedEnumerable = items.OrderBy(item => item.element.Name);
                break;
            case ColumnIndex.Path:
                orderedEnumerable = items.OrderBy(item => item.element.Path);
                break;
            case ColumnIndex.Bytes:
                orderedEnumerable = items.OrderBy(item => item.element.Bytes);
                break;
            case ColumnIndex.LastTime:
                orderedEnumerable = items.OrderBy(item => item.element.LastWriteTime);
                break;
            case ColumnIndex.OpenButton:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
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
            int id = 1;
            foreach(string path in paths)
            {
                root.AddChild(new ScenarioTableItem(id++, path));
            }

            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = (ScenarioTableItem)args.item;

            for(int i=0; i<args.GetNumVisibleColumns(); i++)
            {
                var rect = args.GetCellRect(i);
                var columnIndex = (ColumnIndex)args.GetColumn(i);

                var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;

                switch (columnIndex)
                {
                case ColumnIndex.Name:
                    EditorGUI.LabelField(rect, item.element.Name, labelStyle);
                    break;
                case ColumnIndex.Path:
                    EditorGUI.LabelField(rect, item.element.Path, labelStyle);
                    break;
                case ColumnIndex.Bytes:
                    EditorGUI.LabelField(rect, $"{item.element.DownloadSizeToString()}", labelStyle);
                    break;
                case ColumnIndex.LastTime:
                    EditorGUI.LabelField(rect, $"{item.element.LastWriteTime:yyyy/MM/dd HH:mm:ss}");
                    break;
                case ColumnIndex.OpenButton:
                    if(GUI.Button(rect, "OPEN"))
                    {
                        ScenarioEditFlowWindow.CreateWindow(item.element.ScenarioAsset);
                        Debug.Log(item.element.Path);
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
            return scenarioTableItem.element.Name.Contains(search);
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