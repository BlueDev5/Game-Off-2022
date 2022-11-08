using UnityEngine;


namespace Utils.Init
{
    /// <summary>
    /// Wrapper for adding plain old classes to gameobjects
    /// </summary>
    public class Wrapper<Type> : MonoBehaviour, IInitable<Type>
    {
        #region Variables
        [SerializeField] private Type _data;

        private IOnUpdate _updateInterfaceData;
        private IOnFixedUpdate _fixedUpdateInterfaceData;
        private IOnLateUpdate _lateUpdateInterfaceData;
        #endregion


        #region Getters and Setters
        public Type Data => _data;
        #endregion


        #region Unity Calls
        private void Start()
        {
            _updateInterfaceData = _data as IOnUpdate;
            _fixedUpdateInterfaceData = _data as IOnFixedUpdate;
            _lateUpdateInterfaceData = _data as IOnLateUpdate;

            IOnStart converted = _data as IOnStart;

            if (converted != null) converted.Start();
        }

        private void OnEnable()
        {
            IOnEnable converted = _data as IOnEnable;

            if (converted != null) converted.OnEnable();
        }

        private void Update()
        {
            if (_updateInterfaceData != null) _updateInterfaceData.Update();
        }

        private void FixedUpdate()
        {
            if (_fixedUpdateInterfaceData != null) _fixedUpdateInterfaceData.FixedUpdate();
        }

        private void LateUpdate()
        {
            if (_lateUpdateInterfaceData != null) _lateUpdateInterfaceData.LateUpdate();
        }

        private void OnDestroy()
        {
            IOnDestroy converted = _data as IOnDestroy;

            if (converted != null) converted.OnDestroy();
        }

        private void OnDisable()
        {
            IOnDisable converted = _data as IOnDisable;

            if (converted != null) converted.OnDisable();
        }
        #endregion


        #region Functions
        public void Init(Type data)
        {
            _data = data;
        }
        #endregion
    }
}