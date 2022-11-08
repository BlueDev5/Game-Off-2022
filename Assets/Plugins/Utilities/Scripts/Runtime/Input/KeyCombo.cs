using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Utils
{
    /// <summary>
    /// A utility class for having Key combos like Shift+T, Ctrl+J
    /// </summary>
    [Serializable]
    public class KeyCombo
    {
        #region Variables
        [FormerlySerializedAs("Key")]
        [SerializeField] private KeyCode _key;

        [FormerlySerializedAs("Ctrl")]
        [SerializeField] private bool _ctrl;

        [FormerlySerializedAs("Shift")]
        [SerializeField] private bool _shift;

        [FormerlySerializedAs("Alt")]
        [SerializeField] private bool _alt;

        ///<summary> 
        /// The KeyCode To be used  
        /// </summary>
        public KeyCode Key { get => _key; }
        ///<summary> 
        /// Should check for Control key?  
        /// </summary>
        public bool Ctrl { get => _ctrl; }
        ///<summary> 
        /// Should check for Shift key?
        ///  </summary>
        public bool Shift { get => _shift; }
        ///<summary> 
        /// Should check for Alt key?
        ///  </summary>
        public bool Alt { get => _alt; }

        /// <summary>
        /// Returns true if all the enabled Modifiers are pressed
        /// </summary>
        public bool AreModifiersPressed
        {
            get
            {
                bool flag = true;
                if (_alt)
                {
                    if (InputUtils.IsAlt()) flag = flag && true;
                    else flag = flag && false;
                }

                if (_shift)
                {
                    if (InputUtils.IsShift()) flag = flag && true;
                    else flag = flag && false;
                }

                if (_ctrl)
                {
                    if (InputUtils.IsCtrl()) flag = flag && true;
                    else flag = flag && false;
                }

                return flag;
            }
        }
        #endregion


        #region Constructors
        public KeyCombo(KeyCode key = KeyCode.Tab, bool ctrl = false, bool shift = false, bool alt = false)
        {
            _key = key;
            _ctrl = ctrl;
            _shift = shift;
            _alt = alt;
        }
        #endregion


        #region Functions
        /// <summary>
        /// Is the specified key pressed down
        /// </summary>
        public bool IsComboDown()
        {
            bool flag = AreModifiersPressed;
            bool keyPressed = Input.GetKeyDown(_key);

            Debug.Log($"Flag: {flag}");
            Debug.Log($"Key: {keyPressed}");

            return flag && keyPressed;
        }

        /// <summary>
        /// is the specified key pressed
        /// </summary>
        public bool IsCombo()
        {
            bool flag = AreModifiersPressed;
            bool keyPressed = Input.GetKey(_key);

            Debug.Log($"Flag: {flag}");
            Debug.Log($"Key: {keyPressed}");

            return flag && keyPressed;
        }

        /// <summary>
        /// is the specified key pressed up
        /// </summary>
        public bool IsComboUp()
        {
            bool flag = AreModifiersPressed;
            bool keyPressed = Input.GetKeyUp(_key);

            return flag && keyPressed;
        }

        public override string ToString()
        {
            return $"Key: {_key}\n" +
                    $"Alt: {_alt}\n" +
                    $"Ctrl: {_ctrl}\n" +
                    $"Shift: {_shift}\n";
        }
        #endregion
    }
}