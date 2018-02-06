// Copyright (c) 2015 - 2018 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using DoozyUI.Internal;
using QuickEditor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace DoozyUI
{
    public partial class ControlPanelWindow : QWindow
    {

        DUINewsArticles News { get { return DUINewsArticles.Instance; } }

        GUIStyle buttonStylePlayMaker, buttonStyleMasterAudio, buttonStyleEnergyBarToolkit, buttonStyleTextMeshPro;
        GUIStyle buttonStyleUINavigation, buttonStyleOrientationManager, buttonStyleSceneLoader;
        GUIStyle GetSupportedAssetButtonStyle(QTexture qTexture)
        {
            return new GUIStyle
            {
                normal = { background = qTexture.normal2D },
                onNormal = { background = qTexture.normal2D },
                hover = { background = qTexture.hover2D },
                onHover = { background = qTexture.hover2D },
                active = { background = qTexture.active2D },
                onActive = { background = qTexture.active2D },
            };
        }

        AnimBool showAddNews, showDefineSymbols;

        NewsArticleData newNewsArticle = new NewsArticleData();

        List<string> symbols;

        void InitPageGeneral()
        {
            buttonStylePlayMaker = GetSupportedAssetButtonStyle(DUI.PlayMakerEnabled ? DUIResources.buttonPlayMakerEnabled : DUIResources.buttonPlayMakerDisabled);
            buttonStyleMasterAudio = GetSupportedAssetButtonStyle(DUI.MasterAudioEnabled ? DUIResources.buttonMasterAudioEnabled : DUIResources.buttonMasterAudioDisabled);
            buttonStyleEnergyBarToolkit = GetSupportedAssetButtonStyle(DUI.EnergyBarToolkitEnabled ? DUIResources.buttonEnergyBarToolkitEnabled : DUIResources.buttonEnergyBarToolkitDisabled);
            buttonStyleTextMeshPro = GetSupportedAssetButtonStyle(DUI.TextMeshProEnabled ? DUIResources.buttonTextMeshProEnabled : DUIResources.buttonTextMeshProDisabled);

            buttonStyleUINavigation = GetSupportedAssetButtonStyle(DUI.UINavigationEnabled ? DUIResources.buttonUINavigationEnabled : DUIResources.buttonUINavigationDisabled);
            buttonStyleOrientationManager = GetSupportedAssetButtonStyle(DUI.OrientationManagerEnabled ? DUIResources.buttonOrientationManagerEnabled : DUIResources.buttonOrientationManagerDisabled);
            buttonStyleSceneLoader = GetSupportedAssetButtonStyle(DUI.SceneLoaderEnabled ? DUIResources.buttonSceneLoaderEnabled : DUIResources.buttonSceneLoaderDisabled);

            showAddNews = new AnimBool(false, Repaint);

            showDefineSymbols = new AnimBool(false, Repaint);
            symbols = QUtils.GetScriptingDefineSymbolsForGroup(QUtils.GetActiveBuildTargetGroup());
        }

        void DrawPageGeneral()
        {
            DrawPageHeader("General", QColors.Blue, "", QUI.IsProSkin ? QColors.UnityLight : QColors.UnityMild, DUIResources.pageIconGeneral);
            QUI.Space(SPACE_16);

            float leftColumnWidth = 280;
            float columnSpacing = 8;
            float rightColumnWidth = 280;

            QUI.BeginHorizontal();
            {
                DrawControlPanelGeneralSupportedAssets(leftColumnWidth);
                QUI.Space(columnSpacing);
                DrawControlPanelGeneralModules(rightColumnWidth);
            }
            QUI.EndHorizontal();

            QUI.Space(SPACE_16);

            DrawControlPanelDefineSymbols(WindowSettings.CurrentPageContentWidth);

            QUI.Space(SPACE_16);

            DrawNews(WindowSettings.CurrentPageContentWidth);
        }

        void DrawControlPanelGeneralSupportedAssets(float width)
        {
            QUI.BeginVertical(width);
            {
                DrawControlPanelGeneralButton("PlayMaker", buttonStylePlayMaker, DUI.PlayMakerEnabled, DUI.SYMBOL_PLAYMAKER, width);
                QUI.Space(SPACE_8);
                DrawControlPanelGeneralButton("MasterAudio", buttonStyleMasterAudio, DUI.MasterAudioEnabled, DUI.SYMBOL_MASTER_AUDIO, width);
                QUI.Space(SPACE_8);
                DrawControlPanelGeneralButton("EnergyBarToolkit", buttonStyleEnergyBarToolkit, DUI.EnergyBarToolkitEnabled, DUI.SYMBOL_ENERGY_BAR_TOOLKIT, width);
                QUI.Space(SPACE_8);
                DrawControlPanelGeneralButton("TextMeshPro", buttonStyleTextMeshPro, DUI.TextMeshProEnabled, DUI.SYMBOL_TEXT_MESH_PRO, width);
            }
            QUI.EndVertical();
        }

        void DrawControlPanelGeneralModules(float width)
        {
            QUI.BeginVertical(width);
            {
                DrawControlPanelGeneralButton("UINavigation", buttonStyleUINavigation, DUI.UINavigationEnabled, DUI.SYMBOL_NAVIGATION_SYSTEM, width, true);
                QUI.Space(SPACE_8);
                DrawControlPanelGeneralButton("OrientationManager", buttonStyleOrientationManager, DUI.OrientationManagerEnabled, DUI.SYMBOL_ORIENTATION_MANAGER, width, false);
                QUI.Space(SPACE_8);
                DrawControlPanelGeneralButton("SceneLoader", buttonStyleSceneLoader, DUI.SceneLoaderEnabled, DUI.SYMBOL_SCENE_LOADER, width, true);
            }
            QUI.EndVertical();
        }

        /// <summary>
        /// Draws a special button used to enable/disable asset/module options by using scripting define symbols and custom made button styles.
        /// </summary>
        /// <param name="buttonTarget">Supported asset name (like 'PlayMaker') or the module name (like 'UINavigation').</param>
        /// <param name="buttonStyle">Custom button style that will be used to draw the button.</param>
        /// <param name="isEnabled">Bool that determines if the supplied symbol (the define symbol string) has been added to Player Settings or not.</param>
        /// <param name="symbol">The symbol that gets added/remoded to/from Scripting Define Symbols.</param>
        /// <param name="width">The width of the button.</param>
        /// <param name="inverseSymbol">This is a special bool that is used by the 'UINavigation', for example, as it needs to add a scripting define symbol to remove module options.
        ///                             It is used mostly by DoozyUI modules that are enabled by default and they can be disabled by adding a symbol to Player Settings and vice vresa.</param>
        void DrawControlPanelGeneralButton(string buttonTarget, GUIStyle buttonStyle, bool isEnabled, string symbol, float width, bool inverseSymbol = false)
        {
            QUI.BeginVertical(width);
            {
                if(EditorApplication.isCompiling)
                {
                    QUI.DrawTexture(DUIResources.buttonEditorIsCompiling.texture, width, 48);
                }
                else if(EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    QUI.DrawTexture(DUIResources.buttonEditorInPlayMode.texture, width, 48);
                }
                else
                {
                    if(QUI.Button(buttonStyle, width, 48))
                    {
                        string title, message;

                        if(isEnabled)
                        {
                            title = "Disable " + buttonTarget + " options?";
                            message = "This will " + (inverseSymbol ? "add" : "remove") + " '" + symbol + "' " + (inverseSymbol ? "to" : "from") + " " + "Scripting Define Symbols in Player Settings.";
                        }
                        else
                        {
                            title = "Enable " + buttonTarget + " options ?";
                            message = "This will " + (inverseSymbol ? "remove" : "add") + " '" + symbol + "' " + (inverseSymbol ? "from" : "to") + " " + "Scripting Define Symbols in Player Settings.";
                            message += inverseSymbol ? "" : "\n\nEnable this only if you have " + buttonTarget + " already installed.";
                        }

                        NotificationWindow.YesNo(title, message,
                                                 () =>
                                                 {
                                                     if(isEnabled)
                                                     {
                                                         if(inverseSymbol)
                                                         {
                                                             QUtils.AddScriptingDefineSymbol(symbol);
                                                         }
                                                         else
                                                         {
                                                             QUtils.RemoveScriptingDefineSymbol(symbol);
                                                         }
                                                     }
                                                     else
                                                     {
                                                         if(inverseSymbol)
                                                         {
                                                             QUtils.RemoveScriptingDefineSymbol(symbol);
                                                         }
                                                         else
                                                         {
                                                             QUtils.AddScriptingDefineSymbol(symbol);
                                                         }
                                                     }
                                                 },
                                                 null);
                    }
                }
            }
            QUI.EndVertical();
        }

        void DrawControlPanelDefineSymbols(float width)
        {
            QUI.DrawCollapsableStringList("View/Edit: '" + QUtils.GetActiveBuildTargetGroup() + "' Scripting Define Symbols", showDefineSymbols, QColors.Color.Gray, symbols, this, width, MiniBarHeight);
            QUI.BeginHorizontal(width);
            {
                QUI.Space((width - 120 - 2 - 160) * showDefineSymbols.faded);
                if(showDefineSymbols.faded > 0.8f)
                {
                    if(QUI.GhostButton("Refresh Symbols List", QColors.Color.Gray, 120 * showDefineSymbols.faded))
                    {
                        symbols = QUtils.GetScriptingDefineSymbolsForGroup(QUtils.GetActiveBuildTargetGroup());
                    }
                    QUI.Space(SPACE_2);
                    if(QUI.GhostButton("Save Changes to Symbols List", QColors.Color.Gray, 160 * showDefineSymbols.faded))
                    {
                        QUtils.SetScriptingDefineSymbolsForGroup(QUtils.GetActiveBuildTargetGroup(), symbols);
                        showDefineSymbols.target = false;
                    }
                }
            }
            QUI.EndHorizontal();
        }

        void DrawNews(float width)
        {
            if(News == null) { return; }

            QLabel.text = !showAddNews.target ? "" : "New Article";
            QLabel.style = Style.Text.Title;
            QUI.BeginHorizontal(width, 16);
            {
                if(showAddNews.target)
                {
                    QUI.SetGUIColor(QUI.AccentColorBlue);
                }
                QUI.Label(QLabel);
                QUI.ResetColors();
#if dUI_SOURCE
                QUI.Space(width - QLabel.x - 104 - 100 * showAddNews.faded);
                if(QUI.GhostButton(showAddNews.target ? "Save" : "Add News", showAddNews.target ? QColors.Color.Green : QColors.Color.Gray, 100, showAddNews.target))
                {
                    showAddNews.target = !showAddNews.target;
                    if(!showAddNews.target)
                    {
                        News.articles.Add(new NewsArticleData(newNewsArticle));
                        QUI.SetDirty(News);
                        AssetDatabase.SaveAssets();
                    }
                    newNewsArticle.Reset();
                }
                if(showAddNews.faded > 0.2f)
                {
                    if(QUI.GhostButton("Cancel", QColors.Color.Red, 100 * showAddNews.faded, showAddNews.target))
                    {
                        showAddNews.target = false;
                        newNewsArticle.Reset();
                    }
                }
#else
                QUI.FlexibleSpace();
#endif
            }
            QUI.EndHorizontal();
            QUI.Space(SPACE_2);
            if(QUI.BeginFadeGroup(showAddNews.faded))
            {
                QUI.BeginVertical(width);
                {
                    QUI.Space(SPACE_4 * showAddNews.faded);
                    QUI.DrawLine(QColors.Color.Blue, width);
                    QUI.Space(SPACE_4 * showAddNews.faded);
                    QUI.SetGUIBackgroundColor(QUI.AccentColorBlue);
                    QUI.BeginHorizontal(width);
                    {
                        QLabel.text = "Title";
                        QLabel.style = Style.Text.Normal;
                        QUI.Label(QLabel);
                        newNewsArticle.title = QUI.TextField(newNewsArticle.title, width - QLabel.x - 8);
                    }
                    QUI.EndHorizontal();
                    QUI.Space(SPACE_2 * showAddNews.faded);
                    QUI.BeginHorizontal(width);
                    {
                        QLabel.text = "Content";
                        QLabel.style = Style.Text.Normal;
                        QUI.Label(QLabel);
                        newNewsArticle.content = EditorGUILayout.TextArea(newNewsArticle.content, GUILayout.Width(width - QLabel.x - 8));
                    }
                    QUI.EndHorizontal();
                    QUI.ResetColors();
                    QUI.Space(SPACE_4 * showAddNews.faded);
                    QUI.DrawLine(QColors.Color.Blue, width);
                    QUI.Space(SPACE_16 * showAddNews.faded);
                }
                QUI.EndVertical();
            }
            QUI.EndFadeGroup();

            if(News.articles.Count == 0)
            {
                //QLabel.text = "There are no news...";
                //QLabel.style = Style.Text.Help;
                //QUI.Label(QLabel);
                return;
            }

            QUI.Space(SPACE_2);

            for(int i = 0; i < News.articles.Count; i++)
            {
#if dUI_SOURCE
                DrawNewsArticle(News.articles, i, News, true, width);
#else
                DrawNewsArticle(News.articles, i, News, false, width);
#endif
                QUI.Space(9 + 4);
            }

        }

        void DrawNewsArticle(List<NewsArticleData> articles, int index, Object targetObject, bool showDeleteButton, float width)
        {
            QLabel.text = articles[index].title;
            QLabel.style = Style.Text.Subtitle;
            QUI.Box(QStyles.GetBackgroundStyle(Style.BackgroundType.Low, QColors.Color.Gray), width, 20);
            QUI.Space(-20);
            QUI.BeginHorizontal(width);
            {
                QUI.Space(6);
                QUI.Label(QLabel);
                QUI.FlexibleSpace();
                if(showDeleteButton)
                {
                    if(QUI.ButtonMinus())
                    {
                        if(QUI.DisplayDialog("Delete Article", "Delete the '" + QLabel.text + "' article?", "Ok", "Cancel"))
                        {
                            articles.RemoveAt(index);
                            QUI.SetDirty(targetObject);
                            AssetDatabase.SaveAssets();
                            QUI.ExitGUI();
                        }
                    }
                }
                QUI.Space(SPACE_4);
            }
            QUI.EndHorizontal();
            QLabel.text = articles[index].content;
            QLabel.style = Style.Text.Normal;
            QUI.Space(-8 + (showDeleteButton ? 2 : 0));
            EditorGUILayout.LabelField(articles[index].content, QStyles.GetInfoMessageMessageStyle(Style.InfoMessage.Help), GUILayout.Width(width));
            QUI.Space(SPACE_4);
        }

        void DrawNewsArticle(string title, string content, float width)
        {
            QLabel.text = title;
            QLabel.style = Style.Text.Subtitle;
            QUI.Box(QStyles.GetBackgroundStyle(Style.BackgroundType.Low, QColors.Color.Gray), width, 20);
            QUI.Space(-20);
            QUI.BeginHorizontal(width);
            {
                QUI.Space(6);
                QUI.Label(QLabel);
                QUI.FlexibleSpace();
            }
            QUI.EndHorizontal();
            QLabel.text = content;
            QLabel.style = Style.Text.Normal;
            QUI.Space(-8);
            EditorGUILayout.LabelField(content, QStyles.GetInfoMessageMessageStyle(Style.InfoMessage.Help), GUILayout.Width(width));
        }
    }
}
