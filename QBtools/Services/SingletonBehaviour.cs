// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingletonBehaviour.cs" company="CNC Software, Inc.">
//   Copyright (c) 2019 CNC Software, Inc.
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace QBtools.Services
{
    using System;

    public abstract class SingletonBehaviour<T>
    {
        /// <summary>
        /// Backing field for the T Instance property
        /// </summary>
        private static T _instance;

        /// <summary>
        /// Returns the Instance of type T
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null && !_instance.Equals(null))
                {
                    return _instance;
                }

                _instance = Activator.CreateInstance<T>();

                return _instance;
            }
        }
    }
}