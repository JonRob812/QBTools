// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceReaderService.cs" company="CNC Software, Inc.">
//   Copyright (c) 2019 CNC Software, Inc.
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace QBtools.Services
{
    using NETHookResources = Properties.Resources;
    using Models;

    public class ResourceReaderService : SingletonBehaviour<ResourceReaderService>
    {
        /// <summary>
        /// Gets the resource string from our resources
        /// </summary>
        /// <param name="name">The resource name</param>
        /// <returns>The value of the resource</returns>
        public static Result<string> GetString(string name)
        {
            try
            {
                return Result.Ok(NETHookResources.ResourceManager.GetString(name));
            }
            catch
            {
                return Result.Fail<string>($"Missing resource {name} ");
            }
        }
    }
}