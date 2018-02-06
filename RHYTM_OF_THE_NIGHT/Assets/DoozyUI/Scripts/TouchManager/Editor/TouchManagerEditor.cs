// Copyright (c) 2015 - 2018 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEditor;
using UnityEngine;

namespace DoozyUI.Gestures
{
    [CustomEditor(typeof(TouchManager), true)]
    [DisallowMultipleComponent]
    public class TouchManagerEditor : QEditor
    {
        TouchManager touchManager { get { return (TouchManager)target; } }

        SerializedProperty
            debug,
            minSwipeLength,
            longTapDuration,
            useEightDirections;

        float GlobalWidth { get { return DUI.GLOBAL_EDITOR_WIDTH; } }

        protected override void SerializedObjectFindProperties()
        {
            base.SerializedObjectFindProperties();

            debug = serializedObject.FindProperty("debug");
            minSwipeLength = serializedObject.FindProperty("minSwipeLength");
            longTapDuration = serializedObject.FindProperty("longTapDuration");
            useEightDirections = serializedObject.FindProperty("useEightDirections");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            requiresContantRepaint = true;
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerTouchManager.texture, WIDTH_420, HEIGHT_42);
            serializedObject.Update();
            QUI.QToggle("debug", debug);
            QUI.Space(SPACE_2);
            QUI.QObjectPropertyField("Min Swipe Length", minSwipeLength, GlobalWidth, 20, false);
            QUI.Space(SPACE_2);
            QUI.QObjectPropertyField("Long Tap Duration", longTapDuration, 174, 20, false);
            QUI.Space(SPACE_2);
            QUI.QToggle("use eight directions", useEightDirections);
            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }
    }
}
