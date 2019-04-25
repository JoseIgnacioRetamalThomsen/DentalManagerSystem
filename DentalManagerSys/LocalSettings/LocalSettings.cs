using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagerSys
{
    public class LocalSettings
    {
        private const string BACKUP = "isBackup";
        private const string LOGIN_REQUERID = "isLoginRequerid";
        

        static Windows.Storage.ApplicationDataContainer localSettings =
      Windows.Storage.ApplicationData.Current.LocalSettings;

        private bool isLoginRequerid;

        public bool IsLoginRequerid
        {
            set
            {
                SaveIsLoginRequerid(value);
            }
            get
            {
                return getIsLoginRequerid();
            }
        }

        private bool isBackup;
        public bool IsBackup
        {
            set
            {
                saveIsBackUp(value);
            }
            get
            {
                return getIsBackup();
            }
        }

        private void saveIsBackUp(bool value)
        {
            localSettings.Values[BACKUP] = value.ToString();
        }

        

        private void SaveIsLoginRequerid(bool value)
        {
            localSettings.Values[LOGIN_REQUERID] = value.ToString();
           
        }


        /// <summary>
        /// Get work of line 
        /// </summary>
        /// <returns> Actual value, default is false</returns>
        private bool getIsBackup()
        {
            try
            {

                return Convert.ToBoolean(localSettings.Values[BACKUP]);
            }
            catch
            {
                return true;
            }
        }
        private bool getIsLoginRequerid()
        {
            try
            {
            
                return Convert.ToBoolean(localSettings.Values[LOGIN_REQUERID]);
            }
            catch
            {
                return true;
            }

        }

        public LocalSettings()
        {
            try
            {
                isLoginRequerid = Convert.ToBoolean(localSettings.Values[BACKUP]);
            }
            catch
            {
                isLoginRequerid = true;
            }

            try
            {
                isBackup = Convert.ToBoolean(localSettings.Values[LOGIN_REQUERID]);
            }
            catch
            {
                isBackup = true;
            }
        }


        /// <summary>
        /// Change login requerid to false when true and biseversa
        /// </summary>
        /// <returns> new IsLoginRequeridValue</returns>
        public bool ChangeIsLoginRequerid()
        {
            Debug.WriteLine(IsLoginRequerid);
            if (IsLoginRequerid)
            {
                SaveIsLoginRequerid(false);
                return false;

            }
            else
            {
                SaveIsLoginRequerid(true);
                return true;
            }
        }

        public bool ChangeIsBackup()
        {
            Debug.WriteLine(IsLoginRequerid);
            if (IsBackup)
            {
                saveIsBackUp(false);
                return false;

            }
            else
            {
                saveIsBackUp(true);
                return true;
            }
        }
    }
}
