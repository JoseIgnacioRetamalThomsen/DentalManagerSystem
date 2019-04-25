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

        private void SaveIsLoginRequerid(bool value)
        {
            localSettings.Values["isLoginRequerid"] = value.ToString();
           
        }

        private bool getIsLoginRequerid()
        {
            try
            {
            
                return Convert.ToBoolean(localSettings.Values["isLoginRequerid"]);
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
                isLoginRequerid = Convert.ToBoolean(localSettings.Values["isLoginRequerid"]);
            }
            catch
            {
                isLoginRequerid = true;
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
    }
}
