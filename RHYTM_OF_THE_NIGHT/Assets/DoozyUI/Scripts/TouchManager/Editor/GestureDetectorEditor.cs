// Copyright (c) 2015 - 2018 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using QuickEditor;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI.Gestures
{
    [CustomEditor(typeof(GestureDetector), true)]
    [DisallowMultipleComponent]
    public class GestureDetectorEditor : QEditor
    {
        GestureDetector gestureDetector { get { return (GestureDetector)target; } }

        SerializedProperty
            debug,
            isGlobalGestureDetector,
            overrideTarget, targetGameObject,
            gestureType,
            swipeDirection,
            OnTap, OnLongTap, OnSwipe,
            gameEvents;

        AnimBool
            showTarget,
            showSwipeDirection,
            showOnTap,
            showOnLongTap,
            showOnSwipe,
            showGameEventsAnimBool,
            showNavigation;

        EditorNavigationPointerData editorNavigationData = new EditorNavigationPointerData();

        float GlobalWidth { get { return DUI.GLOBAL_EDITOR_WIDTH; } }
        int MiniBarHeight { get { return DUI.MINI_BAR_HEIGHT; } }

        protected override void SerializedObjectFindProperties()
        {
            base.SerializedObjectFindProperties();

            debug = serializedObject.FindProperty("debug");
            isGlobalGestureDetector = serializedObject.FindProperty("isGlobalGestureDetector");
            overrideTarget = serializedObject.FindProperty("overrideTarget");
            targetGameObject = serializedObject.FindProperty("targetGameObject");
            gestureType = serializedObject.FindProperty("gestureType");
            swipeDirection = serializedObject.FindProperty("swipeDirection");
            OnTap = serializedObject.FindProperty("OnTap");
            OnLongTap = serializedObject.FindProperty("OnLongTap");
            OnSwipe = serializedObject.FindProperty("OnSwipe");
            gameEvents = serializedObject.FindProperty("gameEvents");
        }

        protected override void InitAnimBools()
        {
            base.InitAnimBools();

            showTarget = new AnimBool(!isGlobalGestureDetector.boolValue, Repaint);
            showSwipeDirection = new AnimBool(gestureType.enumValueIndex == (int)GestureType.Swipe, Repaint);

            showOnTap = new AnimBool(false, Repaint);
            showOnLongTap = new AnimBool(false, Repaint);
            showOnSwipe = new AnimBool(false, Repaint);
            showGameEventsAnimBool = new AnimBool(gameEvents.arraySize > 0, Repaint);
            showNavigation = new AnimBool(false, Repaint);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            requiresContantRepaint = true;

            UpdateTarget();
            SyncData();
        }

        void UpdateTarget()
        {
            if(gestureDetector.isGlobalGestureDetector) { return; }
            if(gestureDetector.overrideTarget) { return; }
            if(gestureDetector.targetGameObject == null)
            {
                gestureDetector.targetGameObject = gestureDetector.gameObject;
            }
        }

        void SyncData()
        {
            DUIData.Instance.ValidateUIElements(); //validate the database (used by the Navigation Manager)
            UpdateAllNavigationData();
        }
        void UpdateAllNavigationData()
        {
            if(!UIManager.IsNavigationEnabled)
            {
                return;
            }

            DUIUtils.UpdateNavigationDataList(gestureDetector.navigationPointerData.show, editorNavigationData.showIndex);
            DUIUtils.UpdateNavigationDataList(gestureDetector.navigationPointerData.hide, editorNavigationData.hideIndex);
        }

        public override void OnInspectorGUI()
        {
            DrawHeader(DUIResources.headerGestureDetector.texture, WIDTH_420, HEIGHT_42);

            serializedObject.Update();

            QUI.QToggle("debug", debug);
            QUI.Space(SPACE_2);
            QUI.QToggle("is Global Gesture Detector", isGlobalGestureDetector);
            QUI.Space(SPACE_2);
            showTarget.target = !isGlobalGestureDetector.boolValue;
            if(QUI.BeginFadeGroup(showTarget.faded))
            {
                QUI.BeginHorizontal(GlobalWidth);
                {
                    GUI.enabled = overrideTarget.boolValue;
                    QUI.QObjectPropertyField("Target GameObject", targetGameObject, GlobalWidth - 100);
                    GUI.enabled = true;
                    QUI.Space(SPACE_2);
                    QUI.QToggle("override", overrideTarget, 20);
                }
                QUI.EndHorizontal();
            }
            QUI.EndFadeGroup();
            QUI.Space(SPACE_2);
            showSwipeDirection.target = (GestureType) gestureType.enumValueIndex == GestureType.Swipe;
            QUI.BeginHorizontal(GlobalWidth);
            {
                QUI.QObjectPropertyField("Gesture Type", gestureType, ((GlobalWidth - SPACE_2) / 2), 20, false);
                QUI.Space(SPACE_2);
                if(showSwipeDirection.faded > 0.2f)
                {
                    QUI.QObjectPropertyField("Swipe Direction", swipeDirection, ((GlobalWidth - SPACE_2) / 2) * showSwipeDirection.faded, 20, false);
                }
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
            switch((GestureType) gestureType.enumValueIndex)
            {
                case GestureType.Tap: DUIUtils.DrawUnityEvents(gestureDetector.OnTap.GetPersistentEventCount() > 0, showOnTap, OnTap, "OnTap", GlobalWidth, MiniBarHeight); break;
                case GestureType.LongTap: DUIUtils.DrawUnityEvents(gestureDetector.OnLongTap.GetPersistentEventCount() > 0, showOnLongTap, OnLongTap, "OnLongTap", GlobalWidth, MiniBarHeight); break;
                case GestureType.Swipe: DUIUtils.DrawUnityEvents(gestureDetector.OnSwipe.GetPersistentEventCount() > 0, showOnSwipe, OnSwipe, "OnSwipe", GlobalWidth, MiniBarHeight); break;
            }
            QUI.Space(SPACE_2);
            QUI.DrawCollapsableList("Game Events", showGameEventsAnimBool, gameEvents.arraySize > 0 ? QColors.Color.Blue : QColors.Color.Gray, gameEvents, GlobalWidth, 18, "Not sending any Game Events on gesture... Click [+] to start...");
            QUI.Space(SPACE_2);
            DUIUtils.DrawNavigation(target, gestureDetector.navigationPointerData, editorNavigationData, showNavigation, UpdateAllNavigationData, true, false, GlobalWidth, MiniBarHeight);

            serializedObject.ApplyModifiedProperties();
            QUI.Space(SPACE_4);
        }
    }
}
