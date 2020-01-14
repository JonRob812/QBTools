// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="CNC Software, Inc.">
//   Copyright (c) 2019 CNC Software, Inc.
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace QBtools.Models
{
    using System;

    /// <summary> A result. </summary>
    public class Result
    {
        /// <summary> Gets a value indicating whether this object is success. </summary>
        ///
        /// <value> True if this object is success, false if not. </value>
        public bool IsSuccess { get; }

        /// <summary> Gets the error. </summary>
        ///
        /// <value> The error. </value>
        public string Error { get; }

        /// <summary> Gets a value indicating whether this object is failure. </summary>
        ///
        /// <value> True if this object is failure, false if not. </value>
        public bool IsFailure => !this.IsSuccess;

        /// <summary> Specialized constructor for use only by derived class. </summary>
        ///
        /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid. </exception>
        ///
        /// <param name="isSuccess"> True if this object is success, false if not. </param>
        /// <param name="error">     The error. </param>
        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == string.Empty)
            {
                throw new InvalidOperationException();
            }

            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        /// <summary> Fails. </summary>
        ///
        /// <param name="message"> The message. </param>
        ///
        /// <returns> A Result. </returns>
        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        /// <summary> Fails. </summary>
        ///
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="message"> The message. </param>
        ///
        /// <returns> A Result. </returns>
        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        /// <summary> Gets the ok. </summary>
        ///
        /// <returns> A Result. </returns>
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        /// <summary> Gets the ok. </summary>
        ///
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="value"> The value. </param>
        ///
        /// <returns> A Result. </returns>
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }
    }

    /// <summary> A result. </summary>
    ///
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    public class Result<T> : Result
    {
        /// <summary> The value. </summary>
        private readonly T value;

        /// <summary> Gets the value. </summary>
        ///
        /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid. </exception>
        ///
        /// <value> The value. </value>
        public T Value
        {
            get
            {
                if (!this.IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return this.value;
            }
        }

        /// <summary> Constructor. </summary>
        ///
        /// <param name="value">     The value. </param>
        /// <param name="isSuccess"> True if this object is success. </param>
        /// <param name="error">     The error. </param>
        protected internal Result(T value, bool isSuccess, string error)
            : base(isSuccess, error)
        {
            this.value = value;
        }
    }
}