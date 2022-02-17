using UnityEditor;
using UnityEngine;

/*
 * 
 *  Transform Colours
 *							  
 *	Customise Colours Editor Window
 *      The editor script that handles the customise colours functionality...
 *			
 *  Warning:
 *	    Please refrain from editing this script as it will cause issues to the asset...
 *			
 *  Written by:
 *      Jonathan Carter
 *
 *  Published By:
 *      Carter Games
 *      E: hello@carter.games
 *      W: https://www.carter.games
 *		
 *  Version: 1.2.0
 *	Last Updated: 22/10/2021 (d/m/y)							
 * 
 */

namespace CarterGames.Assets.TransformColours
{
    /// <summary>
    /// Controls the custom editor window for the user customise colours feature...
    /// </summary>
    public class CustomiseColoursEditorWindow : EditorWindow
    {
        // The colours used for the GUI 
        private static readonly Color Green = new Color32(72, 222, 55, 255);
        private static readonly Color Yellow =new Color32(245, 234, 56, 255);
        private static readonly Color Red = new Color32(255, 150, 157, 255);
        private static readonly Color Blue = new Color32(151, 196, 255, 255);

        // The prefix used for the player perfs for its colours 
        private static readonly string PerfPrefix = "CarterGames-TransformColours";
        
        // The ids for each in the transform component
        private const string XString = "XColour";
        private const string YString = "YColour";
        private const string ZString = "ZColour";

        // The colours for the colour fields on the editor window
        private Color currentXColour;
        private Color currentYColour;
        private Color currentZColour;

        // The default colours for the GUI
        private Color normalColour;
        private Color normalBackgroundColour;

        
        /// <summary>
        /// The menu item to open the GUI panel...
        /// </summary>
        [MenuItem("Tools/Transform Colours | CG/Customise Colours", priority = 1)]
        private static void ShowWindow()
        {
            var window = GetWindow<CustomiseColoursEditorWindow>();
            window.titleContent = new GUIContent("Customise Colours");
            window.Show();
        }


        private void OnEnable()
        {
            // Gets the current colours that are saved...
            currentXColour = GetColour(XString);
            currentYColour = GetColour(YString);
            currentZColour = GetColour(ZString);
            
            // Sets the size of the editor window...
            EditorWindow editorWindow = this;
            editorWindow.minSize = new Vector2(450f, 300f);
            editorWindow.maxSize = new Vector2(450f, 300f);

            // Gets the default colours of the GUI...
            normalColour = GUI.color;
            normalBackgroundColour = GUI.backgroundColor;
        }

        
        private void OnGUI()
        {
            EditorGUILayout.HelpBox(
                "Here you can customise the colours that are used in the transform colours asset, " +
                "simply edit the colour fields below and press \"Save Changes\" to change the colours.",
                MessageType.None);

            // The colour fields...
            EditorGUILayout.BeginHorizontal();
            GUI.color = Red;
            EditorGUILayout.LabelField("X Colour >", EditorStyles.boldLabel, GUILayout.Width(62.5f));
            GUI.color = normalColour;
            currentXColour = EditorGUILayout.ColorField(currentXColour);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUI.color = Green;
            EditorGUILayout.LabelField("Y Colour >", EditorStyles.boldLabel, GUILayout.Width(62.5f));
            GUI.color = normalColour;
            currentYColour = EditorGUILayout.ColorField(currentYColour);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            GUI.color = Blue;
            EditorGUILayout.LabelField("Z Colour >", EditorStyles.boldLabel, GUILayout.Width(62.5f));
            GUI.color = normalColour;
            currentZColour = EditorGUILayout.ColorField(currentZColour);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);
            
            // The save changes button
            GUI.backgroundColor = Green;
            if (GUILayout.Button("Save Changes"))
            {
                SetColour(XString, currentXColour);
                SetColour(YString, currentYColour);
                SetColour(ZString, currentZColour);

                Undo.RecordObject(this, "Undo Transform Colours Save Colour Changes");
            }
            GUI.backgroundColor = normalBackgroundColour;

            GUILayout.Space(5f);

            EditorGUILayout.HelpBox(
                "Want the default colours back? Reset the colours to their default values with the button below.",
                MessageType.None);
            
            // The reset button
            GUI.backgroundColor = Yellow;
            if (GUILayout.Button("Reset To Default"))
            {
                SetColour(XString, Color.red);
                SetColour(YString, Color.green);
                SetColour(ZString, Color.blue);
                currentXColour = GetColour(XString);
                currentYColour = GetColour(YString);
                currentZColour = GetColour(ZString);
            }
            GUI.backgroundColor = normalBackgroundColour;
        }


        /// <summary>
        /// Gets the colour of the entered string id
        /// </summary>
        /// <param name="colourID">The colour id to get</param>
        /// <returns>The colour requested</returns>
        public static Color GetColour(string colourID)
        {
            // If there is no perf saved
            if (!EditorPrefs.HasKey($"{PerfPrefix}-{colourID}-R"))
            {
                switch (colourID)
                {
                    case XString:
                        SetColour(XString, Color.red);
                        return Color.red;
                    case YString:
                        SetColour(YString, Color.green);
                        return Color.green;
                    case ZString:
                        SetColour(ZString, Color.blue);
                        return Color.blue;
                }
            }
            
            return new Color
            {
                r = EditorPrefs.GetFloat($"{PerfPrefix}-{colourID}-R"),
                g = EditorPrefs.GetFloat($"{PerfPrefix}-{colourID}-G"),
                b = EditorPrefs.GetFloat($"{PerfPrefix}-{colourID}-B"),
                a = 1
            };
        }

        
        /// <summary>
        /// Updates the colours in the editor perfs. 
        /// </summary>
        /// <param name="colourID">The color id to edit</param>
        /// <param name="col">The color to set</param>
        private static void SetColour(string colourID, Color col)
        {
            EditorPrefs.SetFloat($"{PerfPrefix}-{colourID}-R", col.r);
            EditorPrefs.SetFloat($"{PerfPrefix}-{colourID}-G", col.g);
            EditorPrefs.SetFloat($"{PerfPrefix}-{colourID}-B", col.b);
        }
    }
}